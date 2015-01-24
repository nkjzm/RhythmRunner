using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

		public enum _eStatus {
				IDOL		= 1,
				LEFT_MOVE	= 2,
				RIGHT_MOVE	= 4,
				JUMP 		= 8,
				FREEZE		=16
		};
		public GameObject clear;
		public GameObject over;
		public GameObject cam;

		public float moveSpd = 1.0f;
		public float jumpSpd = 1.0f;

		private int		status=0;		//実行する入力
		private bool	isJump=false;

		public Animator animator;
		public FreezeManager freeze;

		public bool isDead = false;

		public GameObject effect;

		void Start () {
		}

		void Update(){
		}

		void Action() {

				if (rigidbody2D == null)
						return;

				if (IsStatus(_eStatus.FREEZE)) {
						freeze.SendMessage ("StartFreeze");
				}

				if (IsStatus(_eStatus.IDOL)) {
						rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
				}

				if (IsStatus(_eStatus.JUMP)) {
						Debug.Log ("jump");
						SoundManager.Instance.PlaySE (2);
						Vector2 jumpPow = Vector2.zero;
						jumpPow.y = jumpSpd;
						rigidbody2D.AddForce(jumpPow, ForceMode2D.Impulse);
				}

				if (IsStatus(_eStatus.LEFT_MOVE)) {
						rigidbody2D.velocity = (new Vector2(-moveSpd,rigidbody2D.velocity.y));
				}

				if (IsStatus(_eStatus.RIGHT_MOVE)) {
						rigidbody2D.velocity = (new Vector2(moveSpd,rigidbody2D.velocity.y));
				}

		}

		//入力をバリデートし、描画の更新をする
		void ReadyAction(int new_status) {

				status = new_status;

				if (IsStatus(_eStatus.JUMP)) {
						if (!isJump) {
								isJump = true;
								animator.SetBool ("isJump", isJump);
						} else {
								status -= (int)_eStatus.JUMP;
						}
				}

				if (IsStatus (_eStatus.LEFT_MOVE) && IsStatus (_eStatus.RIGHT_MOVE)) {
						status -= (int)_eStatus.LEFT_MOVE + (int)_eStatus.RIGHT_MOVE;
				}

				if (IsStatus (_eStatus.LEFT_MOVE) || IsStatus (_eStatus.RIGHT_MOVE)) {
						status -= (int)_eStatus.IDOL;
				}

				animator.SetBool ("isRun", false);

				SwitchDir (_eStatus.LEFT_MOVE);
				SwitchDir (_eStatus.RIGHT_MOVE);

				Action ();
		}

		void SwitchDir(_eStatus status){
				if (!IsStatus (status))
						return;
				Vector3 dir = Vector3.one;
				dir.x = status==_eStatus.LEFT_MOVE?-1:1;
				transform.localScale = dir;
				animator.SetBool ("isRun", true);
		}

		//状態が一致すればtrue
		bool IsStatus(Player._eStatus status) {
				if ((this.status & (int)status) == (int)status) {
						return true;
				}
				return false;
		}

		//側面に当たった時の処理
		void OnCollisionStay2D(Collision2D col) {
		
//				//箱の真上でないとき
//				if (Mathf.Abs(col.transform.position.x - transform.position.x) > 0.5f)
//						return;

				if (!isJump)
						return;

				if (rigidbody2D.velocity.y != 0)
						return;

				isJump = false;
				animator.SetBool ("isJump", isJump);
				transform.rigidbody2D.velocity = Vector2.zero;
		}

		void KillPlayer() {
				ResultProce (over);
		}

		void WinPlayer() {
				ResultProce (clear);
		}


		void ResultProce(GameObject go){
				if (isDead)
						return;
				Destroy (rigidbody2D);
				ShowResultWindow (go);
				isDead = true;
		}

		void ShowResultWindow(GameObject prefab){
				Vector3 vec = cam.transform.position;
				vec.z = 0;
				GameObject go = (GameObject)Instantiate (
						prefab,
						vec,
						new Quaternion ()
				);
				go.transform.parent = cam.transform;
		}

		void AnimFC(){
				animator.SetTrigger ("triggerFC");
				StartCoroutine ("ShowEffect5sec");
		}

		IEnumerator ShowEffect5sec(){
				effect.SetActive (true);
				effect.transform.localPosition = new Vector3(0,2,0);
				effect.transform.parent = null;
				yield return new WaitForSeconds(2.0f);
				effect.transform.parent = transform;
				effect.SetActive (false);
		}

}
