using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Manage Building actions.
/// </summary>
public class BuildingManagement : MonoBehaviour {

	public GameObject popup;
	public float destroyPopUpAfterSeconds;
	public int percentageBackWhenDestroy;


	void Start(){
		//if not defined in inspector, initialise variables here
		if (destroyPopUpAfterSeconds == 0)
			destroyPopUpAfterSeconds = 5;
		
		if (percentageBackWhenDestroy == 0)
			percentageBackWhenDestroy = 10;
	}

	/// <summary>
	/// Build the specified building.
	/// </summary>
	/// <param name="toBuild">The building to build</param>
	public void build (GameObject toBuild){
		BuildingProperties properties = toBuild.GetComponent<BuildingProperties> ();
		if (properties != null) {
			//check if there are enough resources
			if (!toBuild.gameObject.activeSelf) {
				if (buildSuccess (properties.foodCost, properties.woodCost, properties.ironCost)) {
					toBuild.SetActive (true);
				}
			//else print error message
			else {
					createPopUp ("build",toBuild.name, properties.foodCost, properties.woodCost, properties.ironCost);
				}
			}
			//Focus on it if it already exists
			if (toBuild.gameObject.activeSelf) {
				Camera.main.transform.position = new Vector3 (toBuild.transform.position.x, Camera.main.transform.position.y, toBuild.transform.position.z - Camera.main.transform.position.y);
				Camera.main.transform.GetComponentInChildren<InputController> ().selectObject (toBuild);
			} 
		}
	}

	/// <summary>
	/// Upgrade the specified building.
	/// </summary>
	/// <param name="toUpgrade">the building to be updgraded</param>
	public void upgrade (GameObject toUpgrade){
		BuildingProperties properties = toUpgrade.GetComponent<BuildingProperties> ();
		int nextLevel = properties.level + 1;
		if (properties != null) {
			
			if (toUpgrade.gameObject.activeSelf && buildSuccess (nextLevel * properties.foodCost,nextLevel * properties.woodCost, nextLevel * properties.ironCost)) {
				properties.level++;
				//make it bigger
				toUpgrade.transform.localScale += Vector3.one * 0.2f;
			}
			else
				createPopUp ("upgrade",toUpgrade.name, nextLevel * properties.foodCost,nextLevel * properties.woodCost, nextLevel * properties.ironCost);
		}
	}

	/// <summary>
	/// Destroy the specified building.
	/// </summary>
	/// <param name="toDestroy">The building to be destroyed</param>
	public void destroy (GameObject toDestroy){
		//find building requirements
		BuildingProperties properties = toDestroy.GetComponent<BuildingProperties> ();
		if (properties != null && properties.selfDestroyable) {
			//change selection if needed
			InputController inputController = Camera.main.transform.GetComponentInChildren<InputController> ();
			if (gameObject == inputController.selectedGO)
				inputController.selectObject (null);

			//refund player for X purcent of each building level
			for (int i = 1; i <= properties.level; i++) {
				PlayerData.food +=(int)(i * properties.foodCost * (percentageBackWhenDestroy / 100f));
				PlayerData.wood +=(int)(i * properties.woodCost * (percentageBackWhenDestroy / 100f));
				PlayerData.iron +=(int)(i * properties.ironCost * (percentageBackWhenDestroy / 100f));
			}
			PlayerData.updateResourceView ();
			//destroy GO
			properties.level=1;
			toDestroy.SetActive(false);
			Camera.main.transform.GetComponentInChildren<InputController> ().selectObject (null);
		}
	}

	/// <summary>
	/// Creates a pop up giving missing resources to build a building.
	/// </summary>
	/// <param name="changeType">"Upgrade" or "Create"</param>
	/// <param name="name">Name of the building</param>
	/// <param name="foodCost">Building Food cost.</param>
	/// <param name="woodCost">Building Wood cost.</param>
	/// <param name="ironCost">Building Iron cost.</param>
	private void createPopUp(string changeType,string name,int foodCost,int woodCost, int ironCost){

		//Remove existing popups
		foreach (GameObject toDestroy in GameObject.FindGameObjectsWithTag("Popup"))
			DestroyImmediate (toDestroy);

		//create new one an initialize attributes
		GameObject popupGO = Instantiate (popup, GameObject.Find ("Canvas").transform) as GameObject;
		popupGO.transform.localPosition = Vector3.zero;
		popupGO.transform.localScale = Vector3.one;
		string textToSet = "Can't " + changeType + " " + name + ", need :";
		if (PlayerData.food - foodCost < 0)
			textToSet += "\n" + foodCost + " Food";
		if (PlayerData.wood - woodCost < 0)
			textToSet += "\n" + woodCost + " Wood";
		if (PlayerData.iron - ironCost < 0)
			textToSet += "\n" + ironCost + " Iron";

		Destroy (popupGO, destroyPopUpAfterSeconds);

		popupGO.GetComponentInChildren<Text> ().text = textToSet;
	}


	/// <summary>
	/// Try to build a building
	/// </summary>
	/// <returns><c>true</c>, if success was built, <c>false</c> otherwise.</returns>
	/// <param name="food">Building Food cost</param>
	/// <param name="wood">Building Wood cost</param>
	/// <param name="iron">Building Iron cost</param>
	private bool buildSuccess(int food,int wood, int iron){
		if (PlayerData.food - food >= 0 && PlayerData.iron - iron >= 0 && PlayerData.wood - wood >= 0) {
			//If it worked, take resources from player
			PlayerData.food -= food;
			PlayerData.wood -= wood;
			PlayerData.iron -= iron;
			PlayerData.updateResourceView ();
			return true;
		}
		return false;
	}
		
}
