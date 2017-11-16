using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class behaviour : MonoBehaviour {
	public float speedH;
	private bool facingRight;
	public Rigidbody2D rigid;
	public float jumpHeight;
	private List<GameObject> Projectiles = new List<GameObject> ();
	private List<bool> projectileFacing = new List<bool> ();
	public float projectileVelo;
	private bool extrajump = false;
	private int runRight = 0;
	private bool rr = false;
	private bool rl = false;
	private int runningR = 0;
	private int runningL = 0;
	private int runLeft = 0;
	public GameObject projectilePrefab;
	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
		projectileVelo = 8.0f;
		facingRight = true;
		//Time.timeScale = 3f;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.RightArrow)){
			facingRight = true;
			if (rr) {
				switch(runningR){
					case 0:
						runRight = 0;
						break;
					case 1:
						runRight = 0;
						break;
					case 2:
						runRight = 0;
						break;
					case 3:
						runRight = 1;
						break;
					case 4:
						runRight = 1;
						break;
					case 5:
						runRight = 1;
						break;
					case 6:
						runRight = 2;
						break;
					case 7:
						runRight = 2;
						break;
					case 8:
						runRight = 2;
						break;
					case 9:
						runRight = 3;
						break;
					default:
						runRight = 3;
						break;
				}
				if(runRight>1){
					this.transform.position = new Vector2 (this.transform.position.x+speedH*runRight,this.transform.position.y);
				}else{
					this.transform.position = new Vector2 (this.transform.position.x+speedH,this.transform.position.y);
				}
			} else {
				rr = true;
				runningR++;
				this.transform.position = new Vector2 (this.transform.position.x+speedH,this.transform.position.y);
			}
		}
		else if(Input.GetKey(KeyCode.LeftArrow)){
			facingRight = false;
			if (rl) {
				switch(runningL){
				case 0:
					runLeft = 0;
					break;
				case 1:
					runLeft = 0;
					break;
				case 2:
					runLeft = 0;
					break;
				case 3:
					runLeft = 1;
					break;
				case 4:
					runLeft = 1;
					break;
				case 5:
					runLeft = 1;
					break;
				case 6:
					runLeft = 2;
					break;
				case 7:
					runLeft = 2;
					break;
				case 8:
					runLeft = 2;
					break;
				case 9:
					runLeft = 3;
					break;
				default:
					runLeft = 3;
					break;
				}
				if(runLeft>1){
					this.transform.position = new Vector2 (this.transform.position.x-speedH*runLeft,this.transform.position.y);
				}
				else{
					this.transform.position = new Vector2 (this.transform.position.x-speedH,this.transform.position.y);
				}
			} else {
				rl = true;
				runningL++;
				this.transform.position = new Vector2 (this.transform.position.x-speedH,this.transform.position.y);
			}

		}
		if(Input.GetKeyUp(KeyCode.RightArrow)){
			runRight = 0;
			rr = false;
			runningR = 0;
		}
		if(Input.GetKeyUp(KeyCode.LeftArrow)){
			runLeft = 0;
			rr = false;
			runningL = 0;
		}
		if(Input.GetKeyDown(KeyCode.Z)){
			if (extrajump == false) {
				rigid.AddForce (new Vector2 (0,jumpHeight),ForceMode2D.Impulse);
				extrajump = true;
			} /**else {
				extrajump = true;
			}*/
		}
		if(Input.GetKeyUp(KeyCode.Z)){
			float l = -0.5f * jumpHeight;
			rigid.AddForce (new Vector2 (0, l), ForceMode2D.Impulse);
		}
		if(Input.GetKeyDown(KeyCode.X)){
			GameObject bullet = (GameObject)Instantiate (projectilePrefab, transform.position, Quaternion.identity);
			Projectiles.Add (bullet);
			projectileFacing.Add (facingRight);
		}

		for(int i=0;i<Projectiles.Count;i++){
			GameObject goBullet = Projectiles [i];
			if(goBullet != null){
				if(projectileFacing[i]){
					goBullet.transform.Translate (new Vector2(1,0)*Time.deltaTime * projectileVelo);
				}else{
					goBullet.transform.Translate (new Vector2(-1,0)*Time.deltaTime * projectileVelo);
				}
				Vector2 bulletScreenPos = Camera.main.WorldToScreenPoint (goBullet.transform.position);
				if(bulletScreenPos.x >= Screen.width || bulletScreenPos.x <= 0){
					DestroyObject (goBullet);
					Projectiles.Remove (goBullet);
					projectileFacing.RemoveAt (i);
				}
			}
		}
	}
		
	void OnCollisionEnter2D(Collision2D collide){
		extrajump = false;
	}
}
