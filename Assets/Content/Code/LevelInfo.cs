using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour {


	public static LevelInfo current;

	void Awake()
	{
		current = this;
	}

	Vector3 startingPosition;
	public void setStartPosition(Vector3 pos)
	{
		this.startingPosition = pos;
	}
	public void onRabbitDeath(Rabbit rabbit)
	{
		rabbit.transform.position = this.startingPosition;
	}
}
