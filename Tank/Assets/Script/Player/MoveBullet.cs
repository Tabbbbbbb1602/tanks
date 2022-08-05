using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveBullet : MonoBehaviour
{
    private float bulletSpeed = 80f;

    DateTime startTime;
    DateTime endTime;

    private void OnEnable()
    {
        startTime = DateTime.Now;
        endTime = startTime.AddSeconds(2);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);

        if(DateTime.Now > endTime)
        {
            gameObject.SetActive(false);
        }
    }

    //bullet collision with wall
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            gameObject.SetActive(false);
        }
    }
}
