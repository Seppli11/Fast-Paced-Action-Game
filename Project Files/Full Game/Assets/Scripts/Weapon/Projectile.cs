using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Hit(GameObject attacker, Weapon weapon, Hurtable hurtable);

public class Projectile : MonoBehaviour, DamageDealer {
	public event Hit hitEvents;

	public float lifeTime;
	public float speed;
	[HideInInspector]
	public Vector2 direction;
	public int damage;

	public bool destroyOnHit;

	[HideInInspector]
	public Weapon baseWeapon;
	[HideInInspector]
	public GameObject owner;


	private Animator animator;

	// Use this for initialization
	protected void Start () {
		TimerManager.STimerManager.CreateTimer(lifeTime, t => {
			if(this != null)
				Destroy(gameObject);
			});
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	protected void Update () {
		if (animator != null)
		{
			animator.SetFloat("x", direction.x);
			animator.SetFloat("y", direction.y);
		}
	}

	public void Setup(GameObject owner, Weapon weapon, Vector2 direction)
	{
		this.owner = owner;
		this.baseWeapon = weapon;
		this.direction = direction;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (owner == null) return;
		Hittable h = collision.gameObject.GetComponent<Hittable>();
		if (h != null)
		{
			if (h.GetComponent<Side>().enemy != owner.GetComponent<Side>().enemy)
			{
				h.Hitted(owner, this);
				hitEvents(owner, baseWeapon, h as Hurtable);
			}
			else if (!owner.GetComponent<Side>().enemy)
				Debug.Log(collision.gameObject.name + " is on the player's side");
		}
	}

	public int GetDamage()
	{
		return damage;
	}
}
