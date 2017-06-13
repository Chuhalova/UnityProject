using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiFruitsCounter : MonoBehaviour {


	public static uiFruitsCounter fruitsCounter;

	UILabel label;


	private void Awake()
	{
		this.label = this.transform.GetComponent<UILabel>();
		fruitsCounter = this;

	}


	public void setFruits(int fruits)
	{
		string fruitsNum = fruits.ToString();
		string forPast = "0";
		forPast += fruitsNum;
		forPast+="/20";
		label.text = forPast;
	}
	public void removeFruits(){
		label.text="00/20";
	}
}