using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

	public void LoadByIndex(int sceneIndex)
	{
		//Async?
		SceneManager.LoadScene (sceneIndex);
	}

	public void loadByName(string sceneName){
		SceneManager.LoadScene (sceneName);
	}

	public void loadAdditiveByName(string sceneName){
		SceneManager.LoadScene (sceneName, LoadSceneMode.Additive);
	}
}
