using UnityEngine;
using System.Collections;

public class TapChildAppear : MonoBehaviour {


	public GameObject myCompatriot;
	public GameObject swipeHolder;

	// Use this for initialization
	void Start () {
		myCompatriot.SetActive (false);
		//swipeHolder = GameObject.transform.parent.transform.parent.GetComponent (BoxCollider2D);
		//BoxCollider2D boxColl = swipeHolder.GetComponent(BoxCollider2D);

	}

	void OnMouseOver(){
		print ("Collder should be deactivated");
		
				//this works
				//this.transform.parent.transform.parent.transform.collider2D.enabled = false;
	}

	void OnMouseDown(){
		
		myCompatriot.SetActive (true);
		myCompatriot.transform.position = myCompatriot.transform.parent.transform.parent.position;
		GameObject masterCollider = GameObject.FindWithTag ("MasterCollider");
	    masterCollider.collider2D.enabled = false;
		Component[] blackBtns = this.transform.parent.transform.GetComponentsInChildren<BoxCollider2D> ();
		//print ("this is the blackbtns ArrayList: " + blackBtns); 
		foreach (BoxCollider2D blckbtns in blackBtns) {
						if (blckbtns.name != "BlackBtn_Back") {
								blckbtns.enabled = false;
								print ("blackbtns " + blckbtns);
						}
		}
	}
}
