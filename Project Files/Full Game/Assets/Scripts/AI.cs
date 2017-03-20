using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AI : MonoBehaviour {
    public int CurrentState = -1;
    private Dictionary<int, StatePair> executionStates = new Dictionary<int, StatePair>();

	// Use this for initialization
	void Start () {
		
	}

    private void FixedUpdate()
    {
        foreach(StatePair sp in executionStates.Values)
        {
            if(sp.Key == CurrentState)
            {
                sp.Fire(ref CurrentState);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void addState(StatePair state)
    {
        executionStates.Add(state.Key, state);
    }

    public delegate void Modifiy(Dictionary<int, StatePair> dir);
    public void modifiyStateDictionary(Modifiy modifyFunction)
    {
        modifyFunction(executionStates);
    }
    
}
public delegate int ExecuteState(ref int currentState);
public class StatePair
{
    public event ExecuteState ExecuteStateEvent;
    private int _key;
    public int Key
    {
        get { return _key; }
    }

    public StatePair(ExecuteState execcuteState, int key)
    {
        ExecuteStateEvent += execcuteState;
        this._key = key;
    }

    public StatePair(int key)
    {
        this._key = key;
    }

    public void Fire(ref int currentState)
    {
        ExecuteStateEvent(ref currentState);
    }
}