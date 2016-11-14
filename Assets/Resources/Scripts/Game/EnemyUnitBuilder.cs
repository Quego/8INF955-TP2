using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemyUnitBuilder : MonoBehaviour {
	public GameObject playerBaseBuilding;
	public GameObject buildFactory;
	public GameObject toBuild;
	public float spawnStartTime;
	public float spawnRepeatTime;

	void Start(){
		if (spawnStartTime == 0)
			spawnStartTime = 10;
		if (spawnRepeatTime == 0)
			spawnRepeatTime = 60;
		//it calls the method build after spawnStartTime seconds and it recalls this methods all spawnRepeatTime seconds
		InvokeRepeating("build", spawnStartTime, spawnRepeatTime);
	}


	//it creates a unit of the buildFactory
	public void build (){
		UnitProperties properties = toBuild.GetComponent<UnitProperties> ();
		BuildingProperties factoryProperties = buildFactory.GetComponent<BuildingProperties> ();
		toBuild.SetActive (true);
		GameObject newUnit = Instantiate (toBuild, GameObject.Find ("EnemyUnits").transform) as GameObject;
		newUnit.transform.position = new Vector3 (buildFactory.transform.position.x-5,1.4f, buildFactory.transform.position.z);
		newUnit.transform.name = toBuild.name;
		EnemyUnitController euc = newUnit.AddComponent<EnemyUnitController> ();
		euc.baseBuilding = playerBaseBuilding;
	}

}
