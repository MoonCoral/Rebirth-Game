using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Overlay : MonoBehaviour
{

    private GameObject overlayText;
    private GameObject startButton;
    private GameObject exitButton;
    private GameObject restartButton;

	// Use this for initialization
	void Awake () {
        overlayText = GameObject.Find("Overlay Text");
	    startButton = GameObject.Find("Start Button");
        exitButton = GameObject.Find("Exit Button");
        restartButton = GameObject.Find("Restart Button");
	}

    public void Inroduction()
    {
        overlayText.SetActive(true);
        startButton.SetActive(true);
        exitButton.SetActive(false);
        restartButton.SetActive(false);
    }

    public void MainGame()
    {
        overlayText.SetActive(false);
        startButton.SetActive(false);
    }

    public void EndGame()
    {
        overlayText.SetActive(true);
        exitButton.SetActive(true);
        restartButton.SetActive(true);
    }

    public void GameOver()
    {
        EndGame();
        overlayText.GetComponentInChildren<Text>().text = "Game over!";
    }

    public void Success()
    {
        EndGame();
        overlayText.GetComponentInChildren<Text>().text = "You have Succeded!";
    }
}
