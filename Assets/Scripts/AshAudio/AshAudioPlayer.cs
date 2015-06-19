using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AshAudioPlayer : MonoBehaviour {

	Dictionary<string, AshAudio> BGMDictionary = new Dictionary<string, AshAudio>();
	Dictionary<string, AshAudio> SEDictionary = new Dictionary<string, AshAudio>();
	Dictionary<string, AudioList> audioSourceLists = new Dictionary<string, AudioList>();
	List<string> fadeList = new List<string> ();
	AshAudio creater;
	public static AshAudioPlayer instance = null;
	const string SoundObject = "game_sounds";
	const string AudioPath = "Audio/";
	string NowSceneName = "non";
	float repeatRate = 0.1f;

	enum AudioType : int
	{
		BGM = 1,
		SE = 2
	};

	class AudioList {
		public string Name;
		public string AudioFileName;
		public string FolderPath;
		public int AudioType;
		public string Scenes;
		public bool AutoPlay;
		public float Volume;
		public float Pitch;

		/// <summary>
		/// Initializes a new instance of the <see cref="AshAudioPlayer+AudioList"/> class.
		/// </summary>
		/// <param name="name">管理名</param>
		/// <param name="audioFileName">音楽ファイルの名称</param>
		/// <param name="folderPath">フォルダーのパス</param>
		/// <param name="audioType">Audio type.</param>
		/// <param name="scenes">自動再生するシーン名（Audio TypeがBGMの場合のみ）</param>
		/// <param name="autoPlay">自動再生するか？</param>
		/// <param name="volume">ボリュームの音量</param>
		/// <param name="pitch">Pitch</param>
		public AudioList(string name, string audioFileName, string folderPath, int audioType, string scenes, bool autoPlay, float volume, float pitch)
		{
			this.Name = name;
			this.AudioFileName = audioFileName;
			this.FolderPath = folderPath;
			this.AudioType = audioType;
			this.Scenes = scenes;
			this.AutoPlay = autoPlay;
			this.Volume = volume;
			this.Pitch = pitch;
		}

	}

	void Awake()
	{
		DontDestroyOnLoad (this);
		instance = this;

		audioSourceLists.Add ("coin", new AudioList("coin", "coin07", AudioPath, (int)AudioType.SE, "", false, 1.0f, 1.0f));
		audioSourceLists.Add ("stage", new AudioList("stage", "select07", AudioPath, (int)AudioType.BGM, "Game", true, 1.0f, 1.0f));

		CreateGroup (SoundObject);
		CreateAudioSource ();
	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (NowSceneName != Application.loadedLevelName) {
			NowSceneName = Application.loadedLevelName;
			List<string> playSoundKey = new List<string> ();
			foreach (string key in audioSourceLists.Keys) {
				if (audioSourceLists [key].AutoPlay && audioSourceLists [key].Scenes == NowSceneName) {
					playSoundKey.Add (key);
				}
			}
			StopBgm (playSoundKey);
			StartBgm (playSoundKey);
		}
	}

	private void StartBgm(List<string> playList)
	{
		playList.ForEach(delegate(string key)
			{
				BGMDictionary [key].Play ();
				//FadeIn(key, 0.01f);
			});
	}

	private void StopBgm(List<string> escapeList)
	{
		foreach (string key in BGMDictionary.Keys) {
			if (escapeList.IndexOf (key) == -1) {
				BGMDictionary [key].Stop ();
			}
		}
	}

	public void CreateAudioSource()
	{
		GameObject GameSoundObject = transform.FindChild (SoundObject).gameObject;
		foreach (string key in audioSourceLists.Keys) {
			AudioList audioList = audioSourceLists [key];
			AshAudio audio = new AshAudio (GameSoundObject, audioList.Name, audioList.AudioFileName, audioList.FolderPath, audioList.Volume, audioList.Pitch);
			if (audioList.AudioType == (int)AudioType.BGM) {
				BGMDictionary.Add (audioList.Name, audio);
			} else {
				SEDictionary.Add (audioList.Name, audio);
			}
		}
	}

	public AshAudio SoundResource(string name)
	{
		if (BGMDictionary.ContainsKey (name)) {
			return BGMDictionary [name];
		} else if(SEDictionary.ContainsKey(name)) {
			return SEDictionary [name];
		}
		return null;
	}

	public void FadeIn(string name, float upVolume)
	{
		if (BGMDictionary.ContainsKey (name)) {
			if (fadeList.Count <= 0) {
				InvokeRepeating ("Fade", 0f, repeatRate);
			}			
			BGMDictionary [name].FadeIn (upVolume);
			fadeList.Add (name);
		}
	}

	public void FadeOut(string name, float downVolume)
	{
		if (BGMDictionary.ContainsKey (name)) {
			if (fadeList.Count <= 0) {
				InvokeRepeating ("Fade", 0f, repeatRate);
			}			
			BGMDictionary [name].FadeOut (downVolume);
			fadeList.Add (name);
		}
	}

	void Fade()
	{

		for (int i = 0; i < fadeList.Count; i++) {
			if (!BGMDictionary [fadeList[i]].Fade ()) {
				fadeList.Remove (fadeList[i]);
			}
		}

		if (fadeList.Count <= 0) {
			CancelInvoke ("Fade");
		}
	}

	public void CreateGroup(string name) {
		GameObject go = new GameObject ();
		go.name = name;
		go.transform.parent = transform;
	}
}
