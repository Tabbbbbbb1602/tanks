using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBulletEnemy : MonoBehaviour
{
    //bullet collision with wall and player
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("Player")) 
        {
            gameObject.SetActive(false);
        }
    }
}
