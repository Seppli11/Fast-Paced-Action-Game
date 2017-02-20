using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour {
	private Transform transform;
    private Rigidbody2D rigidbody;

    private LastDirection lastDirection = LastDirection.UP;

    public float speed;
    public float teleportRange;
	// Use this for initialization
	void Start () {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.position = new Vector3((float)(transform.position.x + (speed * Time.deltaTime)), transform.position.y);
            lastDirection = LastDirection.RIGHT;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.position = new Vector3((float)(transform.position.x - (speed * Time.deltaTime)), transform.position.y);
            lastDirection = LastDirection.LEFT;
        }

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            transform.position = new Vector3(transform.position.x, (float)(transform.position.y + (speed * Time.deltaTime)));
            lastDirection = LastDirection.DOWN;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            transform.position = new Vector3(transform.position.x, (float)(transform.position.y - (speed * Time.deltaTime)));
            lastDirection = LastDirection.UP;
        }

        if(Input.GetButtonDown("Fire1"))
        {
            switch(lastDirection)
            {
                case LastDirection.UP:
                    transform.position = new Vector3(transform.position.x, transform.position.y - teleportRange);
                    break;
                case LastDirection.DOWN:
                    transform.position = new Vector3(transform.position.x, transform.position.y + teleportRange);
                    break;
                case LastDirection.LEFT:
                    transform.position = new Vector3(transform.position.x - teleportRange, transform.position.y);
                    break;
                case LastDirection.RIGHT:
                    transform.position = new Vector3(transform.position.x + teleportRange, transform.position.y);
                    break; 
            }
        }
    }

    private enum LastDirection
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
}
