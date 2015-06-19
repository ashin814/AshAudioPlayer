using UnityEngine;
using System.Collections;

public class AshAudio {

	AudioSource audioSource;
	float defaultVolume;
	float changeVolume;
	int fadeType;
	bool fadeing = false;

	enum FadeType : int
	{
		In = 1,
		Out = 2
	};

	public AshAudio(GameObject gameSoundOjbect,  string audioName, string audioFileName, string path, float volume, float pitch)
	{
		audioSource = gameSoundOjbect.AddComponent<AudioSource> ();
		audioSource.playOnAwake = false;
		AudioClip audioClip = Resources.Load (path + audioFileName,typeof(AudioClip)) as AudioClip;
		audioSource.clip = audioClip;
		audioSource.volume = volume;
		audioSource.pitch = pitch;

		defaultVolume = volume;
	}

	public void Play()
	{
		audioSource.Play ();
	}

	public void Stop()
	{
		audioSource.Stop ();
	}

	public void SetVolume(float volume)
	{
		audioSource.volume = volume;
	}

	public void FadeIn(float upVolume)
	{
		audioSource.volume = 0;
		FadeStart ((int)FadeType.In, upVolume);
		Play ();
	}

	public void FadeOut(float downVolume)
	{
		FadeStart ((int)FadeType.Out, downVolume);
	}

	void FadeStart(int fadeType, float volume)
	{
		this.fadeType = fadeType;
		this.changeVolume = volume;
		this.fadeing = true;
	}

	public bool Fade()
	{
		if (fadeType == (int)FadeType.In) {
			audioSource.volume += this.changeVolume;
			if (audioSource.volume >= defaultVolume) {
				audioSource.volume = defaultVolume;
				this.fadeing = false;
			}
		} else {
			audioSource.volume -= this.changeVolume;
			if (audioSource.volume <= 0) {
				Stop ();
				audioSource.volume = defaultVolume;
				this.fadeing = false;
			}
		}
		return this.fadeing;
	}
}
