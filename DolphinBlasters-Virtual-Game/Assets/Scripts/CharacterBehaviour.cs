using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBehaviour : MonoBehaviour {

    [SerializeField]
    protected float _max_ball_velocity;

    [SerializeField]
    protected float _movement_speed;

    [SerializeField]
    protected Transform _holder;

    protected GameObject _ball;

    protected Rigidbody _rb;

    protected abstract void Move();
    protected abstract void Fire();

    //rotates the player towards the direction he is moving
    protected void Rotate()
    {
        Vector3 velocity = _rb.velocity;
        if (velocity.magnitude > 0.2f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(velocity.x, 0, velocity.z));
        }
    }

    //recognizes collisions
    //if collided with the ball while the ball is slow enough he picks up the ball
    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            GameObject ball = collision.gameObject;
            Rigidbody ball_rb = ball.GetComponent<Rigidbody>();
            if (ball_rb.velocity.magnitude < _max_ball_velocity && GameManager.current_ball_owner == null)
            {
                _ball = ball;
                ball_rb.velocity = Vector3.zero;
                GameManager.current_ball_owner = this;
            }
        }
    }
}
