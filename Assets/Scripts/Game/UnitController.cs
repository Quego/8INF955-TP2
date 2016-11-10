using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {

	public GameObject targetGO;
	public Vector3 destination;
	private UnitProperties properties;
	private float speed;
	public bool hasDestination;
	private bool stoppedCurrentAction;

	private GatherController gatherer;
	private FighterController fighter;


	void Start () {
		properties = transform.GetComponent<UnitProperties> ();
		speed = properties.speed;
		destination = transform.position;
		hasDestination = false;
		stoppedCurrentAction = false;

		gatherer = transform.GetComponent<GatherController> ();
		fighter = transform.GetComponent<FighterController> ();
	}

	void Update () {
		moveToTarget ();
	}


	private void moveToTarget()
	{
		if (hasDestination) {
			if (!stoppedCurrentAction) {
				if (gatherer != null) {
					gatherer.associatedResource = null;
				}
				if (fighter != null) {
					fighter.target = null;
				}
				stoppedCurrentAction = true;
			}

			transform.LookAt (destination);
			transform.position += transform.forward * Time.smoothDeltaTime * speed;

			//Arrive on target
			if ((transform.position - destination).magnitude <= 0.1f) {
				hasDestination = false;
				actionOnArrive ();
				stoppedCurrentAction = false;
			}
		}
	}

	void actionOnArrive()
	{
		//it's a minion with a resource targer
		if (targetGO.tag.Equals("Resource") && gatherer != null) {
			gatherer.associatedResource = targetGO;
			gatherer.hasNewAssociatedResource = true;
		} 
		//It's a unit focusing a ennemy entity
		else if (targetGO.tag.Equals ("EnemyUnit") || targetGO.tag.Equals ("EnemyBuilding") && transform.GetComponent<GatherController> () == null) {
			fighter.target = targetGO;
			fighter.hasNewTarget = true;
		}
	}
}
