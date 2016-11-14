using UnityEngine;
using System.Collections;


/// <summary>
/// Handle Gatherer controller (for minions).
/// </summary>
public class GatherController : MonoBehaviour {
	
	public int gatherAmount;
	public GameObject associatedResource;
	public bool hasNewAssociatedResource;
	private UnitProperties unitProperties;

	private ResourceProperties prop;
	private float nextActionTime;

	void Start () {
		//Initialise variables
		associatedResource = null;
		hasNewAssociatedResource = false;
		if (gatherAmount == 0)
			gatherAmount = 5;

		unitProperties = transform.GetComponent<UnitProperties> ();
	}

	void Update () {
		gather ();
	}

	/// <summary>
	/// Gather targeted resource each X seconds, according to unit actionSpeed property.
	/// </summary>
	private void gather()
	{
		if (hasNewAssociatedResource) {
			//When unit has a new ressource, get all associated variableds
			prop = associatedResource.GetComponent<ResourceProperties> ();
			nextActionTime = Time.time + unitProperties.actionSpeed;
			hasNewAssociatedResource = false;
		}
		//When resource exists, gather it each time
		if (associatedResource != null) {
			if (Time.time >= nextActionTime) {
				prop.alreadyGathered += gatherAmount;
				if (prop.foodReward)
					PlayerData.food += gatherAmount;
				if (prop.woodReward)
					PlayerData.wood += gatherAmount;
				if (prop.ironReward)
					PlayerData.iron += gatherAmount;

				PlayerData.updateResourceView ();

				if (prop.alreadyGathered >= prop.maxGathering) {
					associatedResource = null;
				}
				nextActionTime = Time.time + unitProperties.actionSpeed;
			}

		}
	}
}
