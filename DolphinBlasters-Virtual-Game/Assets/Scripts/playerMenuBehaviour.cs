using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script manages the cursor control of each player and lets them choose a character which it will pass on to the GameManager
public class playerMenuBehaviour : MonoBehaviour {

	[SerializeField]
	private int _player_number;

	[SerializeField]
	protected float _movement_speed;

	protected Rigidbody _rb;

	private bool _selected_character;

	GameObject _character;

	// Use this for initialization
	void Start () {
		_rb = GetComponent<Rigidbody> ();
		_selected_character = false;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 velocity = new Vector3(Input.GetAxis("Horizontal" + _player_number),0, Input.GetAxis("Vertical" + _player_number)) * _movement_speed * Time.deltaTime;
		_rb.velocity = velocity;


		 if(Input.GetButton("Deselect" + _player_number) && _selected_character == true)
		{
			_selected_character = false;
			Debug.Log ("PLayer "+ _player_number+ " unchooses: " + _character.name);
			_character = null;
		}

	}

	private void OnTriggerStay(Collider other)
	{
		if (Input.GetButton ("Select" + _player_number) && _selected_character == false)
		{
			_selected_character = true;
			_character = other.gameObject;
			Debug.Log ("PLayer "+ _player_number+ " chooses: " + _character.name);
		} 


	}
}
