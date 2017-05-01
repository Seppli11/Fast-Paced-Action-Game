using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraRatio : MonoBehaviour
{
	public float ratioX, ratioY;
	private float ratio;
	private Camera camera;

	// Use tis for initialization
	void Start ()
	{
		ratio = ratioX / ratioY;
		camera = GetComponent<Camera>();
		camera.projectionMatrix = Matrix4x4.Ortho(
			-camera.orthographicSize*ratio, camera.orthographicSize*ratio,
			-camera.orthographicSize, camera.orthographicSize,
			camera.nearClipPlane, camera.farClipPlane);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
