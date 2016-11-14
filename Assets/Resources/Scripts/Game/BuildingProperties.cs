using UnityEngine;
using System.Collections;

public class BuildingProperties : MonoBehaviour{

	public int woodCost;
	public int foodCost;
	public int ironCost;
	public int initialLife;
	public int life;
	public int level;
	public bool selfDestroyable;

	void Update()
	{
		if (life <= 0) {
			gameObject.SetActive (false);
			life = initialLife;
			level = 1;
		}
	}
}
