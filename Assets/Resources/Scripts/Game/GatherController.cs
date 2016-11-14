using UnityEngine;
using System.Collections;

public class GatherController : MonoBehaviour {
	
	public int gatherAmount;
	public GameObject associatedResource;
	public bool hasNewAssociatedResource;
	private UnitProperties unitProperties;

	private ResourceProperties prop;
	private float nextActionTime;

	void Start () {
		associatedResource = null;
		hasNewAssociatedResource = false;
		if (gatherAmount == 0)
			gatherAmount = 5;

		unitProperties = transform.GetComponent<UnitProperties> ();
	}

	void Update () {
		
		if (hasNewAssociatedResource) {
			prop = associatedResource.GetComponent<ResourceProperties> ();
			nextActionTime = Time.time + unitProperties.actionSpeed;
			hasNewAssociatedResource = false;
		}

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
