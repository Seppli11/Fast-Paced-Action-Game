using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Vector3 offset;
    private Camera camera;
    public Transform playersTransform;
	// Use this for initialization
	void Start ()
	{
	    camera = GetComponent<Camera>();
	    offset = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2));
	}
	
	// Update is called once per frame
	void Update ()
	{
	    
	}

    void LateUpdate()
    {
        transform.position = playersTransform.position + offset;
    }
}
