using UnityEngine;
using System.Collections;

public enum PLAYER_STATE{
		IDOL,
		LEFT_MOVE,
		RIGHT_MOVE,
		JUMP
}

public class NinjaClass : MonoBehaviour {

		PLAYER_STATE pState=PLAYER_STATE.IDOL;

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

				if (pState == PLAYER_STATE.LEFT_MOVE) {
						transform.Translate (Vector3.left/10);
				}

				if (pState == PLAYER_STATE.RIGHT_MOVE) {
						transform.Translate (Vector3.right/10);
				}

		}

		void ChangeStateLeft(){
				pState = PLAYER_STATE.LEFT_MOVE;
		}

		void ChangeStateRight(){
				pState = PLAYER_STATE.RIGHT_MOVE;
		}

		void ChangeStateIdol(){
				pState = PLAYER_STATE.IDOL;
		}



}
