using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class exitFromLevelDoor : MonoBehaviour {
	public GameObject winPrefab;

	void OnCollisionEnter2D(Collision2D col){
		Collider2D collider = col.collider;
		if(col.transform.tag == "Player"){

			GameObject parent = UICamera.first.transform.parent.gameObject;
			GameObject obj = NGUITools.AddChild(parent, winPrefab);
		}
	}
}
