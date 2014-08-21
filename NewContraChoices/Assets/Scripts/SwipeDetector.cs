using UnityEngine;
using System.Collections;

public class SwipeDetector : MonoBehaviour {

	//for DRAG
	public float min_X = -1.0f;
	public float max_X = 1.0f;
	public string prefsString;
	private float m_Volume = 0.0f;

	public bool movedEnoughToGotFoward;
	public bool movedEnoughToGotBackward;	
	public bool moveForwardStarted;
	public bool moveBackwardStarted;

	//float originalWidth = 1280.0f;  // define here the original resolution
	//float originalHeight = 800.0f; // you used to create the GUI contents 
	Vector3 scale;
	private bool handleFingerInput = false;

	// for SWIPE:
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

	private float shiftInterval = 13.5f;

	private Vector3 spotNow;
	private Vector3 spotNext;
	private Vector3 spotPrev;

	public int slideNum = 1; 
 	public int totalNumSlides = 3; 

	float originalSpotDownX = 0;

	public bool fingerTouchedDown = false; 
	public float colliderZ = -1.0f;

	public bool slideChange;


	void OnAwake(){
		spotPrev = new Vector3 (spotNow.x + shiftInterval, 0f, 0f);
		spotNow = new Vector3 (0.0f, 0f, 0f);
		spotNext = new Vector3 (spotNow.x - shiftInterval, 0f, 0f);

		//For Drag
		float xpos = min_X + ((max_X - min_X) * m_Volume);
		gameObject.transform.position = new Vector3(xpos, gameObject.transform.position.y, gameObject.transform.position.z);

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
								spotNext = new Vector3 (spotNow.x - shiftInterval, 0f, colliderZ);
								spotPrev = new Vector3 (spotNow.x + shiftInterval, 0f, colliderZ);
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
								spotNext = new Vector3 (spotNow.x - shiftInterval, 0f, colliderZ);
								spotPrev = new Vector3 (spotNow.x + shiftInterval, 0f, colliderZ);
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
									spotNext = new Vector3 (spotNow.x - shiftInterval, 0f, colliderZ);
									spotPrev = new Vector3 (spotNow.x + shiftInterval, 0f, colliderZ);
									moveBackwardBool = false;
						}
				}
		}
		}


	void  Update(){
		print ("SlideNUm = " +slideNum);
		//Debug.Log ("TotalNumofSlides " +totalNumSlides);

		//Drag stuff
		/*if(gameObject.transform.position.x <= min_X)
		{
			gameObject.transform.position = new Vector3(min_X, gameObject.transform.position.y, gameObject.transform.position.z);
		}
		else if(gameObject.transform.position.x >= max_X)
		{
			gameObject.transform.position = new Vector3(max_X, gameObject.transform.position.y, gameObject.transform.position.z);
		}*/

		if(Input.touchCount > 0)
		{
			//print("Inside Update and touchcount>0");
			for(int i = 0; i < Input.touchCount; i++)
			{
				Vector2 inputPosition = Input.touches[i].position;
				float xpos = transform.position.x;
				xpos = inputPosition.x;
				//print ("xpos = "+xpos);

				if (Input.touches[i].phase == TouchPhase.Began )
				{

					//if(guiTexture.HitTest(inputPosition) == true)
					//{
					handleFingerInput = true;
					print ("TouchPhase = BEGAN");
					//}
				}
				else if((Input.touches[i].phase == TouchPhase.Moved || Input.touches[i].phase == TouchPhase.Stationary) && (handleFingerInput == true))
				{



					if(xpos <= spotPrev.x - 1.0f)
					{
						xpos = spotPrev.x;
					}
					else if(xpos >= spotNext.x + 1.0f)
					{
						xpos = spotNext.x;
					}

					gameObject.transform.position = new Vector3(xpos, gameObject.transform.position.y, gameObject.transform.position.z);

					m_Volume = (xpos - min_X) / (max_X - min_X);

					/*if(PlayerPrefs.GetFloat(prefsString) != m_Volume)
					{
						PlayerPrefs.SetFloat(prefsString, m_Volume);
						PlayerPrefs.Save();
					}*/
				}
				else
				{
					handleFingerInput = false;
				}


			}
		}

		//Swipe stuff

		if (moveForwardBool) {
			moveForward ();
		}

		if (moveBackwardBool) {
			moveBackward ();
		}

		//this is all the swipe to move code
		/*if (Input.touchCount > 0) {
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
							moveForwardBool = true;

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
		}*/
	}
		

	/*void OnMouseOver(){
		if (Input.GetMouseButtonDown (0)) {
			moveForwardBool = true;
			print ("Mouse Left Went Down");

		}

		else if (Input.GetMouseButtonDown(1)){
			moveBackwardBool = true;
			print ("Mouse Right Went Down");

		}
	}*/

	/***Use for TESTING PURPOSESS******//////
	void OnMouseDown()
	{
		//print("MouseOver is true");
		StartCoroutine("HandleMouseDown");
	}


	IEnumerator HandleMouseDown()
	{
		float xpos = gameObject.transform.position.x;
		while(Input.GetMouseButtonUp(0)==false)
		{
			//print("MouseDown is true");
			//On finger down
			Vector3 inputPosition = Input.mousePosition;
			float inputXNormalized = ((inputPosition.x) / (Screen.width));
			//print ("INputXNormal = " + inputXNormalized);
			//xpos = spotNow.x;
			xpos = inputXNormalized + spotNow.x;

			if (!fingerTouchedDown) {
				originalSpotDownX = inputXNormalized;
				print ("Record Original FingerTouchDown Position " + originalSpotDownX);
				fingerTouchedDown = true;
			}

			//On finger drag MOVE
			if (xpos != originalSpotDownX) {
				xpos = inputXNormalized + spotNow.x;
				gameObject.transform.position = new Vector3 (xpos, 0f, 0f);
			}
			yield return null;

		}

		//On Release 
		//print ("Original Spot Position "+originalSpotDownX);
		//print ("xpos " + xpos);
		//print ("SpotNow.x - Original Spot Position "+(spotNow.x + originalSpotDownX));
		fingerTouchedDown = false;
		if(xpos <= spotNow.x +  originalSpotDownX)
		{
			print ("Should go to next spot");
			moveForwardBool = true;
						//movedEnoughToGotFoward
		}

		else if(xpos >= spotNow.x + originalSpotDownX)
		{
			print ("Should go to prev spot");
			moveBackwardBool = true;
		}
	}
}


