using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handle pause 
/// </summary>
public class HandlePause : MonoBehaviour {

	private List<String> scriptsToDisabled;
	private List<MonoBehaviour> disabledScripts;

	void Awake () {
		//Pause the game by disabling all custom scripts located in "Scripts/Game"
		disabledScripts = new List<MonoBehaviour> ();
		scriptsToDisabled = new List<String> ();
		MonoBehaviour thisScript = transform.GetComponent<MonoBehaviour> ();
		foreach (UnityEngine.Object o in  Resources.LoadAll ("Scripts/Game")) {
			scriptsToDisabled.Add (o.name);
		}

		//Disable them all but this script
		MonoBehaviour[] scripts = GameObject.FindObjectsOfType<MonoBehaviour> ();
		foreach (MonoBehaviour p in scripts) {
			if (scriptsToDisabled.Contains (p.GetType ().ToString ()) && !p.Equals (thisScript)) {
				disabledScripts.Add (p);
				p.enabled = false;
			}
		}
	}

	/// <summary>
	/// Resumes the game.
	/// </summary>
	public void resumeGame(){
		SceneManager.UnloadScene ("Menu");

		LoadSceneOnClick.menuLoaded = false;
		//re-enable all disables scripts
		foreach (MonoBehaviour p in disabledScripts) {
			p.enabled = true;
		}

	}

	/// <summary>
	/// Quit menu and load specified scene
	/// </summary>
	/// <param name="sceneName">scene to load when on quit</param>
	public void quit(string sceneName){
		SceneManager.LoadScene (sceneName);
	}


}
