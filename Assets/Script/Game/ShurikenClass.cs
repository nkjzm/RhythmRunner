using UnityEngine;
using System.Collections;

public class ShurikenClass : MonoBehaviour {


		private float delTimer = 999;
		private Vector3 vec = new Vector3 (0, 0, 1000);
		private Vector3 vec2 = new Vector3 (0, -10, 0);
		private bool isDel=false;

		private ShurikenManager sm; 

	// Use this for initialization
	void Start () {
				sm = transform.parent.GetComponent<ShurikenManager> ();
	
	}
	
	// Update is called once per frame
	void Update () {

				if (sm.freeze.isFreeze)
						return;

				if (isDel) {

						delTimer -= Time.deltaTime;

						if (delTimer < 0) {
								DestroyObject (gameObject);
						}

						return;

				}

				transform.position += vec2*Time.deltaTime;
				transform.Rotate(vec*Time.deltaTime);
	
	}

		void OnTriggerEnter2D(Collider2D col){

				if (col.tag == "Trap")
						return;

				isDel = true;

				Rigidbody2D rb =  gameObject.GetComponent<Rigidbody2D> ();
				rb.gravityScale = 0;
				rb.velocity = Vector2.zero;

				delTimer = 0.5f;

				if (col.tag == "Player") {
						col.gameObject.SendMessage ("KillPlayer");
				}
		}
}
