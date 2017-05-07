using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour {
	public static ProgressManager progressManager
	{
		get;
		private set;
	}

	public Text roundText;
	public Text enemiesText;

	public Transform parentOfEnemies;

	[Space(20)]
	public RoundEnemy[] enemies;

	public int currentRoundIndex = 0;
	[HideInInspector]
	public Round currentRound;

	public int killedEnemies = 0;

	private List<RoundEnemyEntry> queue = new List<RoundEnemyEntry>();

	private void Awake()
	{
		progressManager = this;
	}


	private void Start()
	{
		currentRound = new Round(0, enemies);

		roundText.text = "Round " + (currentRoundIndex + 1);
		enemiesText.text = (killedEnemies) + " / " + currentRound.enemiesForThisRound + " Gegnern";

		StartCoroutine(SpawnEnemies());
	}

	private void Update()
	{
		if(queue.Count == 0) {
			//Debug.Log(currentRound.roundEnemyEntries.Length) ;
			foreach (var e in currentRound.roundEnemyEntries)
			{
				
				//count enemies on the map
				int enemiesOnMap = 0;
				for (int i = 0; i < parentOfEnemies.childCount; i++)
				{
					EnemyId id = parentOfEnemies.GetChild(i).GetComponent<EnemyId>();
					if (id == null)
					{
						Debug.LogError("'" + parentOfEnemies.GetChild(i).name + "' has no EnemyId.");
						continue;
					}

					if (id.id == e.id)
					{
						//Debug.Log("add enemy");
						enemiesOnMap++;
					}
				}

				if (enemiesOnMap <= e.minEnemiesOnMap)
				{
					int diff = e.maxEnemiesOnMap - enemiesOnMap;
					if((enemiesOnMap + killedEnemies) + diff >= currentRound.enemiesForThisRound)
					{
						diff = e.enemiesForThisRound - (enemiesOnMap + killedEnemies);
					}
					for (int i = 0; i < diff; i++)
					{
						if (UnityEngine.Random.value <= e.probabilityToSpawn) {
							queue.Add(e);
						} 
					}
				}
			}
		}
		

		roundText.text = "Round " + (currentRoundIndex + 1);
		enemiesText.text = (killedEnemies) + " / " + currentRound.enemiesForThisRound + " Gegnern";
		if (killedEnemies >= currentRound.enemiesForThisRound)
		{
			NextRound();
		}
	}

	private IEnumerator SpawnEnemies()
	{
		while(true)
		{
			yield return new WaitForEndOfFrame();
			queue.Shuffle();
			foreach(var e in queue)
			{
				var go = Instantiate(e.enemyPrefab, parentOfEnemies, true);
				go.transform.position = parentOfEnemies.transform.position;
				var movement = go.GetComponent<Movement>();
				if(movement != null)
				{
					movement.multiplier = UnityEngine.Random.Range(0.6f, 1.4f);
				}
				yield return new WaitForSeconds(e.spawnWaitTime);
			}
			queue.Clear();
		}
	}

	public void NextRound()
	{
		killedEnemies = 0;

		currentRoundIndex++;
		currentRound = new Round(currentRoundIndex, enemies);
		roundText.text = "Round " + (currentRoundIndex+1);
		enemiesText.text = (killedEnemies) +  " / " + currentRound.enemiesForThisRound + " Gegnern";
	}

}

[Serializable]
public class Round
{
	public RoundEnemyEntry[] roundEnemyEntries;
	public int enemiesForThisRound
	{
		get
		{
			if (roundEnemyEntries == null) return 0;
			int r = 0;
			foreach(var ree in roundEnemyEntries)
			{
				//Debug.Log(ree);
				r += ree.enemiesForThisRound;
			}
			return r;
		}
	}

	public Round(int round, RoundEnemy[] enemies)
	{
		List<RoundEnemyEntry> entries = new List<RoundEnemyEntry>();
		int i = 0;
		foreach(var e in enemies)
		{
			var tempRoundEnemyEntry = e.ToRoundEnemyEntry(round);
			if (tempRoundEnemyEntry != null) entries.Add(tempRoundEnemyEntry);
		}

		roundEnemyEntries = entries.ToArray();
	}

	
}

[Serializable]
public class RoundEnemyEntry
{
	public GameObject enemyPrefab;
	public int id;
	public int maxEnemiesOnMap;
	public int minEnemiesOnMap;
	public int enemiesForThisRound;
	public float spawnWaitTime;
	public float probabilityToSpawn;

	public void SetValues(ref GameObject go)
	{
		
	}
}

[Serializable]
public class RoundEnemy
{
	public GameObject enemyPrefab;
	public int spawnInRound = 0;

	public int startMaxEnemy;
	public int startMinEnemy;

	public int startEnemiesForThisRound;

	public int increaseMaxMinEnemiesPerRound = 1;
	public int increaseEnemyPerRound = 5;
	public float spawnWaitTime;

	public float probabilityToSpawn = 0.5f;

	public EnemyId id
	{
		get
		{
			return enemyPrefab.GetComponent<EnemyId>();
		}
	}

	public RoundEnemyEntry ToRoundEnemyEntry(int round)
	{
		if (round < spawnInRound) return null;
		RoundEnemyEntry r = new RoundEnemyEntry();
		r.enemyPrefab = enemyPrefab;
		EnemyId id = enemyPrefab.GetComponent<EnemyId>();
		if(id == null)
		{
			Debug.LogError("Prefab '" + enemyPrefab.name + "' has no EnemyId.");
			return null;
		}
		r.id = id.id;
		r.maxEnemiesOnMap = startMaxEnemy + (round * increaseMaxMinEnemiesPerRound);
		r.minEnemiesOnMap = startMinEnemy + (round * increaseMaxMinEnemiesPerRound);
		r.enemiesForThisRound = startEnemiesForThisRound + (round * increaseEnemyPerRound);
		r.spawnWaitTime = spawnWaitTime;
		r.probabilityToSpawn = probabilityToSpawn;

		return r;
	}
}



public static class ThreadSafeRandom
{
	[ThreadStatic] private static System.Random Local;

	public static System.Random ThisThreadsRandom
	{
		get { return Local ?? (Local = new System.Random(unchecked(Environment.TickCount * 31 + System.Threading.Thread.CurrentThread.ManagedThreadId))); }
	}
}

static class MyExtensions
{
	public static void Shuffle<T>(this IList<T> list)
	{
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}
}
