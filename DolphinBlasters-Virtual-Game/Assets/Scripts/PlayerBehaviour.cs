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

        if (_got_hit == false && _is_dashing == false)
        {
            Move();
            Rotate();

            //throws the ball once the player presses the fire button and he has something that he can throw
            if (Input.GetButton("Fire" + _player_number) && _ball != null)
            {
                Fire();
            }
        }

        Dodge();
    }

    //moves the player according to the input given by the controller
    protected override void Move()
    {
        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal" + _player_number), 0, Input.GetAxis("Vertical" + _player_number)) * _movement_speed * Time.deltaTime;
        _rb.velocity = velocity;
    }

    //TODO: find a solution for GetButton
    protected override void Dodge()
    {
        if(_is_dashing == false)
        {
            if(Input.GetButton("Dodge" + _player_number))
            {
                Debug.Log("DASH");
                _is_dashing = true;
            }
        }
        else
        {
            if(_dash_time <= 0)
            {
                _is_dashing = false;
                _dash_time = _start_dash_time;
            }
            else
            {
                _dash_time -= Time.deltaTime;

                _rb.velocity = transform.forward * _dash_speed * Time.deltaTime;
            }
        }
    }

    //Fires the ball and sets the ball to null
    protected override void Fire()
    {
        Rigidbody ball_rb = _ball.GetComponent<Rigidbody>();
        ball_rb.velocity = Vector3.zero;
        ball_rb.AddForce(new Vector3(transform.forward.normalized.x * 2000, 0, transform.forward.normalized.z * 2000) /* * _throwing_force?*/, ForceMode.Force);
        _ball = null;
        GameManager.current_ball_owner = null;
    }
}
