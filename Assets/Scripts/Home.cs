using UnityEngine;
using System.Collections;

public class Home : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

	public void GoGameScene()
	{
		Application.LoadLevel ("Game");
	}

}
