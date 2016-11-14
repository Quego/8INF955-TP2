﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {

	public GameObject loadingImage;

	public void LoadScene(int level)
	{
		loadingImage.SetActive (true);
		//Application.LoadLevel (level); //Deprecated 
		SceneManager.LoadScene(level);
	} 
}
 