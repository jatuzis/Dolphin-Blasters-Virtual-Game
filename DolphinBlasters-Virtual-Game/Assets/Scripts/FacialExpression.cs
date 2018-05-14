using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this will change the facial expression of the robots according to the game flow
public class FacialExpression : MonoBehaviour {

	private MeshRenderer _renderer;

	[SerializeField]
	private Material[] _angry;

	// Use this for initialization
	void Start () {
		_renderer = GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A))
		{
			_renderer.material = _angry[0];
		}
	}
}
