using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
	public static readonly string prefix = "!!";

	public static LanguageManager languageManager;
	public static Dictionary<string, string> language = new Dictionary<string, string>();

	private static Language _currentLanguage;
	public static Language currentLanguage
	{
		get { return _currentLanguage; }
		set
		{
			_currentLanguage = value;
			languageManager.Load();
		}
	}


	private void Awake()
	{
		if (languageManager == null)
		{
			languageManager = this;
			DontDestroyOnLoad(gameObject);
			SceneManager.sceneLoaded += SceneManager_sceneLoaded;
		} else
		{
			Destroy(gameObject);
		}

	}

	private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
	{
		Load();
	}

	// Use this for initialization
	void Start () {
		currentLanguage = Language.English;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Load()
	{
		language.Clear();
		using(var fs = File.OpenRead(Application.dataPath+ "/langFiles/" + Language.English.filePath))
		using (var reader = new StreamReader(fs))
		{
			var sceneName = "";
			while (!reader.EndOfStream)
			{
				var line = reader.ReadLine().Trim();
				if(line.Equals("")) continue;
				if (line.StartsWith("#")) continue;

				var values = line.Split(',');
				if (values.Length != 3)
				{
					Debug.LogError("Language-file '" + currentLanguage.filePath +
					               "' isn't a valide language file! values does not match(" + line + ")");
					return;
				}
				if(values[0].Trim() == "") continue;
				language.Add(values[0].Trim(), values[2].Trim());
				//Debug.Log("Add value " + values[0] + " - " + values[1] + " to " + sceneName);
			}
		}
		ReplaceStrings();
	}

	void ReplaceStrings()
	{
		foreach (var l in language)
		{
			string k = l.Key;
			MonoBehaviour comp = GetMonoBehaviour(k);
			if(comp != null)
				(comp as Text).text = l.Value;
		}
	}

	MonoBehaviour GetMonoBehaviour(string path)
	{
		string[] splitted = path.Split('.');
		if (splitted.Length < 2) return null;
		Scene s = SceneManager.GetSceneByName(splitted[0]);
		if (!s.Equals(SceneManager.GetActiveScene())) return null;
		GameObject[] root = s.GetRootGameObjects();
		Transform currentGo = null;
		foreach (var go in root)
		{
			if (go.name.Equals(splitted[1]))
			{
				if (currentGo != null)
				{
					Debug.LogError("Multiple GameObject with the same name (" + splitted[1] + ")");
					return null;
				}
				currentGo = go.transform;
			}
		}
		for (int i = 2; i < splitted.Length-1; i++)
		{
			currentGo = currentGo.FindChild(splitted[i]);
			if (currentGo == null)
			{
				Debug.LogError("Path '" + path + "' doesn't exist. Stuck at " + i + "(" + splitted[i] + ")");
				return null;
			}
		}

		return currentGo.gameObject.GetComponent((splitted[splitted.Length - 1])) as MonoBehaviour;
	}

}

public class Language
{
	public static readonly Language English = new Language("en", "en.csv");
	public static readonly Language German = new Language("de", "de.csv");

	public string shortId;
	public string filePath;

	private Language(string shortId, string filePath)
	{
		this.shortId = shortId;
		this.filePath = filePath;
	}
}
