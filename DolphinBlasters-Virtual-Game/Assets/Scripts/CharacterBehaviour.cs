using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBehaviour : MonoBehaviour {

    [SerializeField]
    protected float _die_magnitude;

    [SerializeField]
    protected float _bounce_multiplier;

    [SerializeField]
    protected float _max_ball_velocity;

    [SerializeField]
    protected float _movement_speed;

    [SerializeField]
    protected Transform _holder;

    protected GameObject _ball;

    protected Rigidbody _rb;

    protected float _wall_hit_magnitude;

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

    protected void CalculateBlowBack(GameObject obj)
    {
        if(obj.tag == "Ball")
        {
            Vector3 dir = transform.position - obj.transform.position;
            Debug.Log(dir.normalized * _bounce_multiplier + " " + _bounce_multiplier);
            _rb.AddForce(dir.normalized * _bounce_multiplier, ForceMode.VelocityChange);

            dir = obj.transform.position - transform.position;
            Rigidbody obj_rb = obj.GetComponent<Rigidbody>();
            obj_rb.velocity = Vector3.zero;
            obj_rb.AddForce(dir * 1000);
        }
    }

    //increases the _bounce_multiplier so it gets harder for the player when he gets hit 
    protected void ReceiveDamage(GameObject obj)
    {
        CalculateBlowBack(obj);
        _bounce_multiplier *= 1.1f;
    }

    //Destroys the character
    protected void Die()
    {
        Debug.Log("You died!");
        Destroy(this.gameObject);
        return;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Border")
        {
            _wall_hit_magnitude = _rb.velocity.magnitude;
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
            else
            {
                ReceiveDamage(ball);
            }
        }
        if(collision.gameObject.tag == "Border")
        {
            if(_wall_hit_magnitude > _die_magnitude)
            {
                Die();
            }
        }
    }
}
