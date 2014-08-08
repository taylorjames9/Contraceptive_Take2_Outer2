using UnityEngine;
using System.Collections;

public class PushSlideDown : MonoBehaviour {

	public GameObject myAuxSlide;
	public Vector3 myHidingSpot;
	public Vector3 myAuxVex3;

	void Start(){


	}
	
	void Update(){
	
			myAuxVex3 = new Vector3 (myAuxSlide.transform.position.x, myAuxSlide.transform.position.y, myAuxSlide.transform.position.z);
			myHidingSpot = new Vector3 (myAuxSlide.transform.position.x, (myAuxSlide.transform.position.y - 11.0f), myAuxSlide.transform.position.z);

	}


	void OnMouseDown()
	{
		Debug.Log ("BLACK BUTTON");
		StartCoroutine("move");

	}

	IEnumerator move(){
			//myAuxSlide.transform.position = Vector3.Lerp (myAuxVex3, myHidingSpot, 10*Time.deltaTime);
			myAuxSlide.transform.position = myHidingSpot;
			myAuxSlide.SetActive (false);
			GameObject masterCollider = GameObject.FindWithTag ("MasterCollider");
			masterCollider.collider2D.enabled = true;
			yield return null;
	}
}
