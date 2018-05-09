using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerBehaviour : CharacterBehaviour {

    [SerializeField]
    private int _player_number;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private new void Update()
    {
        base.Update();
        if(_ball != null)
        {
            _ball.transform.position = _holder.position;
        }

        if (_got_hit == false)
        {
            Move();
            Rotate();

            //throws the ball once the player presses the fire button and he has something that he can throw
            if (Input.GetButton("Fire" + _player_number) && _ball != null)
            {
                Fire();
            }
        }
    }

    //moves the player according to the input given by the controller
    protected override void Move()
    {
        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal" + _player_number), 0, Input.GetAxis("Vertical" + _player_number)) * _movement_speed * Time.deltaTime;
        _rb.velocity = velocity;
    }

    //Fires the ball and sets the ball to null
    protected override void Fire()
    {
        Rigidbody ball_rb = _ball.GetComponent<Rigidbody>();
        ball_rb.velocity = Vector3.zero;
        ball_rb.AddForce(new Vector3(transform.forward.x * 2000, 0, transform.forward.z * 2000) /* * _throwing_force?*/, ForceMode.Force);
        _ball = null;
        GameManager.current_ball_owner = null;
    }
}
