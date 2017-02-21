using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Controls Controls;
    public float Speed;

	void Start () {
		
	}
	
	void Update () {
	    if (Controls.HorizontalAxis > 0)
	    {
	        transform.position = new Vector3(transform.position.x + Speed * Time.deltaTime, transform.position.y);
	    } else if (Controls.HorizontalAxis < 0)
	    {
            transform.position = new Vector3(transform.position.x - Speed * Time.deltaTime, transform.position.y);
        }

        if (Controls.VerticalAxis > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Speed * Time.deltaTime);
        }
        else if (Controls.VerticalAxis < 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - Speed * Time.deltaTime);
        }
    }
}
