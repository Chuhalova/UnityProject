using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {
	
	public int MaxHealth = 3;
	int health = 1;
	public float speed = 1;
	Rigidbody2D myBody = null;
	bool isGrounded = false;
	bool JumpActive = false;
	float JumpTime = 0f;
	public float MaxJumpTime = 2f;
	public float JumpSpeed = 2f;

	//music
	public AudioClip deathAudioClip = null;
	public AudioClip goAudioClip = null;
	public AudioClip jumpAudioClip = null;
	AudioSource deathAudioSource = null;
	AudioSource goAudioSource = null;
	AudioSource jumpAudioSource = null;

	//	public bool death = false;
	Transform heroParent = null;
	public static HeroRabbit lastRabbit = null;



	void Start () {
		myBody = this.GetComponent<Rigidbody2D>();
		LevelInfo.current.setStartPosition (transform.position);
		this.heroParent = this.transform.parent;


		//music

		this.deathAudioSource = gameObject.AddComponent<AudioSource>();
		this.deathAudioSource.clip = deathAudioClip;
		this.goAudioSource = gameObject.AddComponent<AudioSource>();
		this.goAudioSource.spatialBlend = 1;
		this.goAudioSource.clip = goAudioClip;
		this.jumpAudioSource= gameObject.AddComponent<AudioSource>();
		this.jumpAudioSource.clip = jumpAudioClip;
	}


	void FixedUpdate()
	{
		
		float value = Input.GetAxis("Horizontal"); 
		if (Mathf.Abs(value) > 0)
		{
			Vector2 vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;
		}

		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		if (value < 0)
		{
			sr.flipX = true;
		}
		else if (value > 0)
		{
			sr.flipX = false;
		}
	
		Animator animator = GetComponent<Animator>(); // run-idle
		if (Mathf.Abs(value) > 0)
		{
			if (soundManager.Instance.isSoundOn() && !goAudioSource.isPlaying && isGrounded)
			{
				goAudioSource.Play();
			}
			else if (!isGrounded)
			{
				goAudioSource.Stop();
			}
			animator.SetBool("run", true);
		}
		else
		{
			animator.SetBool("run", false);
			if (soundManager.Instance.isSoundOn())
			{
				goAudioSource.Stop();
			}
		}
	//	if (death)
	//	{
	//		animator.SetBool("death", true);
	//	}
	//	else
	//	{

	//		animator.SetBool("death", false);
	//	}

		Vector3 from = transform.position + Vector3.up * 0.3f;
		Vector3 to = transform.position + Vector3.down * 0.1f;
		int layer_id = 1 << LayerMask.NameToLayer("Ground");
		RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
		if (hit)
		{
			if(!isGrounded && (soundManager.Instance.isSoundOn())) jumpAudioSource.Play();
			isGrounded = true;
		}
		else
		{
			if (isGrounded && soundManager.Instance.isSoundOn()) 
				jumpAudioSource.Play();
			isGrounded = false;
		}

		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			this.JumpActive = true;
		}
		if (this.JumpActive)
		{
			if (Input.GetButton("Jump"))
			{
				this.JumpTime += Time.deltaTime;
				if (this.JumpTime < this.MaxJumpTime)
				{
					Vector2 vel = myBody.velocity;
					vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
					myBody.velocity = vel;
				}
			}
			else
			{
				this.JumpActive = false;
				this.JumpTime = 0;
			}
		}

		if (this.isGrounded)
		{
			//if (soundManager.Instance.isSoundOn ())jumpAudioSource.Stop ();
			animator.SetBool("jump", false);
		}
		else
		{
			//if(soundManager.Instance.isSoundOn ())jumpAudioSource.Play ();
			animator.SetBool("jump", true);
		}


		if (hit)
		{
			if (hit.transform != null
				&& hit.transform.GetComponent<MovingPlatform>() != null)
			{
				SetNewParent(this.transform, hit.transform);
			}
		}
		else
		{
			SetNewParent(this.transform, this.heroParent);
		}
	}
	void Awake(){
		lastRabbit=this;
	}


	public int rab_health(){
		return health;
	}
	static void SetNewParent(Transform obj, Transform new_parent)
	{
		if (obj.transform.parent != new_parent)
		{
			Vector3 pos = obj.transform.position;
			obj.transform.parent = new_parent;
			obj.transform.position = pos;
		}
	}
	public void addHealth(int number)
	{
		this.health += number;
		if(this.health > MaxHealth)
			{
				this.health = MaxHealth; 
			}
		onHealthChange ();
	}
		
	public void removeHealth(int number)
	{
		this.health -= number;
	if(this.health < 0)
		{
			this.health = 0;
		}
		this.onHealthChange();
	}

	void onHealthChange()
	{
		if(this.health == 1){
			this.transform.localScale = Vector3.one;
		}
		else if(this.health == 2){
		this.transform.localScale = Vector3.one * 2;
		} 
		else if(this.health == 0){
			if (soundManager.Instance.isSoundOn())
			{
				deathAudioSource.Play();
			}
		Animator animator = GetComponent<Animator>(); // run-idle	
		animator.SetBool("death", true);
		StartCoroutine (goOnStart());
		}
}
	IEnumerator goOnStart(){
		yield return new WaitForSeconds (0.8f);	
		LevelInfo.current.onRabbitDeath(this);
		Animator animator = GetComponent<Animator>(); // run-idle	
		animator.SetBool("death", false);
	}
	public void orcAttack(){
		removeHealth (1);

		//LevelInfo.current.onRabbitDeath (this);
	}
	//public void makeBigger()
	//{
		
		//this.transform.localScale += new Vector3 (0.5F, 0.5f, 0);

	//}
	public void makeBigger (){
		this.transform.localScale += new Vector3 (0.5F, 0.5f, 0);
	}


}
