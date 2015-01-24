using System.IO;
using UnityEngine;
using System.Collections;

public class StageButtonManager : MonoBehaviour {

		SceneManager scene;

		// Use this for initialization
		void Start () {
				scene = GameObject.Find ("SceneManager").GetComponent<SceneManager> ();
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

				if(hit.name=="Return")
				{
						SoundManager.Instance.PlaySE(1);
						scene.SendMessage ("ChangeState", SceneManager.STATE.TITLE);
						return;
				}

		}



		//ボタン離れた判定
		void EndTouchPosition(Vector3 vec){

				Vector3 ray = Camera.main.ScreenToWorldPoint(vec);

				Collider2D hit = Physics2D.OverlapPoint(ray);

				if (!hit)
						return;
				Debug.Log (hit.name);


				if (hit.name== "stage1") {
						SoundManager.Instance.PlaySE (0);
						scene.stage = SceneManager.STAGE.STAGE1;
						scene.SendMessage ("ChangeState", SceneManager.STATE.GAME);
						return;
				}

				if (hit.name== "stage2") {
						SoundManager.Instance.PlaySE (0);
						scene.stage = SceneManager.STAGE.STAGE2;
						scene.SendMessage ("ChangeState", SceneManager.STATE.GAME);
						return;
				}

				if (hit.name== "stage3") {
						SoundManager.Instance.PlaySE (0);
						scene.stage = SceneManager.STAGE.STAGE3;
						scene.SendMessage ("ChangeState", SceneManager.STATE.GAME);
						return;
				}


				if (hit.name== "OK") {
						SoundManager.Instance.PlaySE (0);
						scene.SendMessage ("ChangeState", SceneManager.STATE.TITLE);
						return;
				}

				if (hit.name == "share") {
						SoundManager.Instance.PlaySE (0);

						if (Application.platform != RuntimePlatform.OSXEditor) {
								// シェアします
								StartCoroutine ("ShareFunc");

						}
						return;
				}


		}


		IEnumerator ShareFunc(){
				string text = "[ハッピークローバー]現在のハイスコア："+scene.ranking[0]+" - 新感覚ミニゲーム・幸せの4つ葉を集めよう！光合成で成長促進！クローバーを食べ荒らすモグラには要注意?!";
				string url =	"iOS版ダウンロード:https://itunes.apple.com/jp/app/happikuroba/id953852248?mt=8, " +
						"Android版ダウンロード:https://play.google.com/store/apps/details?id=jp.co.kohki.HappyClover";
				SocialConnector.Share(
						text,
						url
				);
				yield return 0;
		}


}
