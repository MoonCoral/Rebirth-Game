using UnityEngine;
using System.Collections;

public class Temple : MonoBehaviour {

	private int state;
	private float time;
	private bool elapsing;
	
	void Awake ()
	{
		state = 0;
		time = 20.0f;
		elapsing = false;

		intro ();
	}

	void Update ()
	{
		if (Time.realtimeSinceStartup >= time)
		{
			stopTime();
			failure();
		}

		//if if if
	}

	void startTime()
	{
		time += Time.realtimeSinceStartup;
		this.elapsing = true;
	}

	void stopTime()
	{
		time -= Time.realtimeSinceStartup;
	}

	float secondsLeft()
	{
		return time - Time.realtimeSinceStartup;
	}

	void intro()
	{
		Debug.Log ("Intro");
		//maybe called from somewhere else
		startTime ();
		Debug.Log ("Start");
	}

	void failure()
	{
		Debug.Log ("Game Over");
	}

}
