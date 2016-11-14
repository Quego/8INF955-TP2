using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Change user interface according to selected building.
/// </summary>
public class ChangeUiOnSelect : MonoBehaviour {

	public GameObject uiToEnable;
	List<GameObject> uiBuildings;

	void Start(){
		//get all Building ui
		uiBuildings = new List<GameObject> ();
		foreach (Transform trsf in  GameObject.Find ("Building Panels").transform)
			uiBuildings.Add (trsf.gameObject);
	}

	/// <summary>
	/// Applies UI for the selected building.
	/// </summary>
	public void applySelectReaction(){
		if (uiBuildings != null)
			foreach (GameObject go in uiBuildings) {
				if (uiToEnable == go)
					go.SetActive (true);
				else
					go.SetActive (false);	
			}
	}
}
