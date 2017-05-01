using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LanguageExporter : EditorWindow {
	private static List<string> lines = new List<string>();
	private static string sceneName = "";
	private static string path;
	private static string doneString = "";

	private static EditorWindow window;

	[MenuItem("Tools/Export Language File")]
	static void Export()
	{
		window = GetWindow(typeof(LanguageExporter));
	}

	private void OnGUI()
	{
		GUILayout.Label ("Language File Export Settings", EditorStyles.boldLabel);
		path = EditorGUILayout.TextField ("path in asset folder: ", path);

		if (GUILayout.Button("Start"))
		{
			StartExporting();
		}

		GUILayout.Label(doneString);
	}

	private static void StartExporting()
	{
		//string path = EditorUtility.OpenFilePanel("Save Language File", "", "");
		//if(path.Length == 0) return;

		for (int i = 0; i < SceneManager.sceneCount; i++)
		{
			Scene s = SceneManager.GetSceneAt(i);
			sceneName = s.name;
			lines.Add("");
			lines.Add("#" + sceneName);
			GameObject[] rootGos = s.GetRootGameObjects();
			foreach (var rootGo in rootGos)
			{
				//Debug.Log("Search in " + rootGo.name);
				SearchThroughGameObject(rootGo);
			}
		}

		path = Application.dataPath + "/" + path;
		Debug.Log("Write file to '" + path + "'");
		doneString = "Write file to '" + path + "'!";
		File.WriteAllText(path, String.Empty);
		using (StreamWriter file = new StreamWriter(path))
		{
			foreach (var line in lines)
			{
				file.WriteLine(line);
			}
			file.Close();
		}
		doneString = "File was written to '" + path + "'!";
		path = "";
		window.Close();
	}

	private static void SearchThroughGameObject(GameObject go)
	{
		MonoBehaviour[] components = go.GetComponents<MonoBehaviour>();
		//Debug.Log(components.Length);
		foreach (var component in components)
		{
			//Debug.Log("searching in component " + component.GetType().FullName;
			if (component is Text)
			{
				lines.Add(GetPath(component) + "," + (component as Text).text);
				Debug.Log("Found Attrib text " + GetPath(component));
			}

			PropertyInfo[] props = component.GetType().GetProperties();
			foreach (var prop in props)
			{
				object[] attrs = prop.GetCustomAttributes(true);
				foreach (var attr in attrs)
				{
					LanguageAttribute langAttr = attr as LanguageAttribute;
					if (langAttr != null)
					{
						string path = GetPath(component) + "." + prop.Name;
						if (langAttr.path != null) path += " - " + langAttr.path;
						lines.Add(langAttr.path);
						Debug.Log("Found Attrib on " + path);
					}
				}
			}

			FieldInfo[] fields = component.GetType().GetFields();
			foreach (var field in fields)
			{
				object[] attrs = field.GetCustomAttributes(true);
				foreach (var attr in attrs)
				{
					LanguageAttribute langAttr = attr as LanguageAttribute;
					if (langAttr != null)
					{
						string path = GetPath(component) + "." + field.Name;
						if (langAttr.path != null) path += " - " + langAttr.path;
						lines.Add(langAttr.path);
						Debug.Log("Found Field on " + path);
					}
				}
			}
		}

		for (int i = 0; i < go.transform.childCount; i++)
		{
			SearchThroughGameObject(go.transform.GetChild(i).gameObject);
		}
	}

	private static string GetPath(MonoBehaviour comp)
	{
		string path = comp.gameObject.name + "." + comp.name;
		Transform parent = comp.gameObject.transform;
		while (parent.parent != null)
		{
			parent = parent.parent;
			path = parent.name + "." + path;
		}
		return sceneName +"." + path;
	}
}

