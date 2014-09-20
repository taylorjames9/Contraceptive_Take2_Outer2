using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour {



		int mouseCounter;

		void OnMouseDown(){
				MovieTexture movie = renderer.material.mainTexture as MovieTexture;
				mouseCounter++;
				if (mouseCounter % 2 == 1) {
						movie.Play ();
						audio.Play ();
				} else if (mouseCounter % 2 == 0) {
						movie.Stop ();
						audio.Stop ();
				}
		}
}
