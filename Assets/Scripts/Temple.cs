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

    public void Init()
	{
		state = 0;
		time = 20.0f;
		elapsing = false;
	}

    public void Intro()
	{
		Debug.Log ("Intro");
	}

	public void MainGame()
	{
		StartTime();
		Debug.Log("Start");
	}

    public void Failure()
	{
		Debug.Log ("Game Over");
	}

}
