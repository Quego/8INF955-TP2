using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Unit properties.
/// Defines all properties for a unit
/// </summary>
public class UnitProperties : MonoBehaviour {

	public int woodCost;
	public int foodCost;
	public int ironCost;
	public int unitCost;
	public int actionSpeed;
	public int speed;
	public int life;
	public int level;

	public int buildingDamage;

	/// <summary>
	/// Damage unit.
	/// Defines damages that unit makes against other units
	/// </summary>
	[Serializable]
	public struct DamageUnit {
		public int damage;
		public GameObject unit;
	}

	public List<DamageUnit> damageUnits;


	void Update()
	{
		died ();
	}

	/// <summary>
	/// Check if unit died. If so, update player Resources
	/// </summary>
	private void died()
	{
		if (life <= 0) {
			if (transform.CompareTag ("FriendlyUnit")) {
				PlayerData.units -= unitCost;
				PlayerData.updateResourceView ();
			}
			DestroyImmediate (gameObject);
		}
	}
}
