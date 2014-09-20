using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour {

	void Start(){

				MovieTexture movie = renderer.material.mainTexture as MovieTexture;
				movie.Play ();
	}
}
