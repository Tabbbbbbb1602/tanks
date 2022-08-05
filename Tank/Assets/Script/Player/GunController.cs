using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GunController : MonoBehaviour, Idame
{
    //Pick which object pool you want to use
    //Chọn nhóm đối tượng bạn muốn sử dụng
    public BulletObjectPool bulletPool;
    public Transform barel;

    //public BulletObjectPoolOptimized bulletPool;


    //Private
    private float fireTimer;

    private float fireInterval = 0.1f;

    private int health;

    public AudioManager audioManager;

    //Define Action
    public static event Action onDeathEnemy;

    public GameObject explosion;

    void Start()
    {
        health = 100;
        fireTimer = Mathf.Infinity;

        if (bulletPool == null)
        {
            Debug.LogError("Need a reference to the object pool");
        }
      /*  audio.GetComponent<AudioSource>();
        audioManager.Play("draw");*/
    }

    void Update()
    {
        ShootBullet();
    }


    private void ShootBullet()
    {
        //Fire gun
        if (Input.GetKey(KeyCode.Space) && fireTimer > fireInterval)
        {
            fireTimer = 0f;

            GameObject newBullet = GetABullet();

            if (newBullet != null)
            {
                newBullet.SetActive(true);

                newBullet.transform.forward = barel.transform.forward;

                //Move the bullet to the tip of the gun or it will look strange if we rotate while firing
                //Di chuyển viên đạn đến đầu súng nếu không chúng ta vừa xoay vừa bắn sẽ trông rất lạ.
                newBullet.transform.position = barel.transform.position;
                //audio.GetComponent<AudioSource>();
                audioManager.Play("fire");
            }
            else
            {
                Debug.Log("Couldn't find a new bullet");
            }
        }


        //Update the time since we last fired a bullet
        //Cập nhật thời gian kể từ lần cuối chúng ta bắn một viên đạn
        fireTimer += Time.deltaTime;
    }


    private GameObject GetABullet()
    {
        GameObject bullet = bulletPool.GetBullet();

        return bullet;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("butlletEnemy"))
        {
            if (health > 0)
            {
                takeDamage(50);
                if (health == 0)
                {
                    Death();
                }
            }
        }
    }

    private void Death()
    {
        if (onDeathEnemy != null)
        {
            onDeathEnemy?.Invoke();
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
