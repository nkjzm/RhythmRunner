using System.IO;
using UnityEngine;
using System.Collections;

public class TitleButtonManager : MonoBehaviour {

		SceneManager sm;

		public Sprite[] start_tex;
		public Sprite[] credit_tex;
		public Sprite[] howto_tex;

		public SpriteRenderer start_sr;
		public SpriteRenderer credit_sr;
		public SpriteRenderer howto_sr;

		enum PUSH{NONE,START,CREDIT,HOWTO,OK}
		PUSH psh=PUSH.NONE;


		// Use this for initialization
		void Start () {
				sm = GameObject.Find ("SceneManager").GetComponent<SceneManager> ();
				if (sm.state == SceneManager.STATE.TITLE)
						SoundManager.Instance.PlayBGM(0);
				else
						SoundManager.Instance.PlayBGM(2);
		}


		// Update is called once per frame
		void Update () {

				if (FadeManager.isFading)
						return;

				if(Input.touches.Length > 0 && Input.touches[0].phase==TouchPhase.Began){
						BeginTouchPosition(Input.touches[0].position);
						return;
				}
				if(Input.touches.Length > 0 && Input.touches[0].phase==TouchPhase.Ended){
						EndTouchPosition(Input.touches[0].position);
						return;
				}

				if (Input.GetMouseButtonDown(0))
				{			
						BeginTouchPosition(Input.mousePosition);
				}
				if (Input.GetMouseButtonUp(0))
				{			
						EndTouchPosition(Input.mousePosition);
				}

		}


		//ボタン判定
		void BeginTouchPosition(Vector3 vec){

				Vector3 ray = Camera.main.ScreenToWorldPoint(vec);

				Collider2D hit = Physics2D.OverlapPoint(ray);

				if (!hit)
						return;

				if (hit.name== "Start") {
						psh = PUSH.START;
						start_sr.sprite = start_tex [1];
						return;
				}

				if (hit.name== "Credit") {
						psh = PUSH.CREDIT;
						credit_sr.sprite = credit_tex [1];
						return;
				}

				if (hit.name== "HowTo") {
						psh = PUSH.HOWTO;
						howto_sr.sprite = howto_tex [1];
						return;
				}

		}

		//ボタン離れた判定
		void EndTouchPosition(Vector3 vec){

				Vector3 ray = Camera.main.ScreenToWorldPoint(vec);

				Collider2D hit = Physics2D.OverlapPoint(ray);

				start_sr.sprite = start_tex [0];
				credit_sr.sprite = credit_tex [0];
				howto_sr.sprite = howto_tex [0];

				if (!hit)
						return;

				if (hit.name== "Start" && psh == PUSH.START ) {
						SoundManager.Instance.PlaySE (0);
						sm.SendMessage ("ChangeState", SceneManager.STATE.STAGE);
						return;
				}

				if (hit.name == "Credit" && psh == PUSH.CREDIT ) {
						SoundManager.Instance.PlaySE (0);
						return;
				}

				if (hit.name == "HowTo" && psh == PUSH.HOWTO ) {
						SoundManager.Instance.PlaySE (0);
						return;
				}

				psh = PUSH.NONE; 

		}


}
