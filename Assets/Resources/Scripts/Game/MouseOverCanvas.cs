using UnityEngine;
using System.Collections;

/// <summary>
/// Check if mouse is on canvas or on game view (used for InputController)
/// </summary>
public class MouseOverCanvas : MonoBehaviour {
	
	public static bool onCanvas;

	void Start(){
		onCanvas = false;
	}

	/// <summary>
	/// Changes boolean on canvas enter event.
	/// </summary>
	public void OnCanvasEnter(){
		onCanvas = true;
	}

	/// <summary>
	/// Changes boolean on canvas exit event.
	/// </summary>
	public void OnCanvasExit(){
		onCanvas = false;
	}
	
}
