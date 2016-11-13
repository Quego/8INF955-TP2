using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class BaseDestroyed : MonoBehaviour {
	public GameObject baseBuilding;
	public GameObject popup;

	private int destroyPopUpAfterSeconds = 3;

	void Start(){
		popup.SetActive (false);
	}


	void Update(){
		if (baseBuilding.GetComponent<BuildingProperties> ().life <= 0) {
			foreach (GameObject toDestroy in GameObject.FindGameObjectsWithTag("Popup"))
				DestroyImmediate (toDestroy);



			if (baseBuilding.CompareTag ("FriendlyBuilding")) {
				popup.GetComponent<Image> ().color = Color.red;
				popup.GetComponentInChildren<Text> ().text = "You lose ! ";
			} else {
				popup.GetComponent<Image> ().color = Color.green;
				popup.GetComponentInChildren<Text> ().text = "You win ! ";
			}
			popup.SetActive (true);
			Destroy (popup, destroyPopUpAfterSeconds);
		} 
	}


}