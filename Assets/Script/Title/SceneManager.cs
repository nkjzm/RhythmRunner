using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;



public class SceneManager : MonoBehaviour {

		public int UserMoney=100;

		static bool isCreated=false;
		public enum STATE:int {TITLE,MAP,GAME,PAUSE,STAGE};
		public enum STAGE:int{STAGE1,STAGE2,STAGE3,STAGE_MAX};

		public bool isFirstState = true;

		public STATE	state = STATE.TITLE;
		public STAGE	stage = STAGE.STAGE1;

		public int[] ranking = new int[10];

		void Awake()
		{
				if (isCreated) {
						Destroy (gameObject);
				}
				DontDestroyOnLoad (this);
				isCreated = true;


				LoadPersistantData ();

		}

		// Use this for initialization
		void Start () {
		}


		// Update is called once per frame
		void Update () {

				//Android用処理
				if (Input.GetKeyDown (KeyCode.Escape)) {
						Application.Quit ();
						Debug.Log ("input escape");
				}

		}


		//状態遷移
		void  ChangeState(STATE st)
		{
				state=st;
				if(st==STATE.GAME){
						FadeManager.Instance.LoadLevel("Game", 0.5f);
						Debug.Log("Scene to GAME");
						return;
				}
				if(st==STATE.TITLE){
						FadeManager.Instance.LoadLevel("Title", 0.5f);
						Debug.Log("Scene to TITLE");
						return;
				}
				if(st==STATE.STAGE){
						FadeManager.Instance.LoadLevel("Stage", 0.5f);
						Debug.Log("Scene to Stage");
						return;
				}
		}

		void OnOption()
		{
				Application.LoadLevelAdditive ("Option");
				Debug.Log("Option");
		}

		//ユーザーデータの書き込み
		public void SavePersistantData()
		{
				string url = "";
				if (Application.platform == RuntimePlatform.Android) {
						url = Application.persistentDataPath + "/user_data.txt";
				}else if (Application.platform == RuntimePlatform.IPhonePlayer) {
						url = Application.persistentDataPath + "/user_data.txt";
				}else{
						url = Application.dataPath+"/StreamingAssets"+"/user_data.txt";
				}

				StreamWriter sw = new StreamWriter(url);

				//1行目:初回起動フラグ
				sw.WriteLine (isFirstState?1:0);

				//2,3行目:音量設定
				sw.WriteLine (SoundManager.Instance.volume.BGM_Mute?1:0);
				sw.WriteLine (SoundManager.Instance.volume.SE_Mute?1:0);

				sw.Close();

				LoadPersistantData ();
		}

		//ユーザーデータの読み込み
		public void LoadPersistantData()
		{
				string url = "";
				if (Application.platform == RuntimePlatform.Android) {
						url = Application.persistentDataPath + "/user_data.txt";
				}else if (Application.platform == RuntimePlatform.IPhonePlayer) {
						url = Application.persistentDataPath + "/user_data.txt";
				}else{
						url = Application.dataPath+"/StreamingAssets"+"/user_data.txt";
				}
				if (!System.IO.File.Exists (@url))
						return;
				TextReader sr = new StreamReader (url);

				//1行目:初回起動フラグ
				isFirstState= int.Parse (sr.ReadLine ()) == 0 ? false : true;

				//2,3行目:音量設定
				SoundManager.Instance.volume.BGM_Mute= int.Parse (sr.ReadLine()) == 0 ? false : true;
				SoundManager.Instance.volume.SE_Mute= int.Parse (sr.ReadLine ()) == 0 ? false : true;

				sr.Close ();
		}


}
