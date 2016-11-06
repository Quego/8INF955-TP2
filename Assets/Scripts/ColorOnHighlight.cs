using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorOnHighlight : MonoBehaviour {

	public void HighlightColor(){
		GetComponent<Image> ().color = Color.red;
	}
}
