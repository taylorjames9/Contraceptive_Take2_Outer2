using UnityEngine;
using System.Collections;

public class ForceBack : MonoBehaviour {

		public GameObject sliderTrack;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp(){
				Debug.Log ("Force Back");
				//gameObject.SetActive (false);
				//sliderTrack = GameObject.Find ("SlideSwipe_Holder");
				SmoothPageSlide_2 pageSlideScript = sliderTrack.GetComponent<SmoothPageSlide_2>();
				pageSlideScript.forceBackBool = true;
				//sliderTrack.collider2D.enabled = true;
				foreach(BoxCollider2D c in sliderTrack.GetComponents<BoxCollider2D> ()) {
						c.enabled = true;
				}
		}


}
