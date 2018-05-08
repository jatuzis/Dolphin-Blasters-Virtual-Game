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

        //throws the ball once the player presses the fire button and he has something that he can throw
        if(Input.GetButton("Fire1") && _ball != null)
        {
            Fire();
        }
    }

    //moves the player according to the input given by the controller
    void Move()
    {
        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * _movement_speed * Time.deltaTime;
        _rb.velocity = velocity;
    }

    //rotates the player so he is always watching in the direction he is moving
    void Rotate()
    {
        Vector3 velocity = _rb.velocity;
        if (velocity.magnitude > 0.2f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(velocity.x, 0, velocity.z));
        }
    }

    //Fires the ball and sets the ball to null
    void Fire()
    {
        Rigidbody ball_rb = _ball.GetComponent<Rigidbody>();
        ball_rb.velocity = Vector3.zero;
        ball_rb.AddForce(new Vector3(transform.forward.x * 1000, 0, transform.forward.z * 1000) /* * _throwing_force?*/, ForceMode.Force);
        _ball = null;
    }

    //recognizes collisions
    //if collided with the ball while the ball is slow enough he picks up the ball
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
