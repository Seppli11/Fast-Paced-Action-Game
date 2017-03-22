using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    
    public GameObject[] objects;

	// Use this for initialization
	void Start () {
		UpdateObjectArray();
       
	}

    public void UpdateObjectArray()
    {
        objects = GameObject.FindGameObjectsWithTag("ManagedByOrderManager");
 
    }

	// Update is called once per frame
	void Update () {
		UpdateObjectOrder();
	}

    public void UpdateObjectOrder()
    {
        Array.Sort(objects, delegate (GameObject go1, GameObject go2)
        {
            //return go1.transform.position.y.CompareTo(go2.transform.position.y);
            float y1 = go1.transform.position.y;
            float y2 = go2.transform.position.y;
            if (y1 > y2) return -1;
            if (y1 < y2) return 1;
            else return 0;
        });

        for (int i = 0; i < objects.Length; i++)
        { 
            
            Renderer sp = objects[i].GetComponent<Renderer>();
            sp.sortingOrder = i;
            Debug.Log(objects[i].name + " = " + i);
        }
    }
}
