using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour {

	[SerializeField]
	private Button[] _buttons;

	[SerializeField]
	private EventSystem eventSystem;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void increaseOnHover(Button other)
	{
		other.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
	}

	public void decreaseOnHoverOff(Button other)
	{
		other.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	public void OnMenuChange(GameObject newHighlightedObject)
	{
		eventSystem.SetSelectedGameObject (newHighlightedObject);
	}
}
