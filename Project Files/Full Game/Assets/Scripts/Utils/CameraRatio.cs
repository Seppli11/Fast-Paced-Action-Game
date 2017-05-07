using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraRatio : MonoBehaviour
{
	public float ratioX, ratioY;
	private float ratio;
	private Camera componentCamera;

	// Use tis for initialization
	void Start ()
	{
		ratio = ratioX / ratioY;
		componentCamera = GetComponent<Camera>();
		componentCamera.projectionMatrix = Matrix4x4.Ortho(
			-componentCamera.orthographicSize*ratio, componentCamera.orthographicSize*ratio,
			-componentCamera.orthographicSize, componentCamera.orthographicSize,
			componentCamera.nearClipPlane, componentCamera.farClipPlane);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
