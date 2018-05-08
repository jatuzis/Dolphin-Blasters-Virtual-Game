using System.Collections;
using System.Collections.Generic;
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
        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * _movement_speed * Time.deltaTime;
        _rb.velocity = velocity;
    }

    void Rotate()
    {
        Vector3 velocity = _rb.velocity;
        transform.rotation = Quaternion.LookRotation(new Vector3(velocity.x, 0, velocity.z));
    }
}
