using UnityEngine;
using System.Collections;

public class TapChildAppear : MonoBehaviour {


	public GameObject myCompatriot;

	// Use this for initialization
	void Start () {
		myCompatriot.SetActive (false);
	}

	void OnMouseDown(){
		myCompatriot.SetActive (true);
		myCompatriot.transform.position = myCompatriot.transform.parent.transform.parent.position;
		//turn box collider off on SlideSwipeHolder
		GameObject masterCollider = GameObject.FindWithTag ("MasterCollider");
	    masterCollider.collider2D.enabled = false;
	}
}
