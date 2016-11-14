 using UnityEngine;
using System.Collections;

/// <summary>
/// Handle music changes
/// </summary>
public class ChangeMusic : MonoBehaviour {

	public AudioClip level2Music;

	private AudioSource source;

	// Use this for initialization
	void Awake () {
	 
		source = GetComponent<AudioSource> ();
	}

	/// <summary>
	/// Change music on level was loaded event.
	/// </summary>
	/// <param name="level">the loaded level</param>
	void OnLevelWasLoaded(int level)
	{
		if (level == 1)
		{
			source.clip = level2Music;
			source.Play ();
		}
	}
}
