using UnityEngine;
using System.Collections;

public class SmoothPageSwipe : MonoBehaviour {


	bool moveForwardBool = false;
	bool moveBackwardBool = false; 

	private float shiftInterval = 13.5f;

	private Vector2 spotNow = Vector3.zero;
	private Vector2 spotNext;
	private Vector2 spotPrev;

	public int slideNum = 1; 
	public int totalNumSlides; 

	//float originalSpotDownX = 0;

	bool moveForwardStarted = false;
	bool moveBackwardStarted = false;

	public bool fingerTouchedDown = false; 

		void Start(){
				spotNext = new Vector2 (spotNow.x + shiftInterval, 0f);
				spotPrev = new Vector2 (spotNow.x - shiftInterval, 0f);

		}

	
	// Update is called once per frame
	void Update () {

				if (Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Moved) {
						Vector2 touchDelta  = Input.GetTouch(0).deltaPosition;
						//this next line might be a problem because when your finger goes down, the slide might shift to center itself at that location.
						Vector3 forTranslate = new Vector2 (touchDelta.x, 0);
						transform.Translate (forTranslate);
				}
				if (Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Ended) {
						Vector2 touchDelta  = Input.GetTouch(0).deltaPosition;
						//This is a flick to the left with magnitude of 5 or more
						if (touchDelta.x>5) {

								moveForwardBool = true; 

						} 

						//This is a flick to the right with magnitude of 5 or more
						if (touchDelta.x<-5) {
								moveBackwardBool = true; 
						} 
				}

				if (moveForwardBool) {
						moveForward ();
				}

				if (moveBackwardBool) {
						moveBackward ();
				}
	}

		void moveBackward(){
				print("Move backward is going");
				if (slideNum <= 1) {
						spotPrev = spotNow;
						slideNum = 1;
						if (transform.position.x < spotPrev.x - 0.1f) {
								//print ("Move Backwards functionsays we should move backwards one");
								transform.position = Vector3.Lerp (transform.position, spotPrev, 10 * Time.deltaTime);
						} 
						if (transform.position.x >= spotPrev.x - 0.1f) {
								transform.position = spotPrev;
								spotNow = spotPrev;
								spotNext = new Vector3 (spotNow.x - shiftInterval, 0f, 0f);
								spotPrev = new Vector3 (spotNow.x + shiftInterval, 0f, 0f);
								moveBackwardBool = false;

						}
				} else if (slideNum > 1) {
						if (transform.position.x < spotPrev.x - 0.1f) {
								transform.position = Vector3.Lerp (transform.position, spotPrev, 10 * Time.deltaTime);
								moveBackwardStarted = true;
								print ("move back started");
						} 
						if (transform.position.x >= spotPrev.x - 0.1f && moveBackwardStarted) {
								transform.position = spotPrev;
								print ("Move back completed");
								spotNow = spotPrev;
								spotNext = new Vector3 (spotNow.x - shiftInterval, 0f, 0f);
								spotPrev = new Vector3 (spotNow.x + shiftInterval, 0f, 0f);
								moveBackwardBool = false;
								if (slideNum > 1) {	
										slideNum--;
								}
								moveBackwardStarted = false;
						} else {
								if (transform.position.x >= spotPrev.x - 0.1f){
										transform.position = spotPrev;
										print ("Move back completed");
										spotNow = spotPrev;
										spotNext = new Vector3 (spotNow.x - shiftInterval, 0f, 0f);
										spotPrev = new Vector3 (spotNow.x + shiftInterval, 0f, 0f);
										moveBackwardBool = false;
								}
						}
				}


		}

		void moveForward(){
				print ("Moveforward is going");
				print ("SlideNum " + slideNum + " .totalNumSlide is" + totalNumSlides);
				if (slideNum >= totalNumSlides) {
						spotNext = spotNow;
						slideNum = totalNumSlides;
						if (transform.position.x > spotNext.x + 0.1f) {
								transform.position = Vector3.Lerp (transform.position, spotNext, 10 * Time.deltaTime);

						}
						if (transform.position.x <= spotNext.x + 0.1f) {
								transform.position = spotNext;
								spotNow = spotNext; 
								spotNext = new Vector3 (spotNow.x - shiftInterval, 0f, 0f);
								spotPrev = new Vector3 (spotNow.x + shiftInterval, 0f, 0f);
								moveForwardBool = false;
						}
				} else if (slideNum < totalNumSlides) {

						if (transform.position.x > spotNext.x + 0.1f) {
								transform.position = Vector3.Lerp (transform.position, spotNext, 10 * Time.deltaTime);
								print ("MOVE STARTED");
								moveForwardStarted = true; 

						}
						if (transform.position.x <= spotNext.x + 0.1f && moveForwardStarted) {

								transform.position = spotNext;
								print ("MOVE COMPLETED");
								spotNow = spotNext; 
								spotNext = new Vector3 (spotNow.x - shiftInterval, 0f, 0f);
								spotPrev = new Vector3 (spotNow.x + shiftInterval, 0f, 0f);
								moveForwardBool = false;
								if (slideNum < totalNumSlides) {
										slideNum++;
								}
								moveForwardStarted = false;
						} else {
								if (transform.position.x <= spotNext.x + 0.1f) {
										transform.position = spotNext;
										print ("MOVE COMPLETED");
										spotNow = spotNext; 
										spotNext = new Vector3 (spotNow.x - shiftInterval, 0f, 0f);
										spotPrev = new Vector3 (spotNow.x + shiftInterval, 0f, 0f);
										moveForwardBool = false;
								}
						}
				}

		}

		//helper function
		//void moveSuccessfullyForwardOrBack(  /*bool forward or moveBackward*/ , /*Arithmetic operator enum*/ ){

		//}
		//helper function 
		//void hitBuffer( ){

		//}

}
