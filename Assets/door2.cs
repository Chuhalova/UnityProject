using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
public class door2 : MonoBehaviour
{


	void OnCollisionEnter2D(Collision2D col){
		Collider2D collider = col.collider;
		if(col.transform.tag == "Player"){
			SceneManager.LoadScene ("Level2");
		}
	}
}