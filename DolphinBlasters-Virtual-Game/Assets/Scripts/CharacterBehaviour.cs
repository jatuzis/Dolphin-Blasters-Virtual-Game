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

    protected bool _got_hit;

    protected bool _is_at_wall;

    protected float _hit_timer;

    [SerializeField]
    protected float _desired_hit_timer;

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

    public void Update()
    {
        if(_hit_timer < 0)
        {
            _got_hit = false;
        }
        else
        {
            _hit_timer -= Time.deltaTime;
        }
    }

    protected void CalculateBlowBack(GameObject obj)
    {
        if(obj.tag == "Ball")
        {
            Vector3 dir = transform.position - obj.transform.position;
            Debug.Log(dir.normalized * _bounce_multiplier + " " + _bounce_multiplier);
            _rb.AddForce(dir * _bounce_multiplier, ForceMode.Impulse);

            _got_hit = true;
            _hit_timer = _desired_hit_timer;

            dir = obj.transform.position - transform.position;
            Rigidbody obj_rb = obj.GetComponent<Rigidbody>();
            obj_rb.velocity = Vector3.zero;
            obj_rb.AddForce(dir.normalized * 1000);
        }
    }

    //increases the _bounce_multiplier so it gets harder for the player when he gets hit 
    protected void ReceiveDamage(GameObject obj)
    {
        if (_is_at_wall == true)
        {
            Die();
            Destroy(this.gameObject);
            return;
        }
        CalculateBlowBack(obj);
        _bounce_multiplier *= 1.1f;
    }

    //Destroys the character
    protected void Die()
    {
        Debug.Log("You died!");
        Destroy(this.gameObject);
    }

    protected void OnTriggerExit(Collider other)
    {
        if(other.tag == "Border")
        {
            _is_at_wall = false;
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Border")
        {
            _wall_hit_magnitude = _rb.velocity.magnitude;
            _is_at_wall = true;
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
            else if(GameManager.current_ball_owner == null)
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
