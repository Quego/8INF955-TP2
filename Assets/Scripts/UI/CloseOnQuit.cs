using UnityEngine;
using System.Collections;

public class CloseOnQuit : MonoBehaviour {

	public GameObject panel;

	public void Unactive(int level)
	{
		panel.SetActive (false);

	} 
}
