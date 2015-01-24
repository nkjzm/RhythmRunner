using UnityEngine;
using System.Collections;

public class ShurikenManager : MonoBehaviour {

		public GameObject shuriken;
		public FreezeManager freeze;

		private float timer = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

				if (freeze.isFreeze)
						return;
	
				timer += Time.deltaTime;

				if (timer >= 1.5f) {
						timer = 0;
						GameObject go = (GameObject)Instantiate (
								                shuriken,
								                transform.position,
								                new Quaternion ()
						                );
						go.transform.parent = transform;
						go.name = shuriken.name;
				}

	}
}
