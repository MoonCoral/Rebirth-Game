using UnityEngine;
using System.Collections;

public class Temple : MonoBehaviour {

	private int state;
	private float time;
	private bool elapsing;

	public Player player;
	public Overlay overlay;
	
	void Awake ()
	{
		state = 0;
		time = 20.0f;
		elapsing = false;

		Intro ();
	}

	void Update ()
	{
		if (Time.realtimeSinceStartup >= time)
		{
			StopTime();
			Failure();
		}

		//if if if
	}

	public void StartTime()
	{
		time += Time.realtimeSinceStartup;
		this.elapsing = true;
	}

	public void StopTime()
	{
		time -= Time.realtimeSinceStartup;
	}

	public float SecondsLeft()
	{
		return time - Time.realtimeSinceStartup;
	}

	private void Intro()
	{
		Debug.Log ("Intro");
		//maybe called from somewhere else
		StartTime ();
		Debug.Log ("Start");
	}

	private void Failure()
	{
		Debug.Log ("Game Over");
	}

}
