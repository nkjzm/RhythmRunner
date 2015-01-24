using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {

		public GameObject Back;
		public int LoopCount=10;
		private float LoopWidth =17.75f;


		// Use this for initialization
		void Start () {
				for (int i = 1; i < LoopCount; ++i) {
						CreateBack (i * LoopWidth);
				}

		}


		void CreateBack(float x){
				Vector3 vec = Vector3.zero;
				vec.x = x;
				GameObject go = (GameObject)Instantiate (
						Back,
						vec,
						new Quaternion ()
				);
				go.transform.parent = transform;
		}

		// Update is called once per frame
		void Update () {

		}
}
