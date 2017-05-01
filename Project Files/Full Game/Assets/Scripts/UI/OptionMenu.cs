using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
	private readonly string RESOLUTION_WIDHT_KEY = "RESOLUTION_WIDTH";
	private readonly string RESOLUTION_HEIGHT_KEY = "RESOLUTION_HEIGHT";
	private readonly string RESOLUTION_REFRESHREATE_KEY = "RESOLUTION_REFRESHRATE";
	private readonly string FULLSCREEN_KEY = "FULLSCREEN";
	private readonly string ANTIALIASING_KEY = "ANTIALIASING";

	public Dropdown resolutionDropdown;
	public Toggle fullscreenToggle, antiAliasingToggle;

	private int resolutionIndex;
	private Resolution resolution;

	private bool fullscreen;

	private bool antiAliasing;

	// Use this for initialization
	void Start ()
	{

	}

	private void OnEnable()
	{
		Load();
		List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
		foreach (var res in Screen.resolutions)
		{
			options.Add(new Dropdown.OptionData(res.ToString()));
		}
		resolutionDropdown.options = options;
		resolutionDropdown.value = resolutionIndex;

		fullscreenToggle.isOn = fullscreen;

		antiAliasingToggle.isOn = antiAliasing;
	}


	// Update is called once per frame
	void Update () {
		
	}

	public void Resolution_DropDown(int value)
	{

		if (value >= Screen.resolutions.Length)
		{
			Debug.LogError("Resolution '" + value + "' index doesn't exist!");
			return;
		}
		resolution = Screen.resolutions[value];
	}

	public void Fullscreen_Toggle()
	{
		fullscreen = fullscreenToggle.isOn;
	}


	public void AntiAliasing_Toggle()
	{
		antiAliasing = antiAliasingToggle.isOn;
	}

	public void Apply_Btn()
	{
		StartCoroutine(ChangeSettings());
		Save();
		MenuManager.sMainMenu.RevertMenu();

	}

	public void Back_Btn()
	{
		MenuManager.sMainMenu.RevertMenu();
	}

	void Save()
	{
		PlayerPrefs.SetInt(RESOLUTION_WIDHT_KEY, resolution.width);
		PlayerPrefs.SetInt(RESOLUTION_HEIGHT_KEY, resolution.height);
		PlayerPrefs.SetInt(RESOLUTION_REFRESHREATE_KEY, resolution.refreshRate);
		PlayerPrefs.SetInt(FULLSCREEN_KEY, fullscreen?1:0);
		PlayerPrefs.SetInt(ANTIALIASING_KEY, antiAliasing?1:0);
		PlayerPrefs.Save();
	}

	void Load()
	{
		int resolutionWidth = PlayerPrefs.GetInt(RESOLUTION_WIDHT_KEY);
		int resolutionHeight = PlayerPrefs.GetInt(RESOLUTION_HEIGHT_KEY);
		int resolutionRefreshrate = PlayerPrefs.GetInt(RESOLUTION_REFRESHREATE_KEY);
		Debug.Log("load " + resolutionWidth + "x" + resolutionHeight + "@" + resolutionRefreshrate);

		int i = 0;
		resolutionIndex = -1;
		foreach (var res in Screen.resolutions)
		{
			if (res.width == resolutionWidth & res.height == resolutionHeight & res.refreshRate == resolutionRefreshrate)
			{
				resolutionIndex = i;
				//Debug.Log(res.width + "=" + resolutionWidth + ", " + res.height + "=" + resolutionHeight + ", " + res.refreshRate + "=" + resolutionRefreshrate);
			}
			i++;
		}
		if (resolutionIndex == -1)
		{
			resolution = Screen.resolutions[0];
			resolutionIndex = 0;
			Debug.Log("loaded resolution isnt' avaible. " + resolution.ToString() + " is taken instate.");
		}
		else
		{
			Debug.Log("resolution index: " + resolutionIndex + " = " + Screen.resolutions[resolutionIndex]);
		}


		fullscreen = PlayerPrefs.GetInt(FULLSCREEN_KEY) != 0;
		antiAliasing= PlayerPrefs.GetInt(ANTIALIASING_KEY) != 0;

		Debug.Log("Fullscreen: " + fullscreen);
		Debug.Log("Anti-Aliasing: " + antiAliasing);

		StartCoroutine(ChangeSettings());
	}

	IEnumerator ChangeSettings()
	{
		Screen.fullScreen = fullscreen;

		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();

		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();

		QualitySettings.antiAliasing = antiAliasing ? 1 : 0;
	}
}
