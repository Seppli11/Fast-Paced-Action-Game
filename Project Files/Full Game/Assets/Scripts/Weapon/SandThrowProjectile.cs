using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandThrowProjectile : Projectile {
	public float bluryTime;

	new private void Start()
	{
		base.Start();
		hitEvents += SandThrowProjectile_hitEvents;
	}

	private void SandThrowProjectile_hitEvents(GameObject attacker, Weapon weapon, Hurtable hurtable)
	{
		if(hurtable.gameObject.GetComponent<Player>() != null)
		{
			MenuManager.sMainMenu.ChangeCamera(MenuManager.sMainMenu.bluryCamera);
			TimerManager.STimerManager.CreateTimer(bluryTime, t => {
				MenuManager.sMainMenu.ChangeCamera(MenuManager.sMainMenu.mainCamera);
			});
		}
	}
}
