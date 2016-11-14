using UnityEngine;
using System.Collections;

public class DesactiveOnWait : MonoBehaviour {

	// Use this for initialization
	public void Disable () {
		if (!gameObject.activeInHierarchy)
			gameObject.SetActive (true);

		StartCoroutine (LateCall ());
	}
	
	IEnumerator LateCall()
	{
		yield return new WaitForSeconds (0.15f);
		gameObject.SetActive (false);
	}
}

