using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetMusicVolume : MonoBehaviour {

	public Slider VolumeBar;
	public AudioSource audioSource;
	
	public void MusicVolume () {
			audioSource.volume = VolumeBar.value;
	}
}
