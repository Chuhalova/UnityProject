﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystalPanel : MonoBehaviour {

	public Sprite first;
	public Sprite second;
	public Sprite third;

	//for win panel
	public bool firstWin=false;
	public bool secondWin=false;
	public bool thirdWin=false;

	public static crystalPanel crystals;

	UI2DSprite[] gemComponents;


	private void Awake()
	{
		crystals = this;
		gemComponents = new UI2DSprite[3];
		loadComponents();
	}

	private void loadComponents()
	{
		for (int i = 0; i < transform.childCount; ++i)
			gemComponents[i] = transform.GetChild(i).GetComponent<UI2DSprite>();
	}

	public void Crystals(string crystals) {
		if (crystals == "gem-1") {
			gemComponents [0].sprite2D = first;
			firstWin = true;
		} else if (crystals == "gem-2") {
			gemComponents [1].sprite2D = second;
			secondWin = true;
		} else if (crystals == "gem-3") {
			gemComponents [2].sprite2D = third;
			thirdWin = true;
		}
	}

}