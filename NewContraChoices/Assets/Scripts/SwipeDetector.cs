using UnityEngine;
using System.Collections;

public class SwipeDetector : MonoBehaviour {

	// Values to set:
	public float comfortZone = 70.0f;
	public float minSwipeDist = 14.0f;
	public float maxSwipeTime = 0.5f;

	private float startTime;
	private Vector2 startPos;
	private bool couldBeSwipe;

	public enum SwipeDirection {
		None,
		Left,
		Right
	}

	public SwipeDirection lastSwipe = SwipeDetector.SwipeDirection.None;
	public float lastSwipeTime;
	private bool moveForwardBool;
	private bool moveBackwardBool;
	/////////private int shiftNum = 0;
	//private float shiftAmtForward = -13.5f;
	//private float shiftAmtBackward = 13.5f;
	private float shiftInterval = 13.5f;
	//private Vector3 shiftInterval = new Vector3(-13.5f,0f,0f);
	//private Vector3 backwardShiftInterval = new Vector3(13.5f,0f,0f);

	private Vector3 spotNow;
	private Vector3 spotNext;
	private Vector3 spotPrev;

	//private float currentX = 0;


	void OnAwake(){
		spotPrev = new Vector3 (spotNow.x + shiftInterval, 0f, 0f);
		spotNow = new Vector3 (spotNow.x, 0f, 0f);
		spotNext = new Vector3 (spotNow.x - shiftInterval, 0f, 0f);
	}


	void moveForward(){

		if (transform.position.x > spotNext.x + 0.1f) {
			print ("should be moving forward one");
			transform.position = Vector3.Lerp (transform.position, spotNext, 10*Time.deltaTime);

		} 
		if (transform.position.x <= spotNext.x + 0.1f) {
			transform.position = spotNext;
			spotNow = spotNext; 
			spotNext = new Vector3 (spotNow.x - shiftInterval, 0f, 0f);
			spotPrev = new Vector3 (spotNow.x + shiftInterval, 0f, 0f);
			moveForwardBool = false;
			Debug.Log (transform.position);
		}
	}

	void moveBackward(){
		if (transform.position.x < spotPrev.x - 0.1f) {
			print ("should be moving backwards one");
			transform.position = Vector3.Lerp (transform.position, spotPrev, 10*Time.deltaTime);
		} 
		if (transform.position.x >= spotPrev.x -0.1f) {
			transform.position = spotPrev;
			spotNow = spotPrev;
			spotNext = new Vector3 (spotNow.x - shiftInterval, 0f, 0f);
			spotPrev = new Vector3 (spotNow.x + shiftInterval, 0f, 0f);
			moveBackwardBool = false;
			Debug.Log (transform.position);
		}
	}


	void  Update(){
		if (moveForwardBool) {
			moveForward ();
		}

		if (moveBackwardBool) {
			moveBackward ();
		}


		if (Input.touchCount > 0) {
			Touch touch = Input.touches[0];

			switch (touch.phase){
			case TouchPhase.Began:
				lastSwipe = SwipeDetector.SwipeDirection.None;
				lastSwipeTime = 0;
				couldBeSwipe = true;
				startPos = touch.position;
				startTime = Time.time;
				break;

			case TouchPhase.Moved:
				if (Mathf.Abs(touch.position.y - startPos.y) > comfortZone)
				{
					Debug.Log("Not a swipe. Swipe strayed " + (int)Mathf.Abs(touch.position.y - startPos.y) +
						"px which is " + (int)(Mathf.Abs(touch.position.y - startPos.y) - comfortZone) +
						"px outside the comfort zone.");
					couldBeSwipe = false;
				}
				break;
			case TouchPhase.Ended:
				if (couldBeSwipe) {
					float swipeTime = Time.time - startTime;
					float swipeDist = (new Vector3(0, touch.position.x, 0) - new Vector3(0, startPos.x, 0)).magnitude;

					if ((swipeTime < maxSwipeTime) && (swipeDist > minSwipeDist)){
						// It's a swiiiiiiiiiiiipe!
						float swipeValue = Mathf.Sign(touch.position.x - startPos.x);
						//Vector2 offStageSpot = new Vector2 (-14f, 0);

						// If the swipe direction is positive, it was an upward swipe.
						// If the swipe direction is negative, it was a downward swipe.
						if (swipeValue < 0) {
							lastSwipe = SwipeDetector.SwipeDirection.Left;
							Debug.Log("This was a <---- swipe");
							//moveForwardBool = true;

						} else if (swipeValue > 0) {
							lastSwipe = SwipeDetector.SwipeDirection.Right;
							Debug.Log("This was a ----> swipe");
							//moveBackwardBool = true;
						}

						// Set the time the last swipe occured, useful for other scripts to check:
						lastSwipeTime = Time.time;
						Debug.Log("Found a swipe!  Direction: " + lastSwipe);
					}
				}
				break;
			}
		}
	}
		

	void OnMouseOver(){
		if (Input.GetMouseButtonDown (0)) {
			moveForwardBool = true;
			print ("Mouse Left Went Down");

		}

		else if (Input.GetMouseButtonDown(1)){
			moveBackwardBool = true;
			print ("Mouse Right Went Down");

		}
	}
}


