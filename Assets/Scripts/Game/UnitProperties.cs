﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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

	[Serializable]
	public struct DamageUnit {
		public int damage;
		public GameObject unit;
	}

	public List<DamageUnit> damageUnits;

	void Update()
	{
		if (life <= 0)
			DestroyImmediate (gameObject);
	}
}