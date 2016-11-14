using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {


	public static bool menuLoaded = false;

	public void LoadByIndex(int sceneIndex)
	{
		//Async?
		SceneManager.LoadScene (sceneIndex);
	}

	public void loadByName(string sceneName){
		SceneManager.LoadScene (sceneName);
	}

	public void loadAdditiveByName(string sceneName){

		if (!menuLoaded) {
			SceneManager.LoadScene (sceneName, LoadSceneMode.Additive);
			menuLoaded = true;
		}
	}
}
