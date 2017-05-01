using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class LanguageAttribute : Attribute
{
	public string path
	{
		get;
		private set;
	}

	public string description
	{
		get;
		private set;
	}

	public LanguageAttribute(string path = null, string description = "")
	{
		this.path = path;
		this.description = description;
	}
}
