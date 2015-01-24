using UnityEngine;
using System.Collections;

public class FreezeManager : MonoBehaviour {

		public bool isFreeze = false;
		public GameObject clock;
		private float timer = 0;
		private GameObject go;

		public GameObject counter;
		public GameObject[] cc;

		public float LimitTime = 5.0f;
		public int ClockCount = 5;
		private float LoopWidth =1.2f;

		public Player player;

		// Use this for initialization
		void Start () {
				for (int i = 0; i < ClockCount; ++i) {
						cc[i] = CreateBack (i * LoopWidth);
				}
		}

		// Update is called once per frame
		void Update () {

				if (isFreeze) {

						timer += Time.deltaTime;

						if (timer >= LimitTime) {
								DestroyObject (go);
								timer = 0;
								isFreeze = false;
						}
				}

		}

		public void StartFreeze(){

				if (isFreeze)
						return;

				if (ClockCount < 1)
						return;

				player.SendMessage ("AnimFC");

				--ClockCount;
				DestroyObject (cc [ClockCount]);
				isFreeze = true;

				go = (GameObject)Instantiate (
						clock,
						transform.position,
						new Quaternion ()
				);
				go.transform.parent = transform;
				go.name = clock.name;


		}


		GameObject CreateBack(float x){
				Vector3 vec = new Vector3(2.6f + x, -4f,0);
				GameObject go = (GameObject)Instantiate (
						counter,
						vec,
						new Quaternion ()
				);
				go.transform.parent = transform;
				return go;
		}

}
