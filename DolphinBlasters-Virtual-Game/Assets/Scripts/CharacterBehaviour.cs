using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
public abstract class CharacterBehaviour : MonoBehaviour {

	[SerializeField]
	protected float _throw_power;

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

    [SerializeField]
    protected float _dash_speed;

    [SerializeField]
    protected float _start_dash_time;

    [SerializeField]
    protected float _max_ball_time;

    protected float _ball_time;

    protected float _dash_time;

    protected bool _is_dashing;

    protected GameObject _ball;

    protected Rigidbody _rb;

    protected float _wall_hit_magnitude;

    protected bool _got_hit;

    protected bool _is_at_wall;

    protected float _hit_timer;

    protected Vector3 _ball_velocity;

    protected Rigidbody _ball_rb;

    [SerializeField]
    protected float _desired_hit_timer;

    //protected CharacterController _controller;

    protected abstract void Move();
    protected abstract void Fire();
    protected abstract void Dodge();


    private void Start()
    {
        _dash_time = _start_dash_time;
    }

    //rotates the player towards the direction he is moving
    protected void Rotate()
    {
        Vector3 velocity = _rb.velocity;
        if (velocity.magnitude > 0.2f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(velocity.x, 0, velocity.z));
        }
    }

    protected void FixedUpdate()
    {
        //ball falls to ground if the holding time exceeds
        if (_ball != null)
        {
            if (_ball_time <= 0)
            {
                _ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
                _ball = null;
                GameManager.SetRestrictedCharacrter(this);
                GameManager.current_ball_owner = null;
            }
            else
            {
                _ball_time -= Time.deltaTime;
            }
        }
        if(_hit_timer <= 0)
        {
            _got_hit = false;
        }
        else
        {
            _hit_timer -= Time.deltaTime;
        }
    }

    //TODO: fix it that the character sometimes doesnt get a draw back
    protected void CalculateBlowBack(GameObject obj, Transform trans)
    {
        if(obj.tag == "Ball")
        {
			//_rb.velocity = Vector3.zero;
            Vector3 dir = transform.position - trans.position;
			Rigidbody obj_rb = obj.GetComponent<Rigidbody>();
			dir = obj_rb.velocity * _bounce_multiplier;
			_rb.velocity = dir;
            dir.y = 0f;
//            Debug.Log(dir + " " + dir.normalized + " " + dir.normalized * _bounce_multiplier + " " + _bounce_multiplier);
//			//dir = dir.normalized;
//			Debug.Log (dir.magnitude);
//			dir.Scale( new Vector3( _bounce_multiplier, _bounce_multiplier, _bounce_multiplier));
//			Debug.Log (dir.magnitude);
//			_rb.AddForce(dir);


            //dir = trans.position - transform.position;
            //dir.y = 0f;
//			dir.x = -dir.x ;
//			dir.z = -dir.z;
//            Rigidbody obj_rb = obj.GetComponent<Rigidbody>();
//			dir = -obj_rb.velocity;
//            obj_rb.velocity = Vector3.zero;
//			dir = dir.normalized;
//			//dir.Scale(new Vector3( 1000f,1000f,1000f)); 
//			Debug.Log (dir.magnitude);
//			obj_rb.AddForce(dir);
        }
        else
        {
            Vector3 dir = transform.position - trans.position;
            dir.y = 0f;
            Debug.Log(dir + " " + dir.normalized + " " + dir.normalized * _bounce_multiplier + " " + _bounce_multiplier);
            _rb.AddForce(dir.normalized * 0.1f * _bounce_multiplier);
        }
    }

    //increases the _bounce_multiplier so it gets harder for the player when he gets hit 
    public void ReceiveDamage(GameObject obj, Transform trans)
    {
        if (_is_at_wall == true)
        {
            Die();
            Destroy(this.gameObject);
            return;
        }
        CalculateBlowBack(obj, trans);
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
        if(other.tag == "Ball")
        {
            _ball_velocity = other.gameObject.GetComponent<Rigidbody>().velocity;
        }
    }

    protected void PickUpBall(GameObject ball, Rigidbody ball_rb)
    {
        _ball = ball;
        ball_rb.velocity = Vector3.zero;
        _ball_time = _max_ball_time;
        GameManager.current_ball_owner = this;
        _ball_rb = ball_rb;
    }

    //recognizes collisions
    //if collided with the ball while the ball is slow enough he picks up the ball
    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Transform trans = collision.gameObject.transform;
            GameObject ball = collision.gameObject;
            Rigidbody ball_rb = ball.GetComponent<Rigidbody>();
            if (_ball_velocity.magnitude < _max_ball_velocity && GameManager.current_ball_owner == null && GameManager.restricted_character != this)
            {
                PickUpBall(ball, ball_rb);
            }
            else if (GameManager.current_ball_owner == null && _got_hit == false && _ball_velocity.magnitude > _max_ball_velocity && _is_dashing == false)
            {
                _got_hit = true;
                _hit_timer = _desired_hit_timer;
                ReceiveDamage(ball, trans);
            }
            else if (GameManager.current_ball_owner == null && _got_hit == false && _is_dashing == true)
            {
                PickUpBall(ball, ball_rb);
            }
        }
        if(collision.gameObject.tag == "Border")
        {
            if(_wall_hit_magnitude > _die_magnitude && _got_hit == true)
            {
                Die();
            }
        }
    }
}
