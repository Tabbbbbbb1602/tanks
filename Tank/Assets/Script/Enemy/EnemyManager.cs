using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, Idame
{
    [Range(0, 100)]
    [SerializeField]
    private float speed;
    public GameObject firePosition;
    public GameObject projectile;
    public GameObject explosion;
    private RaycastHit vision;
    [Range(0.1f, 100.0f)]
    [SerializeField]
    private float speedMove = 5f;
    private int health;
    private int visionRange;

    public AudioManager audioManager;

    public static event Action onDeath;
    void Start()
    {
        //
        audioManager = AudioManager.instance;
        health = 90;
        visionRange = 30;
        StartCoroutine(OnRandomMove());
        StartCoroutine(OnRandomShoot());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(firePosition.transform.position, transform.TransformDirection(Vector3.back) * 30, Color.red);
    }

    private void FixedUpdate()
    {

    }

    // Check have obstacle in vision range
    private bool CheckObstacle()
    {
        /*Ray ray = new(firePosition.transform.position, transform.TransformDirection(Vector3.back));*/
        if (Physics.Raycast(firePosition.transform.position, transform.TransformDirection(Vector3.back), out vision))
        {
            var hitPoint = vision.point;
            var distance = Vector3.Distance(hitPoint, firePosition.transform.position);
            return ((vision.collider.CompareTag("wall") || vision.collider.CompareTag("Enemy")) && distance < visionRange);
        }
        else
        {
            return false;
        }
    }

    // Randomly Move for enemy
    private IEnumerator OnRandomMove()
    {
        while (true)
        {
            // Check if there's obstacle in vision range, then random rotation another direction, then check is there obstacle again
            while (CheckObstacle())
            {
                yield return new WaitForSeconds(1);
                gameObject.transform.rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
            }

            // If there's no obstacle in vision range, then move on 0.5s forward
            yield return new WaitForSeconds(1);
            var moveTimer = .5f;
            while (moveTimer > 0)
            {
                gameObject.transform.Translate(speedMove * Time.deltaTime * Vector3.back);
                moveTimer -= Time.deltaTime;
                yield return null;
            }

        }
    }

    // Auto shoot on moving
    private IEnumerator OnRandomShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            Transform firePoint = firePosition.transform;
            Debug.DrawRay(firePoint.position, firePoint.transform.forward * 100, Color.red);
            Rigidbody rb = Instantiate(projectile, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce((firePosition.transform.forward) * 100f, ForceMode.Impulse);
            rb.AddTorque((firePosition.transform.up) * 1f, ForceMode.Impulse);
            yield return new WaitForSeconds(0.5f);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if collider has tag is ProjectilePlayer (this is bullet which shooted from  Player), then taken damage
        // If collider has other tag that differ from ProjectilePlayer, enemy don't take damage.
        if (collision.gameObject.CompareTag("bullet"))
        {
            if (health > 0)
            {
                takeDamage(30);
                if (health == 0)
                {
                    Death();
                }
            }
        }
    }

    private void Death()
    {
        if (onDeath != null)
        {
            onDeath?.Invoke();
        }
        gameObject.SetActive(false);

        //spawn prefab nổ
        Instantiate(explosion, transform.position, transform.rotation);
        //Audio
        audioManager.Play("enemydie");
    }

    public void takeDamage(int damage)
    {
        health -= damage;
    }
}
