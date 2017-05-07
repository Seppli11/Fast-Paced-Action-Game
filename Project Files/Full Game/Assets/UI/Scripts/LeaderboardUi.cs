using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardUi : MonoBehaviour {
	public GameObject leaderboardEntryPrefab;
	public Transform leaderboardEntrySpawnPoint;
	// Use this for initialization
	void Start () {
		Leaderboard.DownloadedHighscoreEvent += UpdateLeaderboard;
		Leaderboard.sLeaderboard.DownloadHighscores();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateLeaderboard()
	{
		for(int i = 0; i < leaderboardEntrySpawnPoint.childCount; i++)
		{
			Destroy(leaderboardEntrySpawnPoint.GetChild(i).gameObject);
		}
		Highscore[] highscoreList = Leaderboard.sLeaderboard.highscoreList;
		print(highscoreList.Length);
		foreach(Highscore score in highscoreList)
		{
			GameObject go = Instantiate(leaderboardEntryPrefab, leaderboardEntrySpawnPoint, false);
			LeaderboardEntry e = go.GetComponent<LeaderboardEntry>();
			e.name.text = score.username;
			e.score.text = score.score.ToString();
		}
	}
}
