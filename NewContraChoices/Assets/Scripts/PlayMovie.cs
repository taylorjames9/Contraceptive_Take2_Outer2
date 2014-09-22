using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour {

		void Start(){


		}

		int mouseCounter;

		void OnMouseDown(){
				Handheld.PlayFullScreenMovie ("Patrick_MovTest.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
				Debug.Log ("Should be playing movie now");
		}
}
