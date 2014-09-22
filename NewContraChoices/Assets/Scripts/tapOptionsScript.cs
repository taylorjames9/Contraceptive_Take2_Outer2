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
				//Debug.Log ("compatriot name " + myCompatriot.name);

				//SmoothPageSlide_2 sliderTrack.GetComponent("SmoothPageSlide_2");
		}


		void OnMouseUp(){
				Debug.Log ("Hit Me");
				myCompatriot.SetActive (true);
				//float optionSlideXPos = myOptionSlide.transform.position.x;
				//GameObject myPlaceHolderObject = GameObject.Find ("MyOption1");
				myCompatriot.transform.position = new Vector2 (myOption1.transform.position.x, myOption1.transform.position.y);
				foreach (GameObject card in cardArray) {
						if(!string.Equals(card.name, compatriotName)){
								card.SetActive (false);
						}
				}
				//GameObject myOptionsAll = GameObject.Find ("MyOptions_All");
				//myOptionsAll.SetActive (false);

				sliderTrack = GameObject.Find ("SlideSwipe_Holder");
				SmoothPageSlide_2 pageSlideScript = sliderTrack.GetComponent<SmoothPageSlide_2>();
				pageSlideScript.forceForwardBool = true;
				//sliderTrack.collider2D.enabled = false;
				Debug.Log ("PRINTING moveForwardBool from myOptions page " + pageSlideScript.forceForwardBool);
		}
}
