
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManagerObject : MonoBehaviour
{

    private TimerManager timerManager;
	// Use this for initialization
	void Start () {
		timerManager = TimerManager.STimerManager;
	}
	
	// Update is called once per frame
	void Update () {
		timerManager.Update();
	}
}
