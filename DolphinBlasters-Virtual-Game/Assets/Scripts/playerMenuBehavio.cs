using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMenuBehavio : MonoBehaviour {

	[SerializeField]
	private int _player_number;

	[SerializeField]
	protected float _movement_speed;

	protected Rigidbody _rb;

	private CharacterID selectedID;

	// Use this for initialization
	void Start () {
		_rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 velocity = new Vector3(Input.GetAxis("Horizontal" + _player_number),0, Input.GetAxis("Vertical" + _player_number)) * _movement_speed * Time.deltaTime;
		_rb.velocity = velocity;

	}

	private void OnTriggerEnter(Collider other)
	{
		
	}
}
