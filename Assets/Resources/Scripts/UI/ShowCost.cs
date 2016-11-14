using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowCost : MonoBehaviour {
	public GameObject toShow;
	public GameObject factory;
	public GameObject unit;
	private float factoryLevelPercentageReducer = 10.0f;
	private static Text textFood;
	private static  Text textWood;
	private static  Text textIron;


	void Start(){
		if (textFood == null) {
			textFood = GameObject.Find ("ShowFoodCost").GetComponent<Text> ();
			textWood = GameObject.Find ("ShowWoodCost").GetComponent<Text> ();
			textIron = GameObject.Find ("ShowIronCost").GetComponent<Text> ();
			toShow.SetActive (false);
		}
	}

	/// <summary>
	/// Shows the cost of selection.
	/// </summary>
	public void showCost(){
		int f, w, i, lvl;
		UnitProperties properties;
		BuildingProperties factoryProperties = factory.GetComponent<BuildingProperties> ();
		lvl = factoryProperties.level;
		if (unit != null) {
			properties = unit.GetComponent<UnitProperties> ();
			f = costReduced (properties.foodCost, lvl);
			w = costReduced (properties.woodCost, lvl);
			i = costReduced (properties.ironCost, lvl);
		} else {
			f = (lvl+1) * factoryProperties.foodCost;
			w = (lvl+1) * factoryProperties.woodCost;
			i = (lvl+1) * factoryProperties.ironCost;
		}
		textFood.text = f.ToString();
		textWood.text = w.ToString();
		textIron.text = i.ToString();
		toShow.SetActive (true);
	}

	/// <summary>
	/// Get the cost after factory level reduction
	/// </summary>
	/// <returns>The reduced.</returns>
	/// <param name="cost">Initial Cost.</param>
	/// <param name="factoryLevel">Factory level.</param>
	private int costReduced(int cost, int factoryLevel)
	{
		return cost - (int)((factoryLevel-1) * cost* (factoryLevelPercentageReducer / 100f));
	}

	public void hideCost(){
		toShow.SetActive (false);
	}
}
