using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory : MonoBehaviour {
	public static WeaponFactory weaponFactory {
		get;
		private set;
	}


	public GameObject swordPrefab;
	public GameObject lickAttackPrefab;
	public GameObject sandThrowAttackPrefab;
	// Use this for initialization

	private void Awake()
	{
		weaponFactory = this;
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Returns a sword with the setted parameters.
	/// </summary>
	/// <param name="parent">The parent of the weapon</param>
	/// <param name="damage">The damage of the sword</param>
	/// <param name="attackWaitTime">The time which should eleaps before the next attack is possible.</param>
	/// <returns></returns>
	public Weapon GetSword(GameObject parent, int damage, float attackWaitTime = 2, Movement movement = null)
	{
		Weapon wr = GetWeapon(parent, swordPrefab, movement);
		wr.damage = damage;
		wr.attackWaitTime = attackWaitTime;
		return wr;
	}

	/// <summary>
	/// Returns the LickAttack of the Kasa Obake.
	/// </summary>
	/// <param name="parent"></param>
	/// <param name="damage"></param>
	/// <returns></returns>
	public Weapon GetLickAttack(GameObject parent, int damage, Movement movement = null)
	{
		Weapon wr = GetWeapon(parent, lickAttackPrefab, movement);
		wr.damage = damage;
		return wr;
	}

	public Weapon GetSandThrowAttack(GameObject parent, int damage, Movement movement = null)
	{
		Weapon wr = GetWeapon(parent, sandThrowAttackPrefab, movement);
		wr.damage = damage;
		return wr;
	}

	/*public Weapon GetWeapon(GameControl.WeaponData weaponData, GameObject parent)
	{
		Weapon rWeapon = null;
		switch (weaponData.itemType.specificItemType){
			case SpecificItemType.LickAttack:
				rWeapon = GetLickAttack(parent, weaponData.damage);
				break;
			case SpecificItemType.Sword:
				rWeapon = GetSword(parent, weaponData.damage, weaponData.attackTime);
				break;
			default:
				Debug.LogError("SpecifiedItemType '" + weaponData.itemType.specificItemType + "' isn't supported!");
				return null;
		}
		rWeapon.damage = weaponData.damage;
		rWeapon.raycastLength = weaponData.raycastRange;
		rWeapon.maxLoadedAmmo = weaponData.maxLoadedAmmo;
		rWeapon.maxAmmoInMagasin = weaponData.maxAmmoInMagasin;
		rWeapon.attackWaitTime = weaponData.attackTime;
		rWeapon.reloadTime = weaponData.reloadTime;

		foreach (var u in weaponData.upgrades) rWeapon.SetWeaponToUpgrade(u);
		return rWeapon;
	}*/

	/// <summary>
	/// is the base function of every GetWeapon Function in the WeaponFactory <br/>
	/// It sets the position, the parent/owner and the animator.
	/// </summary>
	/// <param name="parent">the parent of the weapon</param>
	/// <param name="prefab">the prefab which will be instantiated.</param>
	/// <returns></returns>
	private Weapon GetWeapon(GameObject parent, GameObject prefab, Movement movement)
	{
		if (parent == null) parent = gameObject;
		GameObject go = Instantiate(prefab, parent.transform, true);
		go.transform.position = parent.transform.position;

		Weapon wr = go.GetComponent<Weapon>();
		if(wr == null)
		{
			Debug.LogError("WeaponPrefab is null");
			return null;
		}

		wr.laysOnGround = false;
		wr.owner = parent;
		wr.animator = parent.GetComponent<Animator>();
		wr.movement = movement ?? parent.GetComponent<Movement>();
		return wr;
	}
}
