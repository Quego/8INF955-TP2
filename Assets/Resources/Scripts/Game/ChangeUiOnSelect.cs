using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeUiOnSelect : MonoBehaviour {

	public GameObject uiToEnable;
	List<GameObject> uiBuildings;

	void Start(){
		uiBuildings = new List<GameObject> ();
		foreach (Transform trsf in  GameObject.Find ("Building Panels").transform)
			uiBuildings.Add (trsf.gameObject);
	}

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
