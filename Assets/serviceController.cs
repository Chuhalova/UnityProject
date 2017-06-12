using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class serviceController : MonoBehaviour {

	public MyButton background;
	public MyButton close;
	public MyButton sound;
	public MyButton volume;
	// Use this for initialization
	void Start ()
	{
		/*setImages();*/
		background.signalOnClick.AddListener(this.onClosePlay);
		close.signalOnClick.AddListener(this.onClosePlay);
		sound.signalOnClick.AddListener(this.onSoundPlay);
		volume.signalOnClick.AddListener(this.onVolumePlay);
	}

	void Update()
	{
		/*setImages();*/
	}

	/*private void setImages()
	{
		if (soundManager.Instance.isSoundOn())
		{


			sound.gameObject.GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>("sound-on");
		}
		else
		{
			sound.gameObject.GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>("sound-off");
		}
		if (soundManager.Instance.isVolumeOn())
		{
			volume.gameObject.GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>("music-on");
		}
		else
		{
			volume.gameObject.GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>("music-off");
		}
	}
	*/
	void onClosePlay()
	{
		Time.timeScale = 1;
		Destroy(this.gameObject);
	}

	void onSoundPlay()
	{
		if (soundManager.Instance.isSoundOn()) {
			soundManager.Instance.setSoundOn(false);

		}
		else
		{
			soundManager.Instance.setSoundOn(true);

		}

	}

	void onVolumePlay()
	{
		if (soundManager.Instance.isVolumeOn())
		{
			soundManager.Instance.setVolumeOn(false);

		}
		else
		{
			soundManager.Instance.setVolumeOn(true);
		}
	}
}
