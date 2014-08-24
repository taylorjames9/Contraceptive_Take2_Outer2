using UnityEngine;
using System.Collections;

public class SmoothPageSlide_2 : MonoBehaviour {


		private float xPos; 
		private bool moveForwardBool;
		private bool moveBackwardBool;
		private bool moveBackToCurrentSpot;

		private bool handleFingerInput = false;

		private int slideNum = 0;
		private float shiftAmt = 13.5f;
		public int totalNumSlides; 

		float fingerDragMagnitude;

		public bool fingerTouchedDown = false; 

		float originalSpotDownX = 0;

		//For move forward and move back functions

		private Vector2 spotNow;
		private Vector2 spotNext;
		private Vector2 spotPrev;

		bool moveForwardStarted = false;
		bool moveBackwardStarted = false;


	// Use this for initialization
	void Start () {
				spotPrev = new Vector2 (spotNow.x + shiftAmt, 0f);
				spotNow = Vector2.zero;
				spotNext = new Vector2 (spotNow.x - shiftAmt, 0f);
	}
	
	// Update is called once per frame
	void Update () {

				if (Input.touchCount > 0) {
						for (int i = 0; i < Input.touchCount; i++) {
								Vector2 inputPosition = Input.touches[i].position;
								if (Input.touches [i].phase == TouchPhase.Began) {

										handleFingerInput = true;
										if (!fingerTouchedDown) {
												originalSpotDownX = inputPosition.x;
												print ("Record Original FingerTouchDown Position " + originalSpotDownX);
												fingerTouchedDown = true;
										}
								} else if ((Input.touches [i].phase == TouchPhase.Moved || Input.touches [i].phase == TouchPhase.Stationary) && handleFingerInput) {
										Vector2 touchDelta = Input.GetTouch (0).deltaPosition;
										Debug.Log ("ORIGNAL POSITION " + originalSpotDownX);
										Debug.Log ("Cureent INpUTPOSTION " + inputPosition.x);
										float xPos = (slideNum * shiftAmt) + ((inputPosition.x - originalSpotDownX)/ (Screen.width/2));
										Debug.Log ("xPos " + xPos);
										transform.position = new Vector2 (xPos, 0f);
										fingerDragMagnitude = touchDelta.x;
								} else if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended && handleFingerInput) {
										fingerTouchedDown = false;
										handleFingerInput = false; 
										Debug.Log ("fingerDragMagnitude = " + fingerDragMagnitude);
										if (fingerDragMagnitude > 2f) {
												moveForwardBool = true;
												Debug.Log ("Prompt to move forward");
										} else if (fingerDragMagnitude < -2f) {
												moveBackwardBool = true;
												Debug.Log ("Prompt to move back");
										}
										else
												moveBackToCurrentSpot = true;
												Debug.Log ("Prompt to return to curr position");
								}
						}
				}
				if (moveForwardBool) {
						moveForward ();
				} else if (moveBackwardBool) {
						moveBack ();
				} else if (moveBackToCurrentSpot) {
						returnToCurrentSpot ();
				}
		}

		void moveForward(){
				print ("SlideNum " + slideNum + " .totalNumSlide is" + totalNumSlides);
				//if w're not at the last slide, lerp to the next slide
				if (slideNum < totalNumSlides) {
						transform.position = Vector3.Lerp (transform.position, spotNext, 10 * Time.deltaTime);
				}
		
		}

		void moveBack(){

		}

		void returnToCurrentSpot(){

		}
}
