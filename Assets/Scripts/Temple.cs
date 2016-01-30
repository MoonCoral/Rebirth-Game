using UnityEngine;
using System.Collections;

public class Temple : MonoBehaviour {

	private int state;
	private float time;
	private bool elapsing;

	public Player Player;
	public Overlay Overlay;
	
	void Awake ()
	{
		Restart();
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
	    if ( !elapsing )
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
		time = 10.0f;
		elapsing = false;
	}

	public void Intro()
	{
		Debug.Log ("Intro");
        Overlay.Inroduction();
		state = 1;
	}

	public void MainGame()
	{
        Debug.Log("Start");
        state = 2;
        Overlay.MainGame();
		StartTime();
	}

	public void Failure()
	{
		Debug.Log ("Game Over");
        Overlay.GameOver();
		state = 3;
	}

    public void Succes()
    {
        Debug.Log("Succes");
        Overlay.Success();
        state = 3;
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void Restart()
    {
        Debug.Log("Restart");
        Init();
        Intro();
    }

}
