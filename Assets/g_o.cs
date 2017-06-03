using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g_o : MonoBehaviour {
	public enum Mode {
		walkToA,
		walkToB,
		attack,
		startRunToRabbit,
		attacked
	}
	//start mode
	Mode mode = Mode.walkToA;

	//points (start and finish)
	public Vector3 pointA;
	public Vector3 pointB;
	//start and finish 
	float startPoint, finishPoint;
	//speed of orc 
	public float speed = 3;
	//----------------
	Rigidbody2D myBody = null;
	//initislization
	void Start () {
		myBody = this.GetComponent<Rigidbody2D>();
		//start point -> point with smaller coordinates
		startPoint = Mathf.Min(pointA.x, pointB.x);
		//finish point -> point with bigger coordinates 
		finishPoint = Mathf.Max(pointA.x, pointB.x);
	}
	//--------------------------
	void Update () {
		//initialize our animator 
		Animator animator = GetComponent<Animator>();
		//modes and animations , which is followed by this modes 
		if(mode==Mode.walkToA || mode==Mode.walkToB){
			animator.SetBool("walk",true);
		}
		if(mode==Mode.startRunToRabbit)animator.SetBool("run",true);
		else animator.SetBool("run",false);
		if(mode==Mode.attack){
			animator.SetTrigger("attack");
		}
		if(mode==Mode.attacked)animator.SetTrigger("attacked");
	}

	void FixedUpdate(){
		//transportation of orc
		float value = this.direction();
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		//changing of direction
		if (value<0) sr.flipX = false;
		else if (value>0) sr.flipX = true;
		//---------------
		if (Mathf.Abs (value) > 0) {
			Vector2 vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;
		}
		//we call the method that is responsible for the death of the rabbit and its return to the starting point
		if(mode==Mode.attack)StartCoroutine(attackLater());

	}
	float direction(){
		//green orc position
		Vector3 g_o_pos = this.transform.position;
		//rabbit position
		Vector3 rabbit_pos = HeroRabbit.lastRabbit.transform.position;
		//special conditions for orc running
		if(mode!=Mode.attacked && rabbit_pos.x<finishPoint &&rabbit_pos.x>startPoint&& mode!=Mode.attack && Mathf.Abs(rabbit_pos.y-g_o_pos.y)<GetComponent<BoxCollider2D>().size.y) mode=Mode.startRunToRabbit;
		//conditions for walking after running
		else if(mode==Mode.startRunToRabbit) mode = Mode.walkToA;
		if(mode==Mode.walkToA){
			if(g_o_pos.x > startPoint)return -1;
			else {
				mode = Mode.walkToB;
				return 1;
			}
		}
		if(mode==Mode.walkToB){	
			if(g_o_pos.x < finishPoint)return 1;
			else {
				mode = Mode.walkToA;
				return -1;
			}
		}
		if(mode==Mode.startRunToRabbit){
			//Move towards rabbit
			if(g_o_pos.x < rabbit_pos.x)return 2;
			else return -2;
		}
		return 0;
	}
	//learn the actions of the rabbit and orc by the rabbit`s location
	void OnCollisionEnter2D(Collision2D col){
		Collider2D collider = col.collider;
		if(col.transform.tag == "Player")
		{
			Vector3 location = col.contacts[0].point;
			float up = this.GetComponent<BoxCollider2D>().bounds.max.y;
			if (Mathf.Abs (location.y - up) < 0.01f) {
				mode = Mode.attacked;
				StartCoroutine(dieLater());
			} else {
				mode = Mode.attack;
			}
		}
	}
	//destroy orc 
	public void destoyGreenOrc(){
		Destroy(this.gameObject);
	}
	void OnCollisionExit2D(Collision2D col){
		if(col.transform.tag == "Player")
		{
			mode=Mode.walkToA;
		}
	}
	//pause after dying and before destroying
	IEnumerator dieLater(){
		yield return new WaitForSeconds (0.8f);	
		destoyGreenOrc ();
	}
	//pause after orc`s attack and before rabbit dying
	IEnumerator attackLater(){
		yield return new WaitForSeconds (0.5f);	
		HeroRabbit.lastRabbit.orcAttack();
	}
}