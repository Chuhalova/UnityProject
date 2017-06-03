using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrc : MonoBehaviour {
	//-------------------
	public Vector3 pointA;
	public Vector3 pointB;
	//-------------------
	public float moveBy;
	public float speed = 1;
	//-------------------
	public Rigidbody2D orcBody = null;
	//--------------------
	public Animator orcAnimator = null;
	//--------------------
	public SpriteRenderer sr = null;
	//--------------------
	protected Mode mode, oldMode;
	public Collider2D head,body;
	protected Renderer r=null;
	//--------------------
	//control of movements
	protected enum Mode {
		//walkink to start
		walkToPointA,
		//walking to finish
		walkToPointB,
		//atacking some rabbit 
		attack,
		//standing
		idle,
		//dying
		die,
		//rotation
		flip
	}
	//----------------------
	public void Start () {
		r = GetComponent<Renderer> ();
		sr = GetComponent<SpriteRenderer> ();
		//get orc
		orcBody = this.GetComponent<Rigidbody2D> ();
		//get orc`s position
		pointA = this.transform.position;
		pointB = pointA;
		//get finish position
		pointB.x += moveBy;
		mode = Mode.walkToPointB;
		//initialize orc`s animator
		orcAnimator = GetComponent<Animator> ();
	}

	//---------------
	public void FixedUpdate () {

		//Vector3 rabbitPos = HeroRabbit.lastRabbit.transform.position;
		if (mode == Mode.die) {
			StartCoroutine (dieAnimation ());
		} else if (mode == Mode.attack) {
			orcAnimator.SetBool ("run", false);
			orcAnimator.SetBool ("walk", false);
			orcAnimator.SetTrigger ("attack");
			speed = 0;
		}
		else {
			float value = getDirection ();
			walk (value);
			walkAnimation (value);
			if (HeroRabbit.lastRabbit.rab_health()!=0) 
			OnRabbitNoticed ();
			flipOrc(value);
		}

	}
	//orc`s transpotration
	public float getDirection() {
		Mode backwardMode;
		//changing of patrol mode
		//if orc is on his second position -> go to first
		if(mode == Mode.walkToPointB) {
			if (transform.position.x < pointB.x) {
				backwardMode = mode;
				mode = Mode.idle;
				StartCoroutine (waitOnPoint (2.0f, backwardMode));
			}
			return -1; 
			//changing of patrol mode 
			//if orc is on his first position -> go to second
		} else if(mode == Mode.walkToPointA) {
			if (transform.position.x >= pointA.x) {
				backwardMode = mode;
				mode = Mode.idle;
				StartCoroutine (waitOnPoint (2.0f, backwardMode));
			}
			return 1; 
		}
		return 0; 
	}
	//we must to know where is our rabbit 
	void whereIsRabbit() {
		//if rabbit is on orc`s
		if (HeroRabbit.lastRabbit.transform.position.x < transform.position.x)
			mode = Mode.walkToPointA;
		else
			mode = Mode.walkToPointB;
		oldMode = mode;
	}
	public void OnRabbitNoticed() {
		float rabbitPos = HeroRabbit.lastRabbit.transform.position.x;
		if ( rabbitPos < pointA.x && rabbitPos > pointB.x &&
			Mathf.Abs (HeroRabbit.lastRabbit.transform.position.y - transform.position.y) < 2f) {
			speed =2.3f;
			fromWalkToRunAnimation ();
			whereIsRabbit ();

		} else {
			fromRunToWalkAnimation ();
			speed = 1f;
		}
	}
	public void walk(float value)
	{
		if (Mathf.Abs (value) > 0) {
			Vector2 vel = orcBody.velocity;
			vel.x = value * speed;
			orcBody.velocity = vel;

		}
	}
	public void flipOrc(float value) 
	{
		if (value < 0) {
			sr.flipX = false;
		} else if (value > 0) {
			sr.flipX = true;
		}

	}


	protected void walkAnimation(float value) {
		if (Mathf.Abs (value) > 0) {
			orcAnimator.SetBool ("walk", true);
		} else {
			orcAnimator.SetBool ("walk", false);
		}
	}


	protected IEnumerator waitOnPoint (float duration, Mode oldMode) {
		orcAnimator.SetBool ("walk", false);
		yield return new WaitForSeconds (duration);
		toggle(oldMode);
		orcAnimator.SetBool ("walk", true);
	}

	protected void toggle(Mode oldMode) {
		if (oldMode == Mode.walkToPointA) {
			mode = Mode.walkToPointB;
		}
		else {
			mode = Mode.walkToPointA;
		}
	}

	public void die() {
		mode = Mode.die;
	}


	public void attack() {
		mode = Mode.attack;
	}
	public  virtual void setUsualBehavior() {
		orcAnimator.SetBool ("walk", true);
		mode = oldMode; 
	}

	public bool isDead() {
		return mode == Mode.die;
	}

	public void becomeTransparent() {
		Color c = new Color (1.0f, 1.0f, 1.0f, 0f);
		r.material.SetColor ("_Color", c);
	}



	void fromWalkToRunAnimation() {
	orcAnimator.SetBool ("run", true);

	} 
	void fromRunToWalkAnimation() {
		if (HeroRabbit.lastRabbit.rab_health () != 0 && !isDead ())
			orcAnimator.SetBool ("run", false); 
	} 

	IEnumerator dieEffectLater() {
		mode = Mode.idle;
		yield return new WaitForSeconds (1.2f);
	}

	public  void attackMethod() {
		orcAnimator.SetTrigger ("attack");
	}
	public IEnumerator dieAnimation() {
		orcAnimator.SetBool ("die", true);
		yield return new WaitForSeconds (1f);
		Destroy (this.gameObject);
		//	becomeTransparent();
	}



}