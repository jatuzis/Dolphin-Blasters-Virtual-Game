using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerSelectionController : MonoBehaviour {

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
		Vector3 velocity = new Vector3(Input.GetAxis("Horizontal" + _player_number), Input.GetAxis("Vertical" + _player_number), 0) * _movement_speed * Time.deltaTime;
		_rb.velocity = velocity;
	}
}
