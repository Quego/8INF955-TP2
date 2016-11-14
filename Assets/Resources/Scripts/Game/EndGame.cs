using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manage End game.
/// </summary>
public class EndGame : MonoBehaviour {

	/// <summary>
	/// Load menu when the destroy event raised.
	/// </summary>
	public void OnDestroy(){
		SceneManager.LoadScene ("MainMenu");
	}
}

