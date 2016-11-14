using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

	public GameObject defaultSelect;
	public Color SelectionColor;

	public float screenDivider; // scaling offsetControllerX and offsetControllerY
	public float speedMove;
	public float speedWheel;
	public float maxZoomY;
	public float minZoomY;

	private float offsetControllerX;
	private float offsetControllerY;

	public GameObject selectedGO;
	private Color previousColor;


	void Start () {
		selectedGO = null;
		//Initialise values if nobody did it in the inscpector
		if (speedMove == 0)
			speedMove = 2;

		if (speedWheel == 0)
			speedWheel = 2;

		if (maxZoomY == 0)
			maxZoomY = 10;
		
		if (maxZoomY == 0)
			maxZoomY = 2;

		if (screenDivider == 0)
			screenDivider = 10;

		if (SelectionColor == Color.black)
			SelectionColor = Color.blue;
	}


	void Update () {
		//Update here in case of screen resizing
		offsetControllerX = Screen.width / screenDivider;
		offsetControllerY = Screen.height / screenDivider;

		if (!MouseOverCanvas.onCanvas) {
			handleCameraMoves ();
			handleWheel ();
			//When left Click
			if (Input.GetMouseButtonDown (0)) {
				handleLeftClick ();
			}
			//when right click, and unit selected
			else if (Input.GetMouseButtonDown (1) && selectedGO != null && selectedGO.tag.Equals ("FriendlyUnit")) {
				handleRightClick ();
			}
		}
		//Center view on selected object or on mainBase
		if (Input.GetKey (KeyCode.Space)) {
			handleSpaceBar ();
		}
	}



	private void handleCameraMoves()
	{
		//moving left, right according to cursor
		if (Input.mousePosition.x > 0 && Input.mousePosition.x < Screen.width) {
			if (Input.mousePosition.x < offsetControllerX || Input.GetKey (KeyCode.LeftArrow)) {
				Camera.main.transform.position -= Camera.main.transform.right * speedMove * Time.smoothDeltaTime;
			} else if (Input.mousePosition.x > Screen.width - offsetControllerX || Input.GetKey (KeyCode.RightArrow)) {
				Camera.main.transform.position += Camera.main.transform.right * speedMove * Time.smoothDeltaTime;
			}
		}

		//moving forward, backward according to cursor
		if (Input.mousePosition.y > 0 && Input.mousePosition.y < Screen.height) {
			if (Input.mousePosition.y < offsetControllerY || Input.GetKey (KeyCode.DownArrow)) {
				Camera.main.transform.position += Vector3.back * speedMove * Time.smoothDeltaTime;
			} else if (Input.mousePosition.y > Screen.height - offsetControllerY || Input.GetKey (KeyCode.UpArrow)) {
				Camera.main.transform.position += Vector3.forward * speedMove * Time.smoothDeltaTime;
			}
		}
	}


	private void handleWheel()
	{
		//zooming in, out according to mouseWheel
		if (Input.mouseScrollDelta.y > 0 && Camera.main.transform.position.y > minZoomY) {
			Camera.main.transform.position += Camera.main.transform.forward * speedWheel * Time.smoothDeltaTime;
		} else if (Input.mouseScrollDelta.y < 0 && Camera.main.transform.position.y < maxZoomY) {
			Camera.main.transform.position -= Camera.main.transform.forward * speedWheel * Time.smoothDeltaTime;
		}
	}

	private void handleLeftClick(){
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		//project click on game environment
		if (Physics.Raycast (ray, out hit)) {
			GameObject currentHit = null;

			//check which game object has been hit
			if (hit.transform.tag.Equals ("FriendlyBuilding")) {
				currentHit = hit.transform.gameObject;
			} else if (hit.transform.parent != null && hit.transform.parent.tag.Equals ("FriendlyBuilding")) {
				currentHit = hit.transform.parent.gameObject;
			} else if (hit.transform.tag.Equals ("FriendlyUnit")) {
				currentHit = hit.transform.gameObject;
			} else if (hit.transform.parent != null && hit.transform.parent.tag.Equals ("FriendlyUnit")) {
				currentHit = hit.transform.parent.gameObject;
			}
			selectObject (currentHit);
		} 
	}

	private void handleRightClick(){
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		//project click on game environment
		if (Physics.Raycast (ray, out hit)) {
			GameObject currentHit = hit.transform.gameObject;

			//If it click on a enemy bodypart look for parent
			if (hit.transform.parent != null && hit.transform.parent.tag.Equals ("EnemyBuilding")) {
				currentHit = hit.transform.parent.gameObject;
			} else if (hit.transform.parent != null && hit.transform.parent.tag.Equals ("EnemyUnit")) {
				currentHit = hit.transform.parent.gameObject;
			}

			//make selected unit move
			UnitController selectedUnitController = selectedGO.GetComponent<UnitController> ();
			selectedUnitController.targetGO = currentHit;
			selectedUnitController.destination = new Vector3 (hit.point.x, hit.point.y + 2, hit.point.z);
			selectedUnitController.hasDestination = true;
		}

	}

	private void handleSpaceBar(){
		if (selectedGO != null)
			Camera.main.transform.position = new Vector3 (selectedGO.transform.position.x, Camera.main.transform.position.y, selectedGO.transform.position.z - Camera.main.transform.position.y);
		else {
			if (defaultSelect != null)
				Camera.main.transform.position = new Vector3 (defaultSelect.transform.position.x, Camera.main.transform.position.y, defaultSelect.transform.position.z - Camera.main.transform.position.y);
		}
	}

	public void selectObject(GameObject newSelection){

		//show new selection
		if (newSelection != null) {
			colorSelection (newSelection, selectedGO);
			selectedGO = newSelection;
		} 
		//if there no new selection, set it to default
		else {
			colorSelection (defaultSelect, selectedGO);
			selectedGO = defaultSelect;
		}
		//show UI linked to this GO
		if (selectedGO != null) {
			ChangeUiOnSelect reactScript = selectedGO.GetComponent<ChangeUiOnSelect> ();
			if (reactScript != null)
				reactScript.applySelectReaction ();
		}
	}

	private void colorSelection(GameObject toColor, GameObject previousSelection){

		//Paint everything about selection in previous color
		if (previousSelection != null) {
			previousSelection.GetComponent<Renderer> ().material.color = previousColor;
			foreach (Renderer rend in previousSelection.transform.GetComponentsInChildren<Renderer>()) {
				rend.material.color = previousColor;
			}
		}
		//Paint new Selection with the Selection Color
		if (toColor != null) {
			Renderer temp = toColor.GetComponent<Renderer> ();
			previousColor = temp.material.color;
			temp.material.color = SelectionColor;
			foreach (Renderer rend in toColor.transform.GetComponentsInChildren<Renderer>()) {
					rend.material.color = SelectionColor;
			}
		}
	}
		
}
