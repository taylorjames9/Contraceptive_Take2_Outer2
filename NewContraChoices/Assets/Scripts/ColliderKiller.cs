using UnityEngine;
using System.Collections;

public class ColliderKiller : MonoBehaviour {



		public GameObject sliderTrack;

		void OnMouseDown(){
				foreach(BoxCollider2D c in sliderTrack.GetComponents<BoxCollider2D> ()) {
						c.enabled = false;
				}

		}

}
