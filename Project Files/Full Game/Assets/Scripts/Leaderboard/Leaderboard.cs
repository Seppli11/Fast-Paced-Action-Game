using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DownloadedHighscore();

public class Leaderboard : MonoBehaviour {
	public static string currentUserName = "Test";

	public static Leaderboard sLeaderboard
	{
		get;
		private set;
	}

	public static event DownloadedHighscore DownloadedHighscoreEvent;

	const string privateCode = "AlfOaXX3pEqZTSwSnl5zAAQ6WqSlfzG0GHZ5M-tAKTnQ";
	const string publicCode = "590a02b64389dd024c82334c";
	const string webURL = "http://dreamlo.com/lb/";

	public Highscore[] highscoreList
	{
		get;
		protected set;
	}

	private void Awake()
	{
		if (sLeaderboard == null)
		{
			DontDestroyOnLoad(this);
			sLeaderboard = this;
			DownloadHighscores();
		} else
		{
			sLeaderboard.DownloadHighscores();
			Destroy(gameObject);
		}
	}

	public void AddNewHighscore(string username, int score)
	{
		StartCoroutine(UploadNewHighscore(username, score));
	}

	IEnumerator UploadNewHighscore(string username, int score)
	{
		WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
		yield return www;
		
		if(string.IsNullOrEmpty(www.error))
		{
			print("Upload Successful to '" + (webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score) + "'");
		} else
		{
			print("Error uploading: " + www.error);
		}
	}


	public void DownloadHighscores()
	{
		StartCoroutine(DownloadHighscoresFromDatabase());
	}

	IEnumerator DownloadHighscoresFromDatabase()
	{
		WWW www = new WWW(webURL + publicCode + "/pipe/");
		yield return www;

		if (string.IsNullOrEmpty(www.error))
		{
			FormatHighscores(www.text);
			if(DownloadedHighscoreEvent != null)
				DownloadedHighscoreEvent();
		}
		else
		{
			print("Error downloading : " + www.error);
		}
	}

	void FormatHighscores(string textStream)
	{
		string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
		highscoreList = new Highscore[entries.Length];
		for(int i = 0; i < entries.Length; i++)
		{
			string[] entryInfo = entries[i].Split(new char[] { '|' });
			highscoreList[i] = new Highscore();
			highscoreList[i].username = entryInfo[0];
			highscoreList[i].score = int.Parse(entryInfo[1]);
		}
	}
}

public struct Highscore
{
	public string username;
	public int score;

	public Highscore(string username, int score)
	{
		this.username = username;
		this.score = score;
	}
}
