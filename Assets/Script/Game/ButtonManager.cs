using UnityEngine;
using System.Collections;

public class  ButtonManager : MonoBehaviour {

		private SceneManager scene;

		public Sprite[] tex_left;
		public Sprite[] tex_right;
		public Sprite[] tex_jump;
		public Sprite[] tex_freeze;
		public SpriteRenderer sr_left;
		public SpriteRenderer sr_right;
		public SpriteRenderer sr_jump;
		public SpriteRenderer sr_freeze;

		public GameObject player;
		private Player c_player;

		private float xVelocity = 0.0F;	//送れて着いてくるカメラ用変数

		private Player._eStatus p_status = Player._eStatus.IDOL;

		// Use this for initialization
		void Start () {
				if (GameObject.Find ("SceneManager")) {
						scene = GameObject.Find ("SceneManager").GetComponent<SceneManager> ();
				}
				SoundManager.Instance.PlayBGM (1);
				c_player = player.GetComponent<Player> ();
		}

		// Update is called once per frame
		void FixedUpdate () {

				if (FadeManager.isFading)
						return;

				GameInput ();

				LateUpdate ();

				if(Input.GetKeyDown(KeyCode.T))
						scene.SendMessage ("ChangeState", SceneManager.STATE.GAME);


		}


		void LateUpdate(){

				//遅れてカメラ処理
				float second = 0.5f;
				float newPosition = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref xVelocity, second);
				transform.position = new Vector3(newPosition,transform.position.y, transform.position.z);
		}

		void GameInput () {

				p_status = Player._eStatus.IDOL;	//リセット

				//タッチ入力
				int count = Input.touches.Length;
				if (count > 0) {
						for (int i = 0; i < count; ++i) {

								TouchPhase phase = Input.touches [i].phase;
								Vector3 pos	= Input.touches [i].position;

								if (phase == TouchPhase.Began) {
										BeginTouchPosition (pos);
								}
								if (phase == TouchPhase.Ended) {
										EndTouchPosition (pos);
								}
								IsTouchPosition (pos);
						}
				}

				//マウス入力
				{
						Vector3 pos = Input.mousePosition;
						if (Input.GetMouseButtonDown (0)) {			
								BeginTouchPosition (pos);
						}
						if (Input.GetMouseButtonUp (0)) {			
								EndTouchPosition (pos);
						}
						if (Input.GetMouseButton (0)) {			
								IsTouchPosition (pos);
						}
				}

				//キー入力
				{
						if(Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A)) {
								AddStatus (Player._eStatus.LEFT_MOVE);
						}
						if(Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.S)) {
								AddStatus (Player._eStatus.RIGHT_MOVE);
						}
						if(Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.Space)) {
								AddStatus (Player._eStatus.JUMP);
						}
						if(Input.GetKey(KeyCode.DownArrow)||Input.GetKey(KeyCode.Return)) {
								AddStatus (Player._eStatus.FREEZE);
						}
				}


				if(c_player.isDead)
						p_status = Player._eStatus.IDOL;

				UpdateButton ();
				player.SendMessage ("ReadyAction",(int)p_status);

		}


		void UpdateButton(){

				bool isLeft = IsStatus (Player._eStatus.LEFT_MOVE);
				bool isRight = IsStatus (Player._eStatus.RIGHT_MOVE);
				bool isJump = IsStatus (Player._eStatus.JUMP);
				bool isFreeze = IsStatus (Player._eStatus.FREEZE);


				sr_left.sprite = tex_left [isLeft?1:0];
				sr_right.sprite = tex_right [isRight?1:0];
				sr_jump.sprite = tex_jump [isJump?1:0];
				sr_freeze.sprite = tex_freeze [isFreeze?1:0];

		}


		void BeginTouchPosition(Vector3 vec){

				Vector3 ray = Camera.main.ScreenToWorldPoint(vec);
				Collider2D hit = Physics2D.OverlapPoint(ray);

				if (hit){
				

						if(hit.transform.name=="Return")
						{
								SoundManager.Instance.PlaySE(1);
								scene.SendMessage ("ChangeState", SceneManager.STATE.TITLE);
								return;
						}
				}

		}

		void EndTouchPosition(Vector3 vec){

				Vector3 ray = Camera.main.ScreenToWorldPoint(vec);
				Collider2D hit = Physics2D.OverlapPoint(ray);

				if (hit){
				
				}
		}


		void IsTouchPosition(Vector3 vec){

				Vector3 ray = Camera.main.ScreenToWorldPoint(vec);
				Collider2D hit = Physics2D.OverlapPoint(ray);

				if (hit){

						if(hit.transform.name=="left")
						{
								AddStatus (Player._eStatus.LEFT_MOVE);
						}
						if(hit.transform.name=="right")
						{
								AddStatus (Player._eStatus.RIGHT_MOVE);
						}

						if(hit.transform.name=="jump")
						{
								AddStatus (Player._eStatus.JUMP);
						}

						if(hit.transform.name=="stop")
						{
								AddStatus (Player._eStatus.FREEZE);
						}

						if(hit.transform.name=="Next")
						{
								SoundManager.Instance.PlaySE(1);
								scene.stage = (SceneManager.STAGE)((int)scene.stage+1);
								scene.SendMessage ("ChangeState", SceneManager.STATE.GAME);
								return;
						}

						if(hit.transform.name=="Retry")
						{
								SoundManager.Instance.PlaySE(1);
								scene.SendMessage ("ChangeState", SceneManager.STATE.GAME);
								return;
						}

						if(hit.transform.name=="Return")
						{
								SoundManager.Instance.PlaySE(1);
								scene.SendMessage ("ChangeState", SceneManager.STATE.TITLE);
								return;
						}
				}

		}

		void AddStatus(Player._eStatus status){
				p_status = p_status | status;
		}

		//状態が一致すればtrue
		bool IsStatus(Player._eStatus status) {
				if (((int)p_status & (int)status) == (int)status) {
						return true;
				}
				return false;
		}

	
}
