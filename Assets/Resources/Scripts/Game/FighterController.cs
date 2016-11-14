using UnityEngine;
using System.Collections;

public class FighterController : MonoBehaviour {

	public GameObject target;
	public bool hasNewTarget;

	private UnitProperties tragetProp;
	private BuildingProperties tragetBuildingProp;
	private UnitProperties fighterProp;
	private float nextActionTime;
	private bool isTargetDead;

	// Use this for initialization
	void Start () {
		tragetProp = null;
		tragetBuildingProp = null;
		fighterProp = null;
		hasNewTarget = false;
		isTargetDead = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (hasNewTarget) {
			tragetProp = target.GetComponent<UnitProperties> ();
			tragetBuildingProp = target.GetComponent<BuildingProperties> ();
			fighterProp = transform.GetComponent<UnitProperties> ();
			hasNewTarget = false;
			nextActionTime = Time.time + fighterProp.actionSpeed;
		}
		if (target != null) {
			if (Time.time >= nextActionTime) {
				Debug.Log ("HIT");
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

	private int findDamage(GameObject target)
	{
		for (int i = 0; i < fighterProp.damageUnits.Count; i++) {
			if (fighterProp.damageUnits [i].unit.name.Equals (target.name))
				return fighterProp.damageUnits [i].damage;
		}
		return 0;
	}
}
