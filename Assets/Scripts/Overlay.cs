using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Overlay : MonoBehaviour
{

    private GameObject overlayText;
    private GameObject startButton;
    private GameObject exitButton;
    private GameObject restartButton;
    private GameObject pauseButton;
    private GameObject panel;
    private GameObject shadowPanel;

	// Use this for initialization
	void Awake () {
        overlayText = GameObject.Find("Overlay Text");
	    startButton = GameObject.Find("Start Button");
        exitButton = GameObject.Find("Exit Button");
        restartButton = GameObject.Find("Restart Button");
	    pauseButton = GameObject.Find("Pause Button");
	    panel = GameObject.Find("Panel");
	    shadowPanel = GameObject.Find("Shadow Panel");
        
        overlayText.SetActive(false);
        startButton.SetActive(false);
        exitButton.SetActive(false);
        restartButton.SetActive(false);
        pauseButton.SetActive(false);
        panel.SetActive(false);
        shadowPanel.SetActive(false);
	}

    public void Introduction()
    {
        shadowPanel.SetActive(true);
        overlayText.SetActive(true);
        overlayText.GetComponentInChildren<Text>().text = "You have died\ndue to your own\nstupidity!\n\nYou have two minutes to revive yourself!";
        startButton.SetActive(true);
        exitButton.SetActive(false);
        restartButton.SetActive(false);
        pauseButton.SetActive(false);
    }

    public void MainGame()
    {
        overlayText.SetActive(false);
        shadowPanel.SetActive(false);
        startButton.SetActive(false);
        pauseButton.SetActive(true);
        pauseButton.GetComponentInChildren<Text>().text = "Pause";
    }

    public void EndGame()
    {
        overlayText.SetActive(true);
        exitButton.SetActive(true);
        restartButton.SetActive(true);
        pauseButton.SetActive(false);
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

    public void Pause(bool toggle)
    {
        panel.SetActive(toggle);
        exitButton.SetActive(toggle);
        restartButton.SetActive(toggle);

        if (toggle)
        {
            pauseButton.GetComponentInChildren<Text>().text = "Unpause";
        }
        else
        {
            pauseButton.GetComponentInChildren<Text>().text = "Pause";
        }
    }
}
