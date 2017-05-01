using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryInfo : MonoBehaviour {
	public static InventoryInfo inventoryInfo;

	private int _currencyLeft = 0;
	public int currencyLeft {
		get { return _currencyLeft;  }
		set
		{
			_currencyLeft = value;
		}
	}

	public static float animationCurrencyLeft
	{
		get;
		private set;
	}

	public Text[] currencyLeftText;

	private void Awake()
	{
		inventoryInfo = this;
		StartCoroutine(CurrencyLeftAnimation());
	}

	IEnumerator CurrencyLeftAnimation()
	{
		int lastCurrencyLeft = currencyLeft;
		bool missingNegative = false;
		while (true)
		{
			lastCurrencyLeft = currencyLeft;
			//Debug.Log("currencyLeft:" + currencyLeft + ", animationCurrencyLeft:" + animationCurrencyLeft);
			missingNegative = currencyLeft - animationCurrencyLeft < 0;
			float steps = (currencyLeft - animationCurrencyLeft) / 10;
			while(animationCurrencyLeft != currencyLeft)
			{
				animationCurrencyLeft = animationCurrencyLeft + steps;
				
				foreach (var t in currencyLeftText) t.text = animationCurrencyLeft + " Score";
				//Debug.Log("currencyLeft:" + currencyLeft + ", animationCurrencyLeft:" + animationCurrencyLeft + ", steps: " + steps);
				if(lastCurrencyLeft != currencyLeft)
				{
					break;
				}
				yield return new WaitForSecondsRealtime(0.05f);
			}
			_currencyLeft = Mathf.FloorToInt(animationCurrencyLeft);
			//yield return new WaitForSeconds(0.2f);
			yield return new WaitForSecondsRealtime(0.2f);
		}
	}
}
