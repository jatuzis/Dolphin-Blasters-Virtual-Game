using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float _movement_speed;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        Debug.Log("Horizontal: " + Input.GetAxis("Horizontal") + "Vertical: " + Input.GetAxis("Vertical"));

        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * _movement_speed * Time.deltaTime;
        Debug.Log(velocity);
        _rb.velocity = velocity;
    }

    void Rotate()
    {
        Vector3 velocity = _rb.velocity;
        if (velocity.magnitude > 0.2f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(velocity.x, 0, velocity.z));
        }
    }
}
