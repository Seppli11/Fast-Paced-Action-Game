using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
	public RuntimeAnimatorController runtimeAnimatorController;
	public Animator animator;
	public string attackAnimation = "attack";
	public string reloadAnimation = "reload";

	public string name;
	public WeaponType weaponType;
	public GameObject owner;
	public int damage;
	public Sprite WeaponSprite;

	[Space(10)]
	public WeaponAttackType attackType;

	public float raycastLength;
	public GameObject prefab;
	

	[Space(20)]
	public bool hasToReload;
	public bool hasInfinitAmmo;
	public int loadedAmmo;
	public int maxLoadedAmmo;
	public int ammoInMagasin;
	public int maxAmmoInMagasin;

	[Space(20)]
	public float attackTime;
	public float attackWaitTime;
	public float reloadTime;

	private float lastAttackTime;
	private float lastReloadTime;
	private List<Hittable> hittable = new List<Hittable>();
	private Movement movement;

	// Use this for initialization
	void Start () {
		movement = owner.GetComponent<Movement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void FixedUpdate()
	{
		if (movement != null)
		{
			float angle = Mathf.Atan2(-movement.rotation.x, movement.rotation.y) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
	}

	public AttackReturn Attack()
	{
		if (lastAttackTime + attackTime > Time.time) return AttackReturn.Attacking;
		if (lastAttackTime + attackWaitTime > Time.time) return AttackReturn.Waiting;
		if(hasToReload)
		{
			if(loadedAmmo <= 0)
			{
				if (!hasInfinitAmmo) return AttackReturn.NeedToReload;
			}
		}

		animator.runtimeAnimatorController = runtimeAnimatorController;
		animator.SetTrigger(attackAnimation);

		switch (attackType)
		{
			case WeaponAttackType.Collider:
				ColiderAttack();
				break;
			case WeaponAttackType.Prefab:
				PrefabAttack();
				break;
			case WeaponAttackType.Raycast:
				RaycastAttack();
				break;
		}
		lastAttackTime = Time.time;
		return AttackReturn.Attacked;
	}

	public ReloadReturn Reload()
	{
		if (!hasToReload) return ReloadReturn.Reloaded;
		if (lastReloadTime + reloadTime > Time.time) return ReloadReturn.Reloading;
		if (ammoInMagasin <= 0) return ReloadReturn.OutOfAmmo;
		if(!hasInfinitAmmo)
		{
			int neededAmmo = maxLoadedAmmo - loadedAmmo;
			if(neededAmmo >= ammoInMagasin)
			{
				loadedAmmo += ammoInMagasin;
				ammoInMagasin = 0;
			} else
			{
				loadedAmmo += neededAmmo;
				ammoInMagasin -= neededAmmo;
			}
		} else
		{
			loadedAmmo = maxLoadedAmmo;
		}

		animator.runtimeAnimatorController = runtimeAnimatorController;
		animator.SetTrigger(reloadAnimation);

		lastReloadTime = Time.time;
		return ReloadReturn.Reloaded;
	}

	public void Destroy()
	{
		Destroy();
	}


	#region colider attack
	private void ColiderAttack()
	{
		List<Hittable> hittableToDelete = new List<Hittable>();
		foreach(Hittable h in hittable)
		{
			if (h == null)
			{
				hittableToDelete.Add(h);
				continue;
			}
			if (h.gameObject == null)
			{
				hittableToDelete.Add(h);
				continue;
			}
		}
		foreach(Hittable h in hittableToDelete)
		{
			hittable.Remove(h);
		}
		foreach(Hittable h in hittable)
		{
			h.Hitted(owner, this);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Hittable h = collision.gameObject.GetComponent<Hittable>();
		if (h != null) hittable.Add(h);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		Hittable h = collision.gameObject.GetComponent<Hittable>();
		if (h != null) hittable.Remove(h);
	}
	#endregion

#region prefab attack
	private void PrefabAttack()
	{
		if (prefab == null) {
			Debug.LogError("Prefab is null!");
			return;
		}
		Instantiate(prefab);
	}
	#endregion

#region Raycast Attack
	private void RaycastAttack()
	{
		RaycastHit2D hit = Physics2D.Raycast(owner.transform.position, transform.rotation * Vector2.up, raycastLength);
		if(hit.collider != null)
		{
			Hittable h = hit.collider.gameObject.GetComponent<Hittable>();
			if(h != null)
			{
				h.Hitted(owner, this);
			}
		}
	}
#endregion
}


public enum WeaponType
{
	EmptyHand,
	Sword,
	LickAttack,
	Unknown
}

public enum WeaponAttackType
{
	Raycast,
	Collider,
	Prefab
}

public enum AttackReturn
{
	Attacked,
	Attacking,
	Waiting,
	NeedToReload
}

public enum ReloadReturn
{
	Reloaded,
	Reloading,
	OutOfAmmo
}
