using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.IO;
using System.Diagnostics;
using System.Text;

/// \brief A test script for "The Lab" style teleportation if you don't have a Vive.  Keep in mind that this 
///        doesn't have fade in/out, whereas TeleportVive (a version of this specifically made for the Vive) does.
/// \sa TeleportVive
[AddComponentMenu("Vive Teleporter/Test/Teleporter Test (No SteamVR)")]
public class TeleportScript : MonoBehaviour {

	public ParabolicPointer Pointer;

	public static bool ismoving;

	private const float epsilon = 0.001f;

	private Vector3 dest = Vector3.zero;

	public Fader fader;


	void Start(){	
		previousPosition = transform.position;
		ParabolicPointer.SelectionMeshScale = 0.1f;
		fader.fade (null);
	}
	public void fadercallback(){

	}

	void Update () {

		if (GvrController.ClickButtonDown && Pointer.PointOnNavMesh) {
			// we need to teleport
			dest = Pointer.SelectedPoint;
			dest.y = transform.position.y;
			teleport ();
		}
		if (ismoving) {
			blendedMove ();
		}


	}



	void teleport(){
		if (Config.USE_VECTION ) {
			distance=Vector3.Distance (dest , transform.position);
			UnityEngine.Debug.Log (distance);
			smoothInTime = Mathf.Clamp(smoothInPercent/distance,0,0.5f);
			smoothOutTime = Mathf.Clamp(smoothOutPercent/distance,0,0.5f);
			linearTime = 1f - smoothInTime - smoothOutTime;
			smoothedBlend = blend = 0;
			ismoving = true;
		} else {
			transform.position = dest;
		}

	}
	float distance;
	float blend = 0;
	float smoothedBlend=0;
	public float SmoothedBlend{
		get{
			return smoothedBlend ;
		}
	}

	const float smoothInPercent = 0.5f;
	const float smoothOutPercent = 1.5f;
	float smoothInTime;
	float smoothOutTime;
	float linearTime ;

	private Vector3 previousPosition;

	public void blendedMove(){
		if(blend < 1){

			blend = Mathf.Clamp01(blend + Config.Speed*Time.deltaTime/distance);
		}
		smoothedBlend = blend;
		if(smoothedBlend < 0.001f){
			smoothedBlend = 0;
		}
		else if (smoothedBlend > 0.999f){
			smoothedBlend = 1;
		}
		transform.position = (1-smoothedBlend)*previousPosition + smoothedBlend*dest;
		if(blend >= 1){
			ismoving = false;
			transform.position = dest;
			previousPosition = transform.position;
			blend = 0.0f;
		}

	}





}







