using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickToLoadAsync : MonoBehaviour {

	public Slider loadingBar;
	public GameObject loadingImage;

	private AsyncOperation async;

	/// <summary>
	/// Launches loading view
	/// </summary>
	/// <param name="level">level to loed</param>
	public void ClickAsync(int level)
	{
		loadingImage.SetActive (true);
		StartCoroutine (LoadLevelWithBar (level));
	}

	/// <summary>
	/// Updates the load bar until level's loaded.
	/// </summary>
	/// <returns>The level with bar.</returns>
	/// <param name="level">Level.</param>
	IEnumerator LoadLevelWithBar (int level)
	{
		async = SceneManager.LoadSceneAsync (level);
		while (!async.isDone)
		{
			loadingBar.value = async.progress; 
			yield return null;
		}
	}

}
