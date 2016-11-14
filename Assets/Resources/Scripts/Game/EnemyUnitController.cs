using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Enemy unit behaviour.
/// </summary>
public class EnemyUnitController : MonoBehaviour {
	public GameObject baseBuilding;
	public Vector3 destination;

	public GameObject targetGO;
	private UnitProperties properties;
	private float speed;
	public bool hasDestination;
	private int maximumNumberPlayerUnitsToAttackPlayerBase = 5; 
	private FighterController fighter;
	private GameObject[] targetList;
	private UnitProperties tragetProp;
	private BuildingProperties tragetBuildingProp;
	private UnitProperties fighterProp;
	private float nextActionTime;


	void Start () {
		//initialise variables
		properties = transform.GetComponent<UnitProperties> ();
		speed = properties.speed;
		destination = transform.position;
		hasDestination = false;
		targetList = GameObject.FindGameObjectsWithTag ("FriendlyUnit");
		fighterProp = transform.GetComponent<UnitProperties> ();
		nextActionTime = Time.time + fighterProp.actionSpeed;
	}

	void Update () {
		moveToTarget ();
	}

	/// <summary>
	/// Makes the unit move target, if there's no target, give one
	/// </summary>
	private void moveToTarget()
	{
		if (hasDestination) {
			
			transform.LookAt (destination);
			transform.position += transform.forward * Time.smoothDeltaTime * speed;

			//Arrive on target
			if (targetGO != null) {
				if (targetGO.tag.Equals ("FriendlyUnit")) {
					if ((transform.position - destination).magnitude <= 0.5f) {
						actionOnArrive ();
					}
				} else if (targetGO.tag.Equals ("FriendlyBuilding")) {
					if ((transform.position - destination).magnitude <= 1.0f) {
						actionOnArrive ();
					}
				}
			}
		} 
		else {
			targetList = GameObject.FindGameObjectsWithTag ("FriendlyUnit");
			if (targetList.Length > maximumNumberPlayerUnitsToAttackPlayerBase) {
				targetGO = targetList [Random.Range (0, targetList.Length)];
			} else {
				targetGO = baseBuilding;
			}
			tragetProp = targetGO.GetComponent<UnitProperties> ();
			tragetBuildingProp = targetGO.GetComponent<BuildingProperties> ();
			hasDestination = true;
		}
		if (targetGO != null) {
			if (targetGO.tag.Equals ("FriendlyBuilding"))
				destination = targetGO.transform.position + new Vector3 (4, 0, 0);
			else
				if ((transform.position - targetGO.transform.position + new Vector3 (1, 0, 0)).magnitude > 1f) 
					destination = targetGO.transform.position + new Vector3 (1, 0, 0);
		} else {
			hasDestination = false;
		}
	}

	/// <summary>
	/// The action to do when unit arrived to its destination
	/// </summary>
	void actionOnArrive()
	{
		if (targetGO.tag.Equals ("FriendlyUnit") || targetGO.tag.Equals ("FriendlyBuilding") ) {
			if (Time.time >= nextActionTime) {
				if (tragetProp != null) {
					tragetProp.life -= findDamage (targetGO);
				} else if (tragetBuildingProp != null) {
					tragetBuildingProp.life -= fighterProp.buildingDamage;
				}
				nextActionTime = Time.time + fighterProp.actionSpeed;
				hasDestination = false;
				targetGO = null;
			}
		}
	}

	/// <summary>
	/// Finds the damage that unit has to deal to its target
	/// </summary>
	/// <returns>The damage.</returns>
	/// <param name="target">The unit targer</param>
	private int findDamage(GameObject target)
	{
		for (int i = 0; i < fighterProp.damageUnits.Count; i++) {
			if (fighterProp.damageUnits [i].unit.name.Equals (target.name))
				return fighterProp.damageUnits [i].damage;
		}
		return 0;
	}

}
