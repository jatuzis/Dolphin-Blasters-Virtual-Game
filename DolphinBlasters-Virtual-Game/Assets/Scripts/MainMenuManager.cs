using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {

	public void QuitGame()
	{
		Debug.Log ("This game will be quitted once it has becomes an exe");
		Application.Quit ();
	}
}
