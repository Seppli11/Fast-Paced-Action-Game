using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Vector3 offset;
    private Camera camera;
    public Transform playersTransform;

	public bool enable
	{
		set {
			camera.enabled = value;
		}
		get { return camera.enabled; }
	}

	void Start ()
	{
	    camera = GetComponent<Camera>();
	    //offset = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2));
	    offset = new Vector3(0, 0, -10);
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

    void LateUpdate()
    {
        transform.position = playersTransform.position + offset;
    }

	void AttackCameraAnimation()
	{

	}
	
}
