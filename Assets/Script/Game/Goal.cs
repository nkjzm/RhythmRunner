using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {


		public GameObject player;


		// Collision
		void OnCollisionEnter2D(Collision2D col) {
				player.SendMessage ("WinPlayer");
		}
}
