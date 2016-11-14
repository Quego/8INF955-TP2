using UnityEngine;
using System.Collections;

public class CloseOnQuit : MonoBehaviour {

	public GameObject panel;

	/// <summary>
	/// Deactivate panel
	/// </summary>
	/// <param name="level">Level.</param>
	public void Unactive(int level)
	{
		panel.SetActive (false);

	} 
}
