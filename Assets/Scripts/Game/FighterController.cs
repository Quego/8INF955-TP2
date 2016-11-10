using UnityEngine;
using System.Collections;

public class FighterController : MonoBehaviour {

	public GameObject target;
	public bool hasNewTarget;

	private UnitProperties tagetProp;
	private BuildingProperties tagetBuildingProp;
	private UnitProperties fighterProp;
	private float nextActionTime;

	// Use this for initialization
	void Start () {
		tagetProp = null;
		tagetBuildingProp = null;
		fighterProp = null;
		hasNewTarget = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (hasNewTarget) {
			tagetProp = target.GetComponent<UnitProperties> ();
			tagetBuildingProp = target.GetComponent<BuildingProperties> ();
			fighterProp = transform.GetComponent<UnitProperties> ();
			hasNewTarget = false;
			nextActionTime = Time.time + fighterProp.actionSpeed;
		}
		if (target != null) {
			if (Time.time >= nextActionTime) {
				if (tagetProp != null) {
					tagetProp.life -= findDamage (target);
				} else if (tagetBuildingProp != null) {
					tagetBuildingProp.life -= fighterProp.buildingDamage;
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
