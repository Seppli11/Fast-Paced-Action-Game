using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	public static Camera currentCamera;

    private Vector3 offset;
    private Camera camera;
    public Transform playersTransform;

	public bool enable
	{
		set {
			camera.enabled = value;
			if(value == true)
			{
				currentCamera = camera;
			}
		}
		get { return camera.enabled; }
	}
	// Use this for initialization
	void Start ()
	{
	    camera = GetComponent<Camera>();
		currentCamera = camera;
	    offset = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2));
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if(camera.enabled)
		{
			if (!currentCamera.enabled) currentCamera = camera;
		}
	}

    void LateUpdate()
    {
        transform.position = playersTransform.position + offset;
    }

	
}
