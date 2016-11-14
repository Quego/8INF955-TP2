using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {


	public static bool menuLoaded = false;

	/// <summary>
	/// Loads a scene by index
	/// </summary>
	/// <param name="sceneIndex">Indew of scene to be loaded</param>
	public void LoadByIndex(int sceneIndex)
	{
		SceneManager.LoadScene (sceneIndex);
	}

	/// <summary>
	/// Loads a scene according to its name
	/// </summary>
	/// <param name="sceneName">Name of scene to be loaded</param>
	public void loadByName(string sceneName){
		SceneManager.LoadScene (sceneName);
	}

	/// <summary>
	/// Loads a scene additively
	/// </summary>
	/// <param name="sceneName">Name of scene to be loaded</param>
	public void loadAdditiveByName(string sceneName){

		if (!menuLoaded) {
			SceneManager.LoadScene (sceneName, LoadSceneMode.Additive);
			menuLoaded = true;
		}
	}
}
