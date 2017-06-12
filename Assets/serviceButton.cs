using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class serviceButton : MonoBehaviour {
	public MyButton settingsBtn;
	public GameObject settingsPrefab;
	public static float time;
	void Start () {
		settingsBtn.signalOnClick.AddListener (this.OnSettingsBtn);

	}

	void OnSettingsBtn() {
		showSettings ();
	}

	void showSettings() {
		GameObject parent = UICamera.first.transform.parent.gameObject;
		GameObject obj = NGUITools.AddChild (parent, settingsPrefab);
		obj.GetComponent<serviceController>();

	}
}