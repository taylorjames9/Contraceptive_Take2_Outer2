using UnityEngine;
using System.Collections;

public class tapOptionsScript : MonoBehaviour {


		public GameObject myCompatriot;
		Vector3 adjacentSpot = new Vector3(13.5f,0f,0f);
		Vector3 spotBack = new Vector3(-13.5f,0f,0f);
		public GameObject[] cardArray;
		string compatriotName;

		void Start(){
			compatriotName = myCompatriot.name;
				Debug.Log ("compatriot name " + myCompatriot.name);
		}


		void OnMouseDown(){
				Debug.Log ("Hit Me");
				myCompatriot.SetActive (true);
				myCompatriot.transform.position = adjacentSpot;
				this.transform.parent.transform.parent.transform.position = spotBack;
				foreach (GameObject card in cardArray) {
						if(!string.Equals(card.name, compatriotName)){
								card.SetActive (false);
						}
				}
		}
}
