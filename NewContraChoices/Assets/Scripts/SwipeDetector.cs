﻿using UnityEngine;
using System.Collections;

public class SwipeDetector : MonoBehaviour {

	//for DRAG
	public float min_X;
	public float max_X;
	public string prefsString;
	private float m_Volume = 0.0f;

	float originalWidth = 1280.0f;  // define here the original resolution
	float originalHeight = 800.0f; // you used to create the GUI contents 
	Vector3 scale;
	private bool handleFingerInput = false;

	// Values to set for SWIPE:
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

		//For Drag
		float xpos = min_X + ((max_X - min_X) * m_Volume);
		gameObject.transform.position = new Vector3(xpos, gameObject.transform.position.y, gameObject.transform.position.z);

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
			for(int i = 0; i < Input.touchCount; i++)
			{
				Vector2 inputPosition = Input.touches[i].position;

				if (Input.touches[i].phase == TouchPhase.Began )
				{

					//if(guiTexture.HitTest(inputPosition) == true)
					//{
						handleFingerInput = true;
					//}
				}
				else if((Input.touches[i].phase == TouchPhase.Moved || Input.touches[i].phase == TouchPhase.Stationary) && (handleFingerInput == true))
				{
					float xpos = gameObject.transform.position.x;
					xpos = inputPosition.x /  Screen.width;
					if(xpos <= spotPrev.x - 6.75f)
					{
						xpos = spotPrev.x;
					}
					else if(xpos >= spotNext.x + 6.75f)
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
			//moveForward ();
		}

		if (moveBackwardBool) {
			//moveBackward ();
		}


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
		StartCoroutine("HandleMouseDown");
	}


	IEnumerator HandleMouseDown()
	{
		while(Input.GetMouseButtonUp(0) == false)
		{
			Vector3 inputPosition = Input.mousePosition;
			float xpos = gameObject.transform.position.x;
			xpos = inputPosition.x / Screen.width;
			if(xpos <= spotPrev.x - 6.75f)
			{
				xpos = spotPrev.x;
			}
			else if(xpos >= spotNext.x + 6.75f)
			{
				xpos = spotNext.x;
			}

			gameObject.transform.position = new Vector3(xpos, gameObject.transform.position.y, gameObject.transform.position.z);
			m_Volume = (xpos - min_X) / (max_X - min_X);
			yield return null;
		}
	}
}


