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
		Init();

		Intro ();
	}

	void Update ()
	{
		if (Time.realtimeSinceStartup >= time && elapsing)
		{
			StopTime();
		    time = 0.0f;
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
	    this.elapsing = false;
	}

	public float SecondsLeft()
	{
	    if ( elapsing )
	    {
	        return time;
	    }
	    else if (state < 3)
		{
			return time - Time.realtimeSinceStartup;
		}
		else
		{
			return 0.0f;
		}
	}

	public void Init()
	{
		state = 0;
		time = 120.0f;
		elapsing = false;
	}

	public void Intro()
	{
		Debug.Log ("Intro");
		state = 1;
	}

	public void MainGame()
	{
		StartTime();
		Debug.Log("Start");
		state = 2;
	}

	public void Failure()
	{
		Debug.Log ("Game Over");
		state = 3;
	}

}
