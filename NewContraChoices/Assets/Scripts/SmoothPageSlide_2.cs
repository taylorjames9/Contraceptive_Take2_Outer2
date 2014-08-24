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

		float fingerDragMagnitude;

		public bool fingerTouchedDown = false; 

		float originalSpotDownX = 0;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
				//Debug.Log ("SlideNUm = " + slideNum);

				if (Input.touchCount > 0) {
						//xPos = gameObject.transform.position.x;
						for (int i = 0; i < Input.touchCount; i++) {

								//float xPos = gameObject.transform.position.x;
								Vector2 inputPosition = Input.touches[i].position;

								if (Input.touches [i].phase == TouchPhase.Began) {

										handleFingerInput = true;
										if (!fingerTouchedDown) {
												originalSpotDownX = inputPosition.x / Screen.width;
												print ("Record Original FingerTouchDown Position " + originalSpotDownX);
												fingerTouchedDown = true;
										}


								} else if ((Input.touches [i].phase == TouchPhase.Moved || Input.touches [i].phase == TouchPhase.Stationary) && handleFingerInput) {
										Vector2 touchDelta = Input.GetTouch (0).deltaPosition;
										//float xPos = (slideNum * shiftAmt) + (originalSpotDownX + (inputPosition.x/Screen.width) -1.0f);
										float xPos = (slideNum * shiftAmt) + (inputPosition.x/Screen.width - originalSpotDownX);

										Debug.Log ("xPos " + xPos);
										transform.position = new Vector2 (xPos, 0f);
										fingerDragMagnitude = touchDelta.x;
								} else if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended && handleFingerInput) {
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
				} else
						returnToCurrentSpot ();
		}

		void moveForward(){
				//Debug.Log ("Prompt to move forward");
		
		}

		void moveBack(){
				//Debug.Log("Prompt to move back");

		}

		void returnToCurrentSpot(){
				//Debug.Log("Prompt to return to current location");
		}
}
