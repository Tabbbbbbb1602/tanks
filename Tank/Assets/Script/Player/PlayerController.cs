using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;

    public AudioManager audioManager;
    DateTime nowTime;
    DateTime endTime;

    private void Update()
    {
        MoveOnTank();
        if(endTime < DateTime.Now)
        {
            audioManager.Stop("playerMove");
        }
    }

    private void MoveOnTank()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            controller.Move(direction * speed * Time.deltaTime);
            //audio
            audioManager.Play("playerMove");
            endTime = DateTime.Now.AddSeconds(2);

            //set state
            //iscanPause = true;
            //ismoving = true;
        }
    }
}
