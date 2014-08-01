using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

	public string sceneName = "Arm_A";

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown(){
		Application.LoadLevel (sceneName); 
	}
}
