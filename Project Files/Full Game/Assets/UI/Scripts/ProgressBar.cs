using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{

    public int progression;
    private Animator animator;
    public AnimationState AnimationState;

   
	// Use this for initialization
	void Start ()
	{
	    animator = GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	void Update ()
	{
	    animator.Play("Progress Bar", 0, progression/100);
        Debug.Log( (float) (1 / 14 * GetProgressionFrame()));
	}

    private float GetProgressionFrame()
    {
        return progression * 14 / 100;
    }
}
