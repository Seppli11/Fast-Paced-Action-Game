using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private TimerManager timerManager;

    private Animator animator;

    private Controls controls;
    public float Speed;
    public float TeleportSpeed;

    public float NonTeleportTime;
    public float TeleportTime;

    private Vector2 teleportDirection = Vector2.zero;

    private bool cannotTeleport = false;
    private bool currentlyTeleporting = false;

    private SpriteRenderer spriteRenderer;

    private Sprite lastSprite = null;
    private Vector3 lastPosition;
    public GameObject PlayerShadow;

    private PlayerUi playerUi;

    private Rigidbody2D rigidbody;
	void Start ()
	{
        timerManager = TimerManager.STimerManager;;
	    controls = Controls.StaticControls;
        animator = GetComponent<Animator>();
	    spriteRenderer = GetComponent<SpriteRenderer>();
	    rigidbody = GetComponent<Rigidbody2D>();

	    playerUi = GetComponent<PlayerUi>();
	}
	
	void FixedUpdate () {
	    if (controls.TeleportButton)
	    {
            if(cannotTeleport) return;
	        currentlyTeleporting = true;
	        cannotTeleport = true;
            timerManager.CreateTimer(TeleportTime, timer => currentlyTeleporting = false);
            timerManager.CreateTimer(NonTeleportTime, timer => cannotTeleport = false);
	        playerUi.teleportingBar.progression = 0;
            timerManager.CreateTimer(NonTeleportTime/10, timer => playerUi.teleportingBar.progression = timer.RepeatedCount, true, 10);

	        teleportDirection = controls.LastDirection;

	        StartCoroutine("TeleportAnimation");
	    }
	    if (currentlyTeleporting)
	    {
            rigidbody.velocity = new Vector2(teleportDirection.x*TeleportSpeed*Time.deltaTime, -teleportDirection.y*TeleportSpeed * Time.deltaTime);
	    }
	    else
	    {
	        //direction = direction.normalized;
	        Vector2 direction = controls.CurrentDirection * Speed * Time.deltaTime;
	        rigidbody.velocity = new Vector2(Mathf.Lerp(0, direction.x, 0.8f), Mathf.Lerp(0, direction.y, 0.8f));

            if (direction == Vector2.zero)
            {
                animator.SetBool("walking", false);
            }
            else
            {
                animator.SetBool("walking", true);
                animator.SetFloat("x", direction.x);
                animator.SetFloat("y", direction.y);
            }
           
        }
	}

    private IEnumerator TeleportAnimation()
    {
        lastSprite = GetComponent<SpriteRenderer>().sprite;
        lastPosition = transform.position;
        while (currentlyTeleporting)
        {
            GameObject shadow = Instantiate(PlayerShadow);
            shadow.transform.position = lastPosition;
            shadow.GetComponent<SpriteRenderer>().sprite = lastSprite;
            timerManager.CreateTimer(0.3f, timer1 => Destroy(shadow));

            lastSprite = spriteRenderer.sprite;
            lastPosition = transform.position;
            yield return new WaitForSeconds(0.02f);
        }
    }

 
}
