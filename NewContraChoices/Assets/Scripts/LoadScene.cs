using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

	public string sceneName = "Arm_A";
	public GameObject myCheck;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown(){

		myCheck.renderer.enabled = true;
		Application.LoadLevel (sceneName); 
	}
}
