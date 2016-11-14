using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class BaseDestroyed : MonoBehaviour {
	public GameObject baseBuilding;
	public GameObject popup;

	private int destroyPopUpAfterSeconds = 3;

	void Start(){
		//we hide the ending popup
		popup.SetActive (false);
	}


	void Update(){
		//if the life of the base reaches 0, the game ends
		if (baseBuilding.GetComponent<BuildingProperties> ().life <= 0) {
			//we delete all popups
			foreach (GameObject toDestroy in GameObject.FindGameObjectsWithTag("Popup"))
				DestroyImmediate (toDestroy);


			//if it is the player's base, we show a red popup with the message "You lose !"
			if (baseBuilding.CompareTag ("FriendlyBuilding")) {
				popup.GetComponent<Image> ().color = Color.red;
				popup.GetComponentInChildren<Text> ().text = "You lose ! ";
			} 
			//else it is the enemy's base, so we show a green popup with the message "You Win !"
			else {
				popup.GetComponent<Image> ().color = Color.green;
				popup.GetComponentInChildren<Text> ().text = "You win ! ";
			}
			popup.SetActive (true);

			//we destroy it after few seconds to load the "Main Menu"'s scene
			Destroy (popup, destroyPopUpAfterSeconds);
		} 
	}


}