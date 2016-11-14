using UnityEngine;
using UnityEngine.SceneManagement;


public class EndGame : MonoBehaviour {

	//it calls when the popup with the message "You xin !" or "You lose !" is destroy !
	public void OnDestroy(){
		//it sends the player to the "Main Menu"'s scene
		SceneManager.LoadScene ("MainMenu");
	}
}

