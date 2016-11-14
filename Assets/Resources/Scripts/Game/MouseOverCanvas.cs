using UnityEngine;
using System.Collections;

public class MouseOverCanvas : MonoBehaviour {
	
	public static bool onCanvas;

	void Start(){
		onCanvas = false;
	}
	public void OnCanvasEnter(){
		onCanvas = true;
	}

	public void OnCanvasExit(){
		onCanvas = false;
	}
	
}
