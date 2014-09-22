using UnityEngine;
using System.Collections;

public class SmoothPageSlide_2 : MonoBehaviour {


		private float xPos; 
		public bool moveForwardBool;
		public bool moveBackwardBool;
		public bool moveBackToCurrentSpot;

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

		bool snapToCurrentFor = false;
		bool snapToCurrentBac = false;

		public bool forceForwardBool = false;
		public bool forceBackBool = false;

		bool dontMove = false;


	// Use this for initialization
	void Start () {
				spotPrev = new Vector2 (spotNow.x + shiftAmt, 0f);
				spotNow = Vector2.zero;
				spotNext = new Vector2 (spotNow.x - shiftAmt, 0f);
	}
	
	// Update is called once per frame
	void Update () {

				if (Input.touchCount > 0 && !dontMove) {
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
										float xPos = (slideNum * shiftAmt * (-1)) + ((inputPosition.x - originalSpotDownX)/ (Screen.width/2));
										Debug.Log ("xPos " + xPos);
										transform.position = new Vector2 (xPos, 0f);
										fingerDragMagnitude = touchDelta.x;
								} else if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended && handleFingerInput) {
										fingerTouchedDown = false;
										handleFingerInput = false; 
										Debug.Log ("fingerDragMagnitude = " + fingerDragMagnitude);
										if (fingerDragMagnitude < -20f) {
												moveForwardBool = true;
												Debug.Log ("Prompt to move forward");
										} else if (fingerDragMagnitude > 20f) {
												moveBackwardBool = true;
												Debug.Log ("Prompt to move back");
										} else {
												moveBackToCurrentSpot = true;
												Debug.Log ("Prompt to return to curr position");
										}
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
				if (forceForwardBool) {
						forceForward ();
				}
				if (forceBackBool) {
						forceBack ();
				}
			


		}

		public void moveForward(){
				Debug.Log("Move Forward Called. SlideNum " + slideNum + " .totalNumSlide is" + totalNumSlides);
				//if w're not at the last slide, lerp to the next slide
				if (slideNum < totalNumSlides) {
						if (transform.position.x > spotNext.x + 0.1f) {
								Debug.Log ("Move Forward Started");
								Vector3 vex3ConversionOfSpotNextForLerp = spotNext;
								transform.position = Vector3.Lerp (transform.position, vex3ConversionOfSpotNextForLerp, 10 * Time.deltaTime);
								moveForwardStarted = true;
								Debug.Log ("Move forward started");
						}
						if (transform.position.x <= spotNext.x + 0.1f && moveForwardStarted) {
								transform.position = spotNext;
								Debug.Log ("Force MOVE COMPLETED");
								spotNow = spotNext; 
								spotNext = new Vector2 (spotNow.x - shiftAmt, 0f);
								spotPrev = new Vector2 (spotNow.x + shiftAmt, 0f);
								slideNum++;
								Debug.Log ("SlideNum after forward move = " + slideNum);
								moveForwardStarted = false;
								moveForwardBool = false;
								Debug.Log ("force forward completed");
						}
				} else {
						forwardHitBuffer ();
				}
		}

		public void moveBack(){
				print ("MoveBack Called .SlideNum " + slideNum + " .totalNumSlide is" + totalNumSlides);
				if (slideNum > 0) {
						if (transform.position.x < spotPrev.x - 0.1f) {
								Vector3 vex3ConversionOfSpotPrevForLerp = spotPrev;
								transform.position = Vector3.Lerp (transform.position, vex3ConversionOfSpotPrevForLerp, 10 * Time.deltaTime);
								moveBackwardStarted = true;
								print ("move back started");
						}
						if (transform.position.x >= spotPrev.x - 0.1f && moveBackwardStarted) {
								transform.position = spotPrev;
								spotNow = spotPrev;
								spotNext = new Vector3 (spotNow.x - shiftAmt, 0f);
								spotPrev = new Vector3 (spotNow.x + shiftAmt, 0f);
								moveBackwardBool = false;
								slideNum--;
								Debug.Log ("SlideNum after back move = " + slideNum);
								moveBackwardStarted = false;
								print ("Move back completed");
						}
				} else {
						backHitBuffer();
				}
		}

		void forwardHitBuffer(){
				spotNext = spotNow;
				Debug.Log ("Forward HIT BUFFER");
				if (transform.position.x > spotNext.x + 0.1f) {
						Vector3 vex3ConversionOfSpotNextForLerpBuffer = spotNext;
						transform.position = Vector3.Lerp (transform.position, vex3ConversionOfSpotNextForLerpBuffer, 10 * Time.deltaTime);
						//snapToCurrentFor = true;
				}
				if (transform.position.x <= spotNext.x + 0.1f /*&& snapToCurrentFor*/) {
						transform.position = spotNext;
						spotNow = spotNext; 
						spotNext = new Vector2 (spotNow.x - shiftAmt, 0f);
						spotPrev = new Vector2 (spotNow.x + shiftAmt, 0f);
						moveForwardBool = false;
				}
		}

		void backHitBuffer(){
				Debug.Log ("Back HIT BUFFER");
				spotPrev = spotNow;
				slideNum = 0;
				if (transform.position.x < spotPrev.x - 0.1f) {
						Debug.Log ("Inside first method of backHitBuffer");
						Vector3 vex3ConversionOfSpotPrevForLerpBuffer = spotPrev;
						transform.position = Vector3.Lerp (transform.position, vex3ConversionOfSpotPrevForLerpBuffer, 10 * Time.deltaTime);
						//snapToCurrentBac = true;
				} 
				if (transform.position.x >= spotPrev.x - 0.1f /*&& snapToCurrentBac*/) {
						Debug.Log ("Inside second method of backHitBuffer");
						transform.position = spotPrev;
						spotNow = spotPrev;
						spotNext = new Vector3 (spotNow.x - shiftAmt, 0f);
						spotPrev = new Vector3 (spotNow.x + shiftAmt, 0f);
						moveBackwardBool = false;
				}
		}

		void returnToCurrentSpot(){

		}

		void forceForward (){
				Debug.Log ("Force Forward Called. SlideNum " + slideNum + " .totalNumSlide is" + totalNumSlides);
				//if (slideNum < totalNumSlides) {
						transform.position = spotNext;
						Debug.Log ("Force forwrd COMPLETED");
						spotNow = spotNext; 
						spotNext = new Vector2 (spotNow.x - shiftAmt, 0f);
						spotPrev = new Vector2 (spotNow.x + shiftAmt, 0f);
						slideNum++;
						Debug.Log ("SlideNum after force forward move = " + slideNum);
						forceForwardBool = false;
						Debug.Log ("force forward completed");
						dontMove = true;
				//}
		}
		void forceBack (){
				Debug.Log ("Force Back Called. SlideNum " + slideNum + " .totalNumSlide is" + totalNumSlides);
				//if (slideNum < totalNumSlides) {
				transform.position = spotPrev;
				Debug.Log ("Force Back COMPLETED");
				spotNow = spotPrev; 
				spotNext = new Vector2 (spotNow.x - shiftAmt, 0f);
				spotPrev = new Vector2 (spotNow.x + shiftAmt, 0f);
				slideNum--;
				Debug.Log ("SlideNum after force back move = " + slideNum);
				forceBackBool = false;
				Debug.Log ("force forward completed");
				dontMove = false;
				//}
		}
}
