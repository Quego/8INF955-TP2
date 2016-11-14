using UnityEngine;
using System.Collections;


/// <summary>
/// Manage Unit moves and actions.
/// </summary>
public class UnitController : MonoBehaviour {

	public GameObject targetGO;
	public Vector3 destination;
	private UnitProperties properties;
	private float speed;
	public bool hasDestination;
	private bool stoppedCurrentAction;
	private bool attackTargetGiven;

	private GatherController gatherer;
	private FighterController fighter;


	void Start () {
		//Initialise variables
		properties = transform.GetComponent<UnitProperties> ();
		speed = properties.speed;
		destination = transform.position;
		hasDestination = false;
		stoppedCurrentAction = false;
		attackTargetGiven = false;

		gatherer = transform.GetComponent<GatherController> ();
		fighter = transform.GetComponent<FighterController> ();
	}

	void Update () {
		moveToTarget ();
	}

	/// <summary>
	/// Makes the unit move target, if there's no target, give one
	/// </summary>
	private void moveToTarget()
	{
		if (targetGO == null)
			hasDestination = false;
		if (hasDestination) {
			//if there's a new destination, reinitilise variables
			if (!stoppedCurrentAction && !targetGO.CompareTag ("EnemyUnit")) {
				if (gatherer != null) {
					gatherer.associatedResource = null;
				}
				if (fighter != null) {
					fighter.target = null;
				}
				stoppedCurrentAction = true;
				attackTargetGiven = false;
			}

			transform.LookAt (destination);
			transform.position += transform.forward * Time.smoothDeltaTime * speed;

			//Arrive on target
			if ((transform.position - destination).magnitude <= 0.5f) {

				if(!targetGO.CompareTag("EnemyUnit"))
					hasDestination = false;
				actionOnArrive ();
				stoppedCurrentAction = false;
			}
		}
		if (targetGO!=null && targetGO.CompareTag ("EnemyUnit")) {
			if ((transform.position - targetGO.transform.position).magnitude > 1f) 
				destination = targetGO.transform.position;
		}

	}

	/// <summary>
	/// The action to do when unit arrived to its destination
	/// </summary>
	void actionOnArrive()
	{
		//it's a minion with a resource targer
		if (targetGO.tag.Equals("Resource") && gatherer != null) {
			gatherer.associatedResource = targetGO;
			gatherer.hasNewAssociatedResource = true;
		} 
		//It's a unit focusing a ennemy entity
		else if (!attackTargetGiven && targetGO.tag.Equals ("EnemyUnit") || targetGO.tag.Equals ("EnemyBuilding") && transform.GetComponent<GatherController> () == null) {
			fighter.target = targetGO;
			fighter.hasNewTarget = true;
			attackTargetGiven = true;
		}
	}
}
