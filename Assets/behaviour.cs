using UnityEngine;
using System.Collections;

public class behaviour : MonoBehaviour {
	public float speedH;
	public Rigidbody2D rigid;
	public float jumpHeight;
	private bool extrajump = false;
	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
		//Time.timeScale = 3f;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.RightArrow)){
			this.transform.position = new Vector2 (this.transform.position.x+speedH,this.transform.position.y);
		}
		else if(Input.GetKey(KeyCode.LeftArrow)){
			this.transform.position = new Vector2 (this.transform.position.x-speedH,this.transform.position.y);
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			if (extrajump == false) {
				rigid.AddForce (new Vector2 (0,jumpHeight),ForceMode2D.Impulse);
				extrajump = true;
			} /**else {
				extrajump = true;
			}*/
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			float l = -0.5f * jumpHeight;
			rigid.AddForce (new Vector2 (0, l), ForceMode2D.Impulse);
		}
	}
		
	void OnCollisionEnter2D(Collision2D collide){
		extrajump = false;
	}
}
