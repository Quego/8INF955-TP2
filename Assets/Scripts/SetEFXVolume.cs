using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetEFXVolume : MonoBehaviour {

	public Slider VolumeBar;
	public AudioSource[] audioSource;

	public void EFXVolume () {
		for (int i = 0; i < audioSource.Length ; i++)
			audioSource[i].volume = VolumeBar.value;
	}
}
