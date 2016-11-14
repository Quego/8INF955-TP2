using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Manage a resource mine.
/// </summary>
public class ResourceProperties : MonoBehaviour {

	public bool woodReward;
	public bool foodReward;
	public bool ironReward;
	public int maxMignons;
	public int alreadyGathered;
	public int maxGathering;

	public List<GameObject> linkedMinions;

	void Start(){
		linkedMinions = new List<GameObject> ();

		alreadyGathered = 0;

		if (maxMignons == 0)
			maxMignons = 5;

		//After gathering everything, there are no more resources
		if (maxGathering == 0)
			maxGathering = 5000;
	}


	void Update(){
		died ();
	}

	/// <summary>
	/// Makes the resource disappeared when there are no more resources
	/// </summary>
	private void died()
	{
		if (alreadyGathered >= maxGathering) {
			Destroy (gameObject);
		}
	}
}
