using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiCoinsCounter : MonoBehaviour {


	public static uiCoinsCounter coinsCounter;

	UILabel label;


	private void Awake()
	{
		this.label = this.transform.GetComponent<UILabel>();
		coinsCounter = this;

	}


	public void setCoins(int coins)
	{
		string coinsNum = coins.ToString();
		string forPast = "";
		for (int i = 0; i < 4 - coinsNum.Length; i++) {
			forPast += "0";
		}
		forPast += coinsNum;
		label.text = forPast;
	}
	public void removeCoins(){
		label.text="0000";
	}
}