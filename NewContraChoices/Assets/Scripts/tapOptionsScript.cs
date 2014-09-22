using UnityEngine;
using System.Collections;

public class tapOptionsScript : MonoBehaviour {


		public GameObject myCompatriot;
		public GameObject sliderTrack;
		public GameObject myOption1;
		public GameObject[] cardArray;
		string compatriotName;
		//public GameObject myOptionsAll;



		void Start(){
			compatriotName = myCompatriot.name;
		}


		void OnMouseUp(){
				Debug.Log ("Condoms Hit. Slider Track should be disabled.");
				myCompatriot.SetActive (true);
				myCompatriot.transform.position = new Vector2 (myOption1.transform.position.x, myOption1.transform.position.y);
				foreach (GameObject card in cardArray) {
						if(!string.Equals(card.name, compatriotName)){
								card.SetActive (false);
						}
				}
				SmoothPageSlide_2 pageSlideScript = sliderTrack.GetComponent<SmoothPageSlide_2>();
				pageSlideScript.forceForwardBool = true;
				foreach(BoxCollider2D c in sliderTrack.GetComponents<BoxCollider2D> ()) {
						c.enabled = false;
				}
				Debug.Log ("PRINTING moveForwardBool from myOptions page " + pageSlideScript.forceForwardBool);
		}
}
