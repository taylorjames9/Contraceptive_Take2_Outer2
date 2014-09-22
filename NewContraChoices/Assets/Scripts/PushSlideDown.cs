using UnityEngine;
using System.Collections;

public class PushSlideDown : MonoBehaviour {

	public GameObject myAuxSlide;
	public Vector3 myHidingSpot;
	public Vector3 myAuxVex3;
	public GameObject condomBackToOptionsBtn;

	void Awake(){
			myHidingSpot = new Vector3 (myAuxSlide.transform.position.x, (myAuxSlide.transform.position.y - 11.0f), myAuxSlide.transform.position.z);

	}
	
	void Update(){
			myAuxVex3 = new Vector3 (myAuxSlide.transform.position.x, myAuxSlide.transform.position.y, myAuxSlide.transform.position.z);
			//myHidingSpot = new Vector3 (myAuxSlide.transform.position.x, (myAuxSlide.transform.position.y - 11.0f), myAuxSlide.transform.position.z);

	}


	void OnMouseDown()
	{
		Debug.Log ("BLACK BUTTON");
		StartCoroutine("move");

	}

	IEnumerator move(){
			condomBackToOptionsBtn.SetActive (true);
			myAuxSlide.transform.position = myHidingSpot;
			myAuxSlide.SetActive (false);
			GameObject masterCollider = GameObject.FindWithTag ("MasterCollider");
			//masterCollider.collider2D.enabled = true;
			Component[] blackBtns = this.transform.parent.transform.parent.transform.parent.transform.GetComponentsInChildren<BoxCollider2D> ();
				print ("this is the blackbtns ArrayList: " + blackBtns); 
			foreach (BoxCollider2D blckbtns in blackBtns) {
				blckbtns.enabled = true;
				//print ("disabled a child");
			}


			yield return null;
	}
}
