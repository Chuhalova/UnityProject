using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {
	public int MaxHealth = 2;
	int health = 1;
	public float speed = 1;
	Rigidbody2D myBody = null;
	bool isGrounded = false;
	bool JumpActive = false;
	float JumpTime = 0f;
	public float MaxJumpTime = 2f;
	public float JumpSpeed = 2f;
//	public bool death = false;
	Transform heroParent = null;
	public static HeroRabbit lastRabbit = null;
	void Start () {
		myBody = this.GetComponent<Rigidbody2D>();
		LevelInfo.current.setStartPosition (transform.position);
		this.heroParent = this.transform.parent;
	
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
			animator.SetBool("run", true);
		}
		else
		{
			animator.SetBool("run", false);
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
			isGrounded = true;
		}
		else
		{
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
			animator.SetBool("jump", false);
		}
		else
		{
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
		if(this.health == 1)
		{
			this.transform.localScale = Vector3.one;
		} else if(this.health == 2)
		{
			this.transform.localScale = Vector3.one * 2;
		} else if(this.health == 0)
		{

			LevelInfo.current.onRabbitDeath(this);
		}
	}
	public void orcAttack(){
		LevelInfo.current.onRabbitDeath (this);
	}
	//public void makeBigger()
	//{
		
		//this.transform.localScale += new Vector3 (0.5F, 0.5f, 0);

	//}
	public void makeBigger (){
		this.transform.localScale += new Vector3 (0.5F, 0.5f, 0);
	}


}
