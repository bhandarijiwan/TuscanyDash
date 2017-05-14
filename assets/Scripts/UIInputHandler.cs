using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Diagnostics;
using System;
public class UIInputHandler : MonoBehaviour {

	void Start () {
		intializeConfig ();
	}

	public void intializeConfig(){

		Config.GameState = GAMESTATE.PAUSE;  // The game is paused when you initialize
		Config.USE_VECTION = true;
	}

	void Update () {
		if ( GvrController.AppButtonDown && !TeleportScript.ismoving) { 
			Config.GameState = GAMESTATE.PLAY;	
			Config.USE_VECTION = !Config.USE_VECTION;
		} 

	}


	public void updateSpeed(){

	}

	public void upateViewPoint(float newvalue){

	}



}


public enum GAMESTATE{
	PLAY,PAUSE
}
public class Config{
	private Config(){}
	public static GAMESTATE GameState;
	public static float Speed = 10.0f;
	public static bool USE_VECTION=true;

}

