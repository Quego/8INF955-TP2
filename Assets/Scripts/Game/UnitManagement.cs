﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnitManagement : MonoBehaviour {

	public GameObject popup;
	public GameObject buildFactory;
	public float destroyPopUpAfterSeconds;
	public float factoryLevelPercentageReducer;

	//TODO remove comments on null check 

	void Start(){
		//if not defined in inspector, define here
		if (destroyPopUpAfterSeconds == 0)
			destroyPopUpAfterSeconds = 5;
		
		if (factoryLevelPercentageReducer == 0)
			factoryLevelPercentageReducer = 10;

	}

	public void build (GameObject toBuild){

		UnitProperties properties = toBuild.GetComponent<UnitProperties> ();
		BuildingProperties factoryProperties = buildFactory.GetComponent<BuildingProperties> ();
		//if (properties != null && factoryProperties != null) {

			//calculate reduced cost according to factory level
			int lvl = factoryProperties.level;
			int f = costReduced (properties.foodCost, lvl);
			int w = costReduced (properties.woodCost, lvl);
			int i = costReduced (properties.ironCost, lvl);
			//check if there are enough resources
			if (buildSuccess (f, w, i, properties.unitCost)) {
				toBuild.SetActive (true);

				GameObject newUnit = Instantiate (toBuild, GameObject.Find ("PlayerUnits").transform) as GameObject;
				newUnit.transform.position = new Vector3 (buildFactory.transform.position.x-5,1.4f, buildFactory.transform.position.z);
				newUnit.transform.name = toBuild.name;
				Camera.main.transform.position = new Vector3 (newUnit.transform.position.x, Camera.main.transform.position.y, newUnit.transform.position.z - Camera.main.transform.position.y);
			}
			//else print error message
			else {
				createPopUp (toBuild.name, f, w, i, properties.unitCost);
			}
		//}
	}

	private int costReduced(int cost, int factoryLevel)
	{
		return cost - (int)((factoryLevel-1) * cost* (factoryLevelPercentageReducer / 100f));
	}


	private void createPopUp(string name,int foodCost,int woodCost, int ironCost,int unitCost){

		foreach (GameObject toDestroy in GameObject.FindGameObjectsWithTag("Popup"))
			DestroyImmediate (toDestroy);
		
		GameObject popupGO = Instantiate (popup, GameObject.Find ("Canvas").transform) as GameObject;
		popupGO.transform.localPosition = Vector3.zero;
		popupGO.transform.localScale = Vector3.one;
		string textToSet = "Can't create unit " + name + ", need :";
		if (PlayerData.food - foodCost < 0)
			textToSet += "\n" + foodCost + " Food";
		if (PlayerData.wood - woodCost < 0)
			textToSet += "\n" + woodCost + " Wood";
		if (PlayerData.iron - ironCost < 0)
			textToSet += "\n" + ironCost + " Iron";
		if (PlayerData.maxUnits - PlayerData.units - unitCost < 0)
			textToSet += "\n" + unitCost + " Iron";

		Destroy (popupGO, destroyPopUpAfterSeconds);

		popupGO.GetComponentInChildren<Text> ().text = textToSet;
	}

	private bool buildSuccess(int food,int wood, int iron,int unit){
		if (PlayerData.food - food >= 0 && PlayerData.iron - iron >= 0 && PlayerData.wood - wood >= 0 && PlayerData.maxUnits - PlayerData.units - unit >= 0) {
			PlayerData.food -= food;
			PlayerData.wood -= wood;
			PlayerData.iron -= iron;
			PlayerData.units += unit;
			PlayerData.updateResourceView ();
			return true;
		}
		return false;
	}
}
