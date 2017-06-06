using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
public class buttonMainMenu : MonoBehaviour
{
//	public buttonMainMenu playButton;
//	public UnityEvent signalOnClick = new UnityEvent();
//	void Start()
//	{
//		playButton.signalOnClick.AddListener(this._onClick);
//	}
	public void _onClick()
	{
		SceneManager.LoadScene ("Level");
	}
}