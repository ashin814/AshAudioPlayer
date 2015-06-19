using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlaySe()
	{
		AshAudioPlayer.instance.SoundResource ("coin").Play ();
	}

	public void BackHome()
	{
		Application.LoadLevel ("Home");
	}

}
