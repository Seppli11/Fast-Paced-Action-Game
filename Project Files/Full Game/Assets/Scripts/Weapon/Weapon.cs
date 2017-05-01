
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {
    [Space(20)]
	//public RuntimeAnimatorController runtimeAnimatorController;
	[HideInInspector] public Animator animator; //set in WeaponFactory
	public bool attackAnimationIsBool = false;
	public string attackAnimation = "attack";
	public string reloadAnimation = "reload";

	public float timeToAttack = 0f;
	//public WeaponType weaponType;
	[HideInInspector] public GameObject owner;//set in WeaponFactory
	public int damage; 

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
	[HideInInspector] public Movement movement; //set in factory

	public Upgrade[] avaibleUpgrades;

	public override void UpdateHowerText()
	{
		itemDescriptionLines.Clear(); //clears last UpdateHoverText() lines

		newLine = "Schaden: " + damage;
		newLine = "Angriffs Zeit: " + attackWaitTime;
		if(hasToReload)
		{
			newLine = "Munition: " + loadedAmmo + "/" + maxAmmoInMagasin;
			if(!hasInfinitAmmo)
			{
				newLine = "Gesammte Munition: " + ammoInMagasin;
			}
		}

		base.UpdateHowerText(); //calls the base method in which the string gets compiled
	}

	private void Start()
	{
		base.Start();
		foreach(var u in avaibleUpgrades)
		{
			SetWeaponToUpgrade(u);
		}
	}

	private void FixedUpdate()
	{
		if (movement != null)
		{
			//is used to copy the location of the parent to the colider
			float angle = Mathf.Atan2(-movement.rotation.x, movement.rotation.y) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
	}

	/// <summary>
	/// Attacks with the setted Type (AttackType).
	/// </summary>
	/// <returns>
	///		Returns an AttackReturn enum. <seealso cref="AttackReturn"/>
	/// </returns>
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
		lastAttackTime = Time.time;
		//animator.runtimeAnimatorController = runtimeAnimatorController;
		StartCoroutine(Private_Attack());
		return AttackReturn.Attacked;
	}

	private IEnumerator Private_Attack()
	{
		float startAttack = Time.time;
		while (startAttack + timeToAttack > Time.time) yield return new WaitForSeconds(0.1f);
		Debug.Log("Start Attacking");
		if (attackAnimationIsBool)
		{
			animator.SetBool(attackAnimation, true);
			TimerManager.STimerManager.CreateTimer(attackTime, (t) => {
				if (animator != null)
					animator.SetBool(attackAnimation, false);
			});
		}
		else
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
	}

	/// <summary>
	/// Reloads weapon. 
	/// </summary>
	/// <returns>Returns ReloadReturn <seealso cref="ReloadReturn"/></returns>
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

		//animator.runtimeAnimatorController = runtimeAnimatorController;
		animator.SetTrigger(reloadAnimation);

		lastReloadTime = Time.time;
		return ReloadReturn.Reloaded;
	}

	public void Buy(ref Upgrade u)
	{
		if(u.currentStep >= u.maxSteps-1)
		{
			return;
		}
		u.currentStep++;
		switch(u.upgrade)
		{
			case UpgradeType.AttackTime:
				attackWaitTime += u.increasePerStep;
				u.currentValue = attackWaitTime;
				break;
			case UpgradeType.ColiderSize:
				//todo colider size
				break;
			case UpgradeType.Damage:
				damage += (int) u.increasePerStep;
				u.currentValue = damage;
				break;
			case UpgradeType.MaxAmmoInMagasin:
				maxAmmoInMagasin += (int)u.increasePerStep;
				u.currentValue = maxAmmoInMagasin;
				break;
			case UpgradeType.MaxLoadedAmmo:
				maxLoadedAmmo += (int)u.increasePerStep;
				u.currentValue = maxLoadedAmmo;
				break;
			case UpgradeType.RaycastRange:
				raycastLength += u.increasePerStep;
				u.currentValue = raycastLength;
				break;
			case UpgradeType.ReloadTime:
				reloadTime += u.increasePerStep;
				u.currentValue = reloadTime;
				break;
		}
	}

	public void SetUpgrade(ref Upgrade u)
	{
		switch (u.upgrade)
		{
			case UpgradeType.AttackTime:
				u.currentValue = attackWaitTime;
				break;
			case UpgradeType.ColiderSize:
				//todo colider size
				break;
			case UpgradeType.Damage:
				u.currentValue = damage;
				break;
			case UpgradeType.MaxAmmoInMagasin:
				u.currentValue = maxAmmoInMagasin;
				break;
			case UpgradeType.MaxLoadedAmmo:
				u.currentValue = maxLoadedAmmo;
				break;
			case UpgradeType.RaycastRange:
				u.currentValue = raycastLength;
				break;
			case UpgradeType.ReloadTime:
				u.currentValue = reloadTime;
				break;
		}
	}

	public void SetWeaponToUpgrade(Upgrade u)
	{
		switch (u.upgrade)
		{
			case UpgradeType.AttackTime:
				attackWaitTime = u.currentValue;
				break;
			case UpgradeType.ColiderSize:
				//todo colider size
				break;
			case UpgradeType.Damage:
				damage =(int) u.currentValue;
				break;
			case UpgradeType.MaxAmmoInMagasin:
				maxAmmoInMagasin = (int) u.currentValue;
				break;
			case UpgradeType.MaxLoadedAmmo:
				maxLoadedAmmo = (int)u.currentValue;
				break;
			case UpgradeType.RaycastRange:
				raycastLength = u.currentValue;
				break;
			case UpgradeType.ReloadTime:
				reloadTime = u.currentValue;
				break;
		}
	}

	public override void PickedUp(GameObject owner) {
		this.owner = owner;
		this.animator = owner.GetComponent<Animator>();
		if(movement == null)
			this.movement = owner.GetComponent<Movement>();
		transform.SetParent(owner.transform);
		transform.localPosition = Vector2.zero;
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
			//Debug.Log(h.gameObject.name);
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

	new private void OnTriggerEnter2D(Collider2D collision)
	{
		base.OnTriggerEnter2D(collision);
		if (laysOnGround) return;
		Hittable h = collision.gameObject.GetComponent<Hittable>();
		if (h != null) {
			if (h.GetComponent<Side>().enemy != owner.GetComponent<Side>().enemy)
			{
				if(!hittable.Contains(h))
					hittable.Add(h);
			}
			else if (!owner.GetComponent<Side>().enemy)
				Debug.Log(collision.gameObject.name + " is on the player's side");
		}
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
				if (owner.GetComponent<Side>().enemy != hit.collider.gameObject.GetComponent<Side>().enemy)
				{
					if(!owner.GetComponent<Side>().enemy) ComboCounter.comboCounter.RegisterHit();
					h.Hitted(owner, this);
				} 
			}
		}
	}
