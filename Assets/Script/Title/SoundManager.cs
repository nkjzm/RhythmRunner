using UnityEngine;
using System;
using System.Collections;


// 音管理クラス
public class SoundManager : MonoBehaviour {

	protected static SoundManager instance;

	public static SoundManager Instance
	{
		get
		{
			if(instance == null)
			{
				instance = (SoundManager) FindObjectOfType(typeof(SoundManager));

				if (instance == null)
				{
					Debug.LogError("SoundManager Instance Error");
				}
			}

			return instance;
		}
	}

	static bool isCreated=false;

	// 音量
	public SoundVolume volume = new SoundVolume();

	// === AudioSource ===
	// BGM
	private AudioSource BGMsource;
	// SE
	private AudioSource[] SEsources = new AudioSource[16];
	// 音声
	private AudioSource VoiceSources;

	// === AudioClip ===
	// BGM
	public AudioClip[] BGM;
	// SE
	public AudioClip[] SE;
	// 音声
	public AudioClip[] Voice;

	void Awake (){
		if (isCreated) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (this);
		isCreated = true;

		// 全てのAudioSourceコンポーネントを追加する

		// BGM AudioSource
		BGMsource = gameObject.AddComponent<AudioSource>();
		// BGMはループを有効にする
		BGMsource.loop = true;

		// SE AudioSource
		for(int i = 0 ; i < SEsources.Length ; i++ ){
			SEsources[i] = gameObject.AddComponent<AudioSource>();
		}

		// 音声 AudioSource
		VoiceSources = gameObject.AddComponent<AudioSource>();
		// BGMはループを有効にする
		VoiceSources.loop = true;

	}

	void Update () {
		// ミュート設定
		BGMsource.mute = volume.BGM_Mute;
		foreach(AudioSource source in SEsources ){
			source.mute = volume.SE_Mute;
		}
		VoiceSources.mute = volume.SE_Mute;

		// ボリューム設定
		BGMsource.volume = volume.BGM;
		foreach(AudioSource source in SEsources ){
			source.volume = volume.SE;
		}
		VoiceSources.volume = volume.SE;
	}



	// ***** BGM再生 *****
	// BGM再生
	public void PlayBGM(int index){
		if( 0 > index || BGM.Length <= index ){
			return;
		}
		// 同じBGMの場合は何もしない
		//				if( BGMsource.clip == BGM[index] ){
		//						return;
		//				}
		BGMsource.Stop();
		BGMsource.clip = BGM[index];
		BGMsource.Play();
	}

	// BGM停止
	public void StopBGM(){
		BGMsource.Stop();
		BGMsource.clip = null;
	}

	// BGM一時停止
	public void Pause(){
		BGMsource.Pause();
	}

	// BGM再開
	public void Restart(){
		BGMsource.Play();
	}


	// ***** SE再生 *****
	// SE再生
	public void PlaySE(int index){
		if( 0 > index || SE.Length <= index ){
			return;
		}

		// 再生中で無いAudioSouceで鳴らす
		foreach(AudioSource source in SEsources){
			if( false == source.isPlaying ){
				source.clip = SE[index];
				source.Play();
				return;
			}
		}  
	}

	// SE停止
	public void StopSE(){
		// 全てのSE用のAudioSouceを停止する
		foreach(AudioSource source in SEsources){
			source.Stop();
			source.clip = null;
		}  
	}


	// ***** 音声再生 *****
	// 音声再生
	public void PlayVoice(int index){
		if( 0 > index || Voice.Length <= index ){
			return;
		}
		if( VoiceSources.clip == Voice[index] ){
			return;
		}
		VoiceSources.Stop();
		VoiceSources.clip = Voice[index];
		VoiceSources.Play();
	}

	// 音声停止
	public void StopVoice(){
		// 全ての音声用のAudioSouceを停止する
		VoiceSources.Stop();
		VoiceSources.clip = null;
	}
}


// 音量クラス
[Serializable]
public class SoundVolume{
	public float BGM = 1.0f;
	public float Voice = 1.0f;
	public float SE = 1.0f;
	public bool BGM_Mute = false;
	public bool SE_Mute = false;
	bool isInited=false;

	public void Init(){
		BGM = 1.0f;
		Voice = 1.0f;
		SE = 1.0f;
		if (!isInited) {
			BGM_Mute = false;
			SE_Mute = false;
			isInited = true;
		}
	}
}

/*
■使うとき

SoundManager.Instance.PlayBGM(0);
SoundManager.Instance.PlaySE(2);
SoundManager.Instance.PlayVoice(3);

*/