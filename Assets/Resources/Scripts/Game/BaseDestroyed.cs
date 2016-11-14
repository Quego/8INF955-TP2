using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

/// <summary>
/// Handle actions when a base is destroyed
/// </summary>
public class BaseDestroyed : MonoBehaviour {
	public GameObject baseBuilding;
	public GameObject popup;

	private int destroyPopUpAfterSeconds = 3;

	void Start(){
		//we hide the ending popup
		popup.SetActive (false);
	}


	void Update(){
		checkBaseDestroyed ();
	}

	/// <summary>
	/// Checks if the base destroyed.If so, show win or lose popup
	/// </summary>
	private void checkBaseDestroyed()
	{
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