#endregion
}


/*public enum WeaponType
{
	EmptyHand,
	Sword,
	LickAttack,
	Unknown
}
*/
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

[Serializable]
public enum UpgradeType
{
	Damage,
	RaycastRange,
	ColiderSize,
	MaxLoadedAmmo,
	MaxAmmoInMagasin,
	AttackTime,
	ReloadTime
}

[Serializable]
public class Upgrade
{
	public UpgradeType upgrade;
	public string upgradeName;
	public float baseValue;
	public int currentStep;
	public int maxSteps;
	public float increasePerStep;
	public int costPerStep;
	public int baseCost;

	public float currentValue;

	public int currentCost
	{
		get
		{
			return baseCost + (currentStep * costPerStep);
		}
	}

	public Upgrade(UpgradeType upgrade, string upgradeName, float baseValue, int currentStep, int maxSteps, int increasePerStep, int costPerStep, int baseCost, int currentValue = 0)
	{
		this.upgrade = upgrade;
		this.upgradeName = upgradeName;
		this.baseValue = baseValue;
		this.currentStep = currentStep;
		this.maxSteps = maxSteps;
		this.increasePerStep = increasePerStep;
		this.costPerStep = costPerStep;
		this.baseCost = baseCost;
		this.currentValue = currentValue;
	}

}