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
	private int shiftNum = 0;
	private float shiftAmtForward = -13.5f;
	private float shiftAmtBackward = 13.5f;
	private float shiftSize;
	private Vector3 shiftInterval = new Vector3(-13.5f,0f,0f);
	private Vector3 backwardShiftInterval = new Vector3(13.5f,0f,0f);


	void OnAwake(){
	
	}


	void moveForward(){

		shiftSize = shiftAmtForward * shiftNum;
		shiftInterval = new Vector3 (shiftNum * shiftAmtForward, 0, 0);
		if (transform.position.x > shiftSize + 0.1f) {
			//transform.Translate (-20 * Time.deltaTime, 0f, 0f);
			print ("should be moving forward one");
			transform.position = Vector3.Lerp (transform.position, shiftInterval, 10*Time.deltaTime);
		} 
		if (transform.position.x <= shiftSize + 0.1f) {
			shiftNum++;
			moveForwardBool = false;
			Debug.Log (shiftSize);
		}
	}

	void moveBackward(){
		shiftSize = shiftAmtBackward * shiftNum;
		backwardShiftInterval = new Vector3 (shiftNum * shiftAmtBackward, 0, 0);
		if (transform.position.x < shiftSize - 0.1f) {
			print ("should be moving backwards one");
			transform.position = Vector3.Lerp (transform.position, backwardShiftInterval, 10*Time.deltaTime);
		} 
		if (transform.position.x >= shiftSize - 0.1f) {
			moveBackwardBool = false;
			Debug.Log (shiftSize);
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
						Vector2 offStageSpot = new Vector2 (-14f, 0);

						// If the swipe direction is positive, it was an upward swipe.
						// If the swipe direction is negative, it was a downward swipe.
						if (swipeValue < 0) {
							lastSwipe = SwipeDetector.SwipeDirection.Left;
							Debug.Log("This was a <---- swipe");
							moveForwardBool = true;
							//while (this.transform.position.x > -13.0f) {
							//Vector2.Lerp (transform.position, new Vector2 (0.1f, this.transform.position.y), Time.time);
							//transform.position = new Vector2(-0.1f, this.transform.position.y);
							//StartCoroutine ("movingSlideOut");
							//}
						} else if (swipeValue > 0) {
							lastSwipe = SwipeDetector.SwipeDirection.Right;
							Debug.Log("This was a ----> swipe");
							moveBackwardBool = true;
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

	void OnMouseUp(){
		//StartCoroutine (Movement ());
		moveBackwardBool = true;
		//moveForwardBool = true;
		shiftNum--;
		print ("MouseWentDown");
	}

	/*IEnumerator Movement(){
				Vector2 target = new Vector2 (-14.0f, 0f);
				float speed = 0f;
				//while (Vector2.Distance (transform.position, target) > 0.1f) {
				//float step = speed * Time.deltaTime;
						//transform.position = Vector2.MoveTowards(transform.position, target, Time.time/1000);
						transform.Translate (Time.deltaTime, 0f, 0f);
				//}
				yield return new WaitForSeconds(0.0f);

		}*/
}
