using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class o_o : MonoBehaviour {
	//music
	public AudioClip attackAudioClip = null;
	AudioSource attackAudioSource = null;

	public enum Mode {
		walkToA,
		walkToB,
		attack,
		dead,
		attacked,
		//idle
	}
	//starting from point .. 
	Mode mode = Mode.walkToA;

	//initialization of carrot
	public GameObject prefabCarrot;

	//points where orc patrouling 
	public Vector3 pointA;
	public Vector3 pointB;

	public float speed = 1;
	//points where orc patrouling 
	float startPoint, finishPoint;
	float last_carrot = 0;

	Rigidbody2D myBody = null;

	//initialization
	void Start () {
		myBody = this.GetComponent<Rigidbody2D>();
		//learn min point
		startPoint = Mathf.Min(pointA.x, pointB.x);
		//learn max point 
		finishPoint = Mathf.Max(pointA.x, pointB.x);

		//music
		this.attackAudioSource = gameObject.AddComponent<AudioSource>();
		this.attackAudioSource.clip = attackAudioClip;
	}
		
	void Update () {
		//initialization 
		Animator animator = GetComponent<Animator>();
		if(mode==Mode.walkToA || mode==Mode.walkToB){
			animator.SetBool("walk",true);}
		if(mode==Mode.attack){
			//animator.SetBool("idle",true);
			animator.SetBool("walk", false);
		}
		if(mode==Mode.attacked)animator.SetTrigger("attacked");
	}

	void FixedUpdate(){
		float value = this.direction();
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		//change sides
		if (value<0) sr.flipX = false;
		else if (value>0) sr.flipX = true;
		if (Mathf.Abs (value) > 0) {
			Vector2 vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;
		}
		if (mode == Mode.attack && (Time.time - last_carrot > 2.0f)) {
			if(soundManager.Instance.isSoundOn())
				attackAudioSource.Play();
			Animator animator = GetComponent<Animator> ();
			animator.SetBool ("attack", true);
				if(sr.flipX)launchCarrot(1.0f);
				else launchCarrot(-1.0f);
				last_carrot=Time.time;
		} else {
			Animator animator = GetComponent<Animator> ();
			animator.SetBool ("attack", false);
		}

	}

	float direction(){
		Vector3 my_pos = this.transform.position;
		Vector3 rabbit_pos = HeroRabbit.lastRabbit.transform.position;
		//learn situations in which orc start attack 
		if(rabbit_pos.x>startPoint&& rabbit_pos.x<finishPoint && mode!=Mode.attack && Mathf.Abs(rabbit_pos.y-my_pos.y)<GetComponent<BoxCollider2D>().size.y) mode=Mode.attack;
		else if((rabbit_pos.x<startPoint|| rabbit_pos.x>finishPoint) && mode==Mode.attack) mode = Mode.walkToA;
		if(mode==Mode.walkToA){
			if(my_pos.x > startPoint)return -1;
			else {
				mode = Mode.walkToB;
				return 1;
			}
		}
		if(mode==Mode.walkToB){	
			if(my_pos.x < finishPoint)return 1;
			else {
				mode = Mode.walkToA;
				return -1;
			}
		}

		if(mode==Mode.attack){
			SpriteRenderer sr = GetComponent<SpriteRenderer>();

			if (rabbit_pos.x<my_pos.x) sr.flipX = false;
			else if (rabbit_pos.x>my_pos.x) sr.flipX = true;
		}
		return 0;
	}
	//learn situations in which rabbit die or orc die 
	void OnCollisionEnter2D(Collision2D col){

		Collider2D collider = col.collider;

		if(col.transform.tag == "Player")
		{
			Vector3 contactPoint = col.contacts[0].point;
			float up = this.GetComponent<BoxCollider2D>().bounds.max.y;
			if (Mathf.Abs (contactPoint.y - up) < 0.02f) {
				mode = Mode.attacked;
				StartCoroutine(dieLater ());
			}
			else mode=Mode.attack;
		}
	}
	public void DestroyOrc(){
		Destroy(this.gameObject);
	}
	void OnCollisionExit2D(Collision2D col){
		if(col.transform.tag == "Player")
		{
			mode=Mode.walkToA;
		}
	}
	//pause before dying and destroying 
	IEnumerator dieLater(){
		yield return new WaitForSeconds (0.8f);	
		DestroyOrc ();
	}

	void launchCarrot(float direction){
		GameObject obj = GameObject.Instantiate(this.prefabCarrot);

	    Vector3 my_pos = this.transform.position;
	    my_pos.y=this.GetComponent<BoxCollider2D>().bounds.center.y;
	
		carrot carrot = obj.GetComponent<carrot>();
		carrot.transform.position = my_pos;
		carrot.launch(direction);
	}

}