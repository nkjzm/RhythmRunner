using UnityEngine;
using System.Collections;

public class TimerClass : MonoBehaviour {

		private	FreezeManager  freeze;

		public GameObject hari;
		float Ang;

		void Awake(){
				InitParam ();
				freeze = GameObject.Find ("FreezeManager").GetComponent<FreezeManager>();
		}

		void InitParam(){
				Ang = 0;
		}

		// Use this for initialization
		void Start () {
		}

		// Update is called once per frame
		void Update () {

				Ang -= (360/freeze.LimitTime) * Time.deltaTime;
				hari.transform.localRotation = Quaternion.Euler (0,0,Ang);
		}
}
