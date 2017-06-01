﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathZone : MonoBehaviour{
		
	void OnTriggerEnter2D(Collider2D collider)
	{
		HeroRabbit rabbit = collider.GetComponent<HeroRabbit>();

		if (rabbit != null)
		{
			LevelInfo.current.onRabbitDeath(rabbit);
		}
	}
}

