using UnityEngine;
using UnityEngine.SceneManagement;


public class EndGame : MonoBehaviour {

	public void OnDestroy(){
		SceneManager.LoadScene ("MainMenu");
	}
}

