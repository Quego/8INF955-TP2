using UnityEngine;
using System.Collections;

/// <summary>
/// Building properties.
/// Defines all properties for a building
/// </summary>
public class BuildingProperties : MonoBehaviour{

	public int woodCost;
	public int foodCost;
	public int ironCost;
	public int initialLife;
	public int life;
	public int level;
	public bool selfDestroyable; // if player can destruct his building or not

	void Update()
	{
		died ();
	}

	/// <summary>
	/// Check if building died. If so, deactivate it so it can be rebuilt
	/// </summary>
	private void died()
	{
		if (life <= 0) {
			gameObject.SetActive (false);
			life = initialLife;
			level = 1;
		}
	}
}
