using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class exitFromLevelDoor : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D col)
	{
		Collider2D collider = col.collider;
		if (col.transform.tag == "Player") {
			SceneManager.LoadScene ("ChooseLevel");
		}
	}
}