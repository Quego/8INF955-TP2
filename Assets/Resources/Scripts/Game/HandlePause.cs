using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HandlePause : MonoBehaviour {

	private List<String> scriptsToDisabled;
	private List<MonoBehaviour> disabledScripts;

	void Awake () {
		//Pause the game by disabling all custom scripts

		disabledScripts = new List<MonoBehaviour> ();
		scriptsToDisabled = new List<String> ();
		MonoBehaviour thisScript = transform.GetComponent<MonoBehaviour> ();
		foreach (UnityEngine.Object o in  Resources.LoadAll ("Scripts/Game")) {
			scriptsToDisabled.Add (o.name);
		}

		MonoBehaviour[] scripts = GameObject.FindObjectsOfType<MonoBehaviour> ();
		foreach (MonoBehaviour p in scripts) {
			if (scriptsToDisabled.Contains (p.GetType ().ToString ()) && !p.Equals (thisScript)) {
				disabledScripts.Add (p);
				p.enabled = false;
			}
		}
	}

	public void resumeGame(){
		SceneManager.UnloadScene ("Menu");

		LoadSceneOnClick.menuLoaded = false;
		//re-enable all
		foreach (MonoBehaviour p in disabledScripts) {
			p.enabled = true;
		}

	}

	public void quit(string sceneName){
		SceneManager.LoadScene (sceneName);
	}


}
