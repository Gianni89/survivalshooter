using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Intro : MonoBehaviour {

	public AudioMixerSnapshot paused;
	public AudioMixerSnapshot unpaused;
	public GameObject player;
	public Canvas introCanvas;

	public bool introOn = false;


	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == player)
		{
			Pause ();
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && introOn) 
		{
			EndIntro();
		}
	}

	public void Pause()
	{
		PlayIntro ();
		Time.timeScale = 0;
		Lowpass ();
		
	}

	void PlayIntro()
	{
		introOn = true;
		introCanvas.gameObject.SetActive (true);
	}

	void EndIntro()
	{
		introOn = false;
		introCanvas.gameObject.SetActive (false);
		Time.timeScale = 1;
		Lowpass ();
		gameObject.SetActive (false);
	}

	void Lowpass()
	{
		if (Time.timeScale == 0)
		{
			paused.TransitionTo(.01f);
		}
		
		else
			
		{
			unpaused.TransitionTo(.01f);
		}
	}
	
}
