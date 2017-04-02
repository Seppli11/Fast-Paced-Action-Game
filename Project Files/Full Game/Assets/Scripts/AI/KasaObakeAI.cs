using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KasaObakeAI : MonoBehaviour {
    public Transform Player;
    public int MaxSeeDistance;
    public float AttackDistance;
    public float MaxSpeed;

    private State currentState = State.Idle;

    private Animator animator;

	private Movement movement;

	private Weapon weapon;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
		movement = GetComponent<Movement>();
		weapon = WeaponFactory.weaponFactory.GetLickAttack(gameObject, 5);
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(currentState);
        switch (currentState)
        {
            case State.Idle:
                animator.SetBool("walking", false);
                if(Vector2.Distance(Player.position, transform.position) <= MaxSeeDistance)
                    currentState = State.Walking;
                break;
            case State.Walking:
				if (Vector2.Distance(Player.position, transform.position) > MaxSeeDistance)
				{
					currentState = State.Idle;
					return;
				}
				if (Vector2.Distance(Player.position, transform.position) < AttackDistance)
				{
					movement.velocity = Vector2.zero;
					movement.rotationCopiesLastDirection = false;
					currentState = State.Attacking;
					return;
				}
                //if (animator.GetBool("attacking")) currentState = State.Attacking;

                Vector3 lastTransform = transform.position;
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, MaxSpeed * Time.deltaTime);
                Vector2 diff = transform.position - lastTransform;
				movement.velocity = diff;
                break;
            case State.Attacking:
                if (Vector2.Distance(Player.position, transform.position) >= AttackDistance)
                {
					if (animator.GetBool("attack") == false)
					{
						movement.rotationCopiesLastDirection = true;
						currentState = State.Walking;
					}
                } /*else if (animator.GetBool("attacking") == false)
                    animator.SetTrigger("attacking");*/

				Vector3 moveToVec = Vector3.MoveTowards(transform.position, Player.transform.position, MaxSpeed * Time.deltaTime);
				movement.rotation = moveToVec - transform.position;
				weapon.Attack();
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
