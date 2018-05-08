using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    [SerializeField]
    private float _max_ball_velocity;

    [SerializeField]
    private float _movement_speed;

    [SerializeField]
    private Transform _holder;

    private GameObject _ball;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(_ball != null)
        {
            _ball.transform.position = _holder.position;
        }

        Move();
        Rotate();

        if(Input.GetButton("Fire1") && _ball != null)
        {
            Fire();
        }
    }

    void Move()
    {
        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * _movement_speed * Time.deltaTime;
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

    void Fire()
    {
        Rigidbody ball_rb = _ball.GetComponent<Rigidbody>();
        ball_rb.velocity = Vector3.zero;
        ball_rb.AddForce(new Vector3(transform.forward.x * 1000, 0, transform.forward.z * 1000) /* * _throwing_force?*/, ForceMode.Force);
        _ball = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            GameObject ball = collision.gameObject;
            Rigidbody ball_rb = ball.GetComponent<Rigidbody>();
            if(ball_rb.velocity.magnitude < _max_ball_velocity)
            {
                _ball = ball;
                ball_rb.velocity = Vector3.zero;
            }
        }
    }
}
