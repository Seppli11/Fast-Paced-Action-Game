using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handels the playermovement and the animatino of the movement
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    private TimerManager timerManager;

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

	private Movement movement;

	public ProgressBar[] teleportProgressBars;
	private int MaxTeleportProgress;
	public int teleportProgress;

	public float loadTeleportProgressRatio;

	//roll dedection
	private bool currentlyRolling;
	private int rollState;
	private float lastRollStateChange;
	private Vector2 lastRollDirection;
	private Vector2 rollingDirection;

	[Space(20)]
	public float maxRollStateChangeTime = 0.2f;
	public float rollTime = 0.05f;
	public float RollingSpeed;

	void Start ()
	{
        timerManager = TimerManager.STimerManager;;
	    controls = Controls.StaticControls;
	    spriteRenderer = GetComponent<SpriteRenderer>();

		movement = GetComponent<Movement>();

		MaxTeleportProgress = teleportProgressBars.Length * 100;
		teleportProgress = MaxTeleportProgress;
	}
	
	void FixedUpdate () {
		if(teleportProgress < MaxTeleportProgress)
		{
			teleportProgress += (int)( loadTeleportProgressRatio * Time.deltaTime);
			UpdatingTeleportBars();
		}
		if (controls.TeleportButton)
		{
			if (teleportProgress < 100) return;
			if (cannotTeleport) return;
			currentlyTeleporting = true;
			cannotTeleport = true;
			timerManager.CreateTimer(TeleportTime, timer => currentlyTeleporting = false);
			timerManager.CreateTimer(NonTeleportTime, timer => cannotTeleport = false);
			teleportDirection = controls.LastDirection;
			StartCoroutine("TeleportBarDecreasingAnimation");
			StartCoroutine("TeleportAnimation");
		}

		if (currentlyTeleporting)
	    {
            //rigidbody2d.velocity = new Vector2(teleportDirection.x*TeleportSpeed*Time.deltaTime, -teleportDirection.y*TeleportSpeed * Time.deltaTime);
			movement.velocity = new Vector2(teleportDirection.x * TeleportSpeed * Time.deltaTime, -teleportDirection.y * TeleportSpeed * Time.deltaTime);
		}
		else
	    {
			//Debug.Log(new Vector2(Mathf.Round(controls.CurrentDirection.x), Mathf.Round(controls.CurrentDirection.y)));
	        Vector2 direction = controls.CurrentDirection * Speed * Time.deltaTime;
			movement.velocity = new Vector2(Mathf.Lerp(0, direction.x, 0.8f), Mathf.Lerp(0, direction.y, 0.8f));
		}

		
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (!currentlyTeleporting) return;
		if (collision.gameObject.GetComponent<Hittable>() == null) return;
		Collider2D collider = collision.collider;
		collider.enabled = false;
		timerManager.CreateTimer(0.4f, timer => { if (collider != null) collider.enabled = true; });
	}

	private void UpdatingTeleportBars()
	{
		for(int i = teleportProgressBars.Length-1; i >= 0; i--)
		{
			ProgressBar pb = teleportProgressBars[i];
			int progress = teleportProgress - (i * 100);
			pb.progression = progress;
		}
	}

	private IEnumerator TeleportBarDecreasingAnimation()
	{
		for(int i = 100; i >= 0; i-=10)
		{
			teleportProgress -= 10;
			yield return new WaitForSeconds(0.005f);
		}
	}

    private IEnumerator TeleportAnimation()
    {
		//colider.enabled = false;
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
            yield return new WaitForSeconds(0.01f);
        }
		//colider.enabled = true;
	}

 
}
