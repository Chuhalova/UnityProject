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
	/*public GameObject winPrefab;
	public static float time;
	// Use this for initialization
	void Start () {
		time = Time.timeScale;
	}
	void OnTriggerEnter2D(Collider2D collider)
	{
		HeroRabbit rabit = collider.GetComponent<HeroRabbit>();
		if (rabit != null)
		{
			soundManager.setSoundOn(false);
			showWinPanel ();

		}


	}

	public void showWinPanel() {
		//Знайти батьківський елемент
		GameObject parent = UICamera.first.transform.parent.gameObject;
		//	GameObject parent = UICamera.first.transform.SetParent(gameObject);
		//Створити Prefab
		GameObject obj = NGUITools.AddChild (parent, winPrefab);
		//Отримати доступ до компоненту (щоб передати параметри)
		obj.GetComponent<WinPanel>();
		//Time.timeScale = 0;
		//...
	}*/
}