using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
public class buttonMainMenu : MonoBehaviour
{
	public void _onClick()
	{
		SceneManager.LoadScene ("ChooseLevel");
	}
}