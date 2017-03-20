using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KasaObakeAI : AI {
    public Transform Player;
    public int MaxSeeDistance;
    public float AttackDistance;
    public float MaxSpeed;

    private State currentState = State.Idle;

    private Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(currentState);
        Debug.Log(animator.GetBool("attacking"));
        switch (currentState)
        {
            case State.Idle:
                animator.SetBool("walking", false);
                if(Vector2.Distance(Player.position, transform.position) < MaxSeeDistance)
                    currentState = State.Walking;
                break;
            case State.Walking:
                if (Vector2.Distance(Player.position, transform.position) > MaxSeeDistance)
                    currentState = State.Idle;
                if (Vector2.Distance(Player.position, transform.position) < AttackDistance)
                    currentState = State.Attacking;
                if (animator.GetBool("attacking")) currentState = State.Attacking;

                Vector3 lastTransform = transform.position;
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, MaxSpeed * Time.deltaTime);
                Vector2 diff = lastTransform - transform.position;
                animator.SetBool("walking", true);
                animator.SetFloat("x", diff.x);
                animator.SetFloat("y", diff.y);
                break;
            case State.Attacking:              
                if (Vector2.Distance(Player.position, transform.position) >= AttackDistance)
                {
                    if (animator.GetBool("attacking") == false)
                        currentState = State.Walking;
                }
                if (animator.GetBool("attacking") == false)
                    animator.SetTrigger("attacking");
                break;
            default:
                Debug.LogError("Unknown State '" + currentState + "'");
                break;

        }
	}

    enum State
    {
        Idle,
        Walking,
        Attacking
    }
}
