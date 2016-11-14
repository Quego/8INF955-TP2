using UnityEngine;
using System.Collections;

/// <summary>
/// Unit Fighter Behaviour.
/// </summary>
public class FighterController : MonoBehaviour {

	public GameObject target;
	public bool hasNewTarget;

	private UnitProperties tragetProp;
	private BuildingProperties tragetBuildingProp;
	private UnitProperties fighterProp;
	private float nextActionTime;
	private bool isTargetDead;


	void Start () {
		//Initialise variables
		tragetProp = null;
		tragetBuildingProp = null;
		fighterProp = null;
		hasNewTarget = false;
		isTargetDead = false;
	}

	void Update () {
		hit ();
	}

	/// <summary>
	/// Hit unit target
	/// </summary>
	private void hit()
	{
		if (hasNewTarget) {
			//if there is a new target, get all needed variables for this target
			tragetProp = target.GetComponent<UnitProperties> ();
			tragetBuildingProp = target.GetComponent<BuildingProperties> ();
			fighterProp = transform.GetComponent<UnitProperties> ();
			hasNewTarget = false;
			nextActionTime = Time.time + fighterProp.actionSpeed;
		}
		if (target != null) {
			//Hit every X time, handle building and units
			if (Time.time >= nextActionTime) {
				if (tragetProp != null) {
					tragetProp.life -= findDamage (target);
					isTargetDead = tragetProp.life <= 0;
				} else if (tragetBuildingProp != null) {
					tragetBuildingProp.life -= fighterProp.buildingDamage;
					isTargetDead = tragetBuildingProp.life <= 0;
				}
				if (isTargetDead) {
					target = null;
				}
				nextActionTime = Time.time + fighterProp.actionSpeed;
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
