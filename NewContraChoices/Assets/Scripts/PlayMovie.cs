using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour {

		void Start(){


		}

		int mouseCounter;

		void OnMouseDown(){
				Handheld.PlayFullScreenMovie ("Patrick_MovTest.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
				Debug.Log ("Should be playing movie now");
				//MovieTexture movie = renderer.material.mainTexture as MovieTexture;
				//mouseCounter++;
				//if (mouseCounter % 2 == 1) {
						//movie.Play ();
						//audio.Play ();
						/////Handheld.PlayFullScreenMovie ("Patrick_MovTest.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
						//////Debug.Log ("Should be playing movie now");
						//} else if (mouseCounter % 2 == 0) {
						//movie.Stop ();
						//audio.Stop ();
						//}
				//}
		}
}
