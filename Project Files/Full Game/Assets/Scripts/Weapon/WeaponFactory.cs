using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory : MonoBehaviour {
	private static WeaponFactory _weaponFactory;
	public static WeaponFactory weaponFactory {
		get { return _weaponFactory; }
	}


	public GameObject emptyHandPrefab;
	public GameObject swordPrefab;
	public GameObject lickAttackPrefab;
	// Use this for initialization

	private void Awake()
	{
		_weaponFactory = GameObject.Find("WeaponFactory").GetComponent<WeaponFactory>();
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Weapon GetEmptyHand(GameObject parent)
	{
		Weapon wr = GetWeapon(parent, emptyHandPrefab);
		return wr;
	}

	public Weapon GetSword(GameObject parent, int damage, float attackWaitTime = 2)
	{
		Weapon wr = GetWeapon(parent, swordPrefab);
		wr.damage = damage;
		wr.attackWaitTime = attackWaitTime;
		return wr;
	}

	public Weapon GetLickAttack(GameObject parent, int damage)
	{
		Weapon wr = GetWeapon(parent, lickAttackPrefab);
		wr.damage = damage;
		return wr;
	}

	private Weapon GetWeapon(GameObject parent, GameObject prefab)
	{
		GameObject go = Instantiate(prefab, parent.transform, true);
		go.transform.position = parent.transform.position;

		Weapon wr = go.GetComponent<Weapon>();
		if(wr == null)
		{
			Debug.LogError("WeaponPrefab is null");
			return null;
		}

		wr.owner = parent;
		wr.animator = parent.GetComponent<Animator>();
		return wr;
	}
}
