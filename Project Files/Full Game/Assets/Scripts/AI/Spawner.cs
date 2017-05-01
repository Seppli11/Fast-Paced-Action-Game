using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject spawnObject;

	public bool ifPlayerIsInRange;
	public float rangeToPlayer;

	public bool random;

	public float probability;

	public float fixedDelay;

	public float spawnRadius;

	private float lastSpawn;
	// Use this for initialization
	void Start ()
	{
		lastSpawn = Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(ifPlayerIsInRange)
		{
			//Debug.Log(Vector2.Distance(Vector2.Max(Player.player.transform.position, transform.position), Vector2.Min(Player.player.transform.position, transform.position)));
			if (Vector2.Distance(Vector2.Max(Player.player.transform.position, transform.position), Vector2.Min(Player.player.transform.position, transform.position)) > rangeToPlayer)
				return;
		}
		if (random)
		{
			float f = Random.Range(0f, 1f);
			if (f <= probability)
			{
				Spawn();
			}
		}
		else
		{
			if (Time.time - lastSpawn >= fixedDelay)
			{
				Spawn();
				lastSpawn = Time.time;
			}
		}

	}

	private void Spawn()
	{
		Instantiate(spawnObject, (new Vector2(transform.position.x, transform.position.y)+ (Random.insideUnitCircle * spawnRadius)), Quaternion.identity, transform);
	}
}
