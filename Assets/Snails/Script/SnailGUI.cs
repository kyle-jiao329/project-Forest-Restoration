using UnityEngine;
using System.Collections;

public class SnailGUI : MonoBehaviour {

	public SnailMovement SnailScript;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	
	void OnGUI() {

		GUI.Box(new Rect(15,15,250,30), "Use Arrows/WASD to move the snail");

		if (GUI.Button (new Rect (15, 60, 250, 30), "Switch Material")) {
			SnailScript.SwitchMaterial();
		}
		
	}
}
