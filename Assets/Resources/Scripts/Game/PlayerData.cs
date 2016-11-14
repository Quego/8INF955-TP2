using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Manage persistence of player data
/// </summary>
public class PlayerData : MonoBehaviour {

	public static int level;
	public static int maxUnits;
	public static int food;
	public static int wood;
	public static int iron;
	public static int units;

	private static Text textFood;
	private static Text textWood;
	private static Text textIron;
	private static Text textUnits;


	void Start () {
		//Initialise variables
		maxUnits = 200;
		food = 1000;
		wood = 1000;
		iron = 1000;
		units = 0;
		level = 1;

		textFood = GameObject.Find ("FoodCount").GetComponent<Text> ();
		textWood = GameObject.Find ("WoodCount").GetComponent<Text> ();
		textIron = GameObject.Find ("IronCount").GetComponent<Text> ();
		textUnits = GameObject.Find ("UnitsCount").GetComponent<Text> ();

		updateResourceView ();
	}

	/// <summary>
	/// Updates the resource view texts.
	/// </summary>
	public static void updateResourceView(){
		textFood.text = food.ToString();
		textWood.text = wood.ToString();
		textIron.text = iron.ToString();
		textUnits.text = units.ToString()+"/"+maxUnits;
	}
}
