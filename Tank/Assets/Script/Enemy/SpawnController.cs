using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject wallUnit;
    public GameObject spawnPoint;
    /*    public GameObject playerSpawn;*/
    public int wallLength;
    public GameObject enemy;
    /*    public GameObject player;*/
    private GameObject spawnPoint1;
    private GameObject spawnPoint2;
    private GameObject spawnPoint3;
    private int countEnemy = 0;

    private void Awake()
    {
        for (int i = 0; i < wallLength; i++)
        {
            for (int j = 0; j < wallLength; j++)
            {
                if (i == 0 || j == 0 || j == wallLength - 1 || i == wallLength - 1)
                {
                    Instantiate(wallUnit, new Vector3(i * 10, wallUnit.transform.position.y, j * 10), Quaternion.identity);
                }
            }
        }
        /*        playerSpawn = Instantiate(spawnPoint, new Vector3(245, wallUnit.transform.position.y, 50), Quaternion.identity);*/

        /*spawnPoint1 = Instantiate(spawnPoint, new Vector3(45, wallUnit.transform.position.y, 445), Quaternion.identity);
        spawnPoint2 = Instantiate(spawnPoint, new Vector3(245, wallUnit.transform.position.y, 445), Quaternion.identity);
        spawnPoint3 = Instantiate(spawnPoint, new Vector3(445, wallUnit.transform.position.y, 445), Quaternion.identity);*/

        spawnPoint1 = Instantiate(spawnPoint, new Vector3(104, 8, 104), Quaternion.identity);
        spawnPoint2 = Instantiate(spawnPoint, new Vector3(28, 10, 112), Quaternion.identity);
        spawnPoint3 = Instantiate(spawnPoint, new Vector3(65, 16, 150), Quaternion.identity);

    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnRandomEnemy(5f));
        /*        Instantiate(player, playerSpawn.transform.position, Quaternion.identity);*/
    }

    private IEnumerator spawnRandomEnemy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        /* Debug.Log("Start");*/
        Instantiate(enemy, spawnPoint1.transform.position, Quaternion.identity);
        Instantiate(enemy, spawnPoint2.transform.position, Quaternion.identity);
        Instantiate(enemy, spawnPoint3.transform.position, Quaternion.identity);

        countEnemy = countEnemy + 3;
        /*Debug.Log("Spawn " + Time.time);
        Debug.Log("Count: " + countEnemy);*/
        if (countEnemy < 8)
        {
            StartCoroutine(spawnRandomEnemy(waitTime));
        }
        else
        {
            yield break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
