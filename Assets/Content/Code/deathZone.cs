using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathZone : MonoBehaviour{



	void OnTriggerEnter2D(Collider2D collider)
	{
			Rabbit rabbit = collider.GetComponent<Rabbit>();

			if (rabbit != null)
		{
			
			LevelInfo.current.onRabbitDeath(rabbit);
		}
	}
}