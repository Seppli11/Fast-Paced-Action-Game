using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AI : MonoBehaviour {
    public int CurrentState;

    public delegate int ExecuteState();

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private class StatePair
    {
        public event ExecuteState ExecuteStateEvent;
        
    }
}
