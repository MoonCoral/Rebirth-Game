using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Overlay : MonoBehaviour
{

    private GameObject overlayText;
	private GameObject overlayText2;
	private GameObject overlayText3;
    private GameObject startButton;
    private GameObject exitButton;
    private GameObject restartButton;
    private GameObject pauseButton;
    private GameObject panel;
    private GameObject shadowPanel;
	private GameObject titleText;
	private GameObject continueButton;
	private GameObject startPanel;

	private string overlayString;

	// Use this for initialization
	void Awake () {
        overlayText = GameObject.Find("Overlay Text");
		overlayText2 = GameObject.Find("Overlay Text 2");
		overlayText3 = GameObject.Find("Overlay Text 3");
		startButton = GameObject.Find("Start Button");
        exitButton = GameObject.Find("Exit Button");
        restartButton = GameObject.Find("Restart Button");
	    pauseButton = GameObject.Find("Pause Button");
	    panel = GameObject.Find("Panel");
		shadowPanel = GameObject.Find("Shadow Panel");
		titleText = GameObject.Find("Title Text");
		continueButton = GameObject.Find("Continue Button");
		startPanel = GameObject.Find("Start Panel");
        
        overlayText.SetActive(false);
        startButton.SetActive(false);
        exitButton.SetActive(false);
        restartButton.SetActive(false);
		continueButton.SetActive(false);
		pauseButton.SetActive(false);
		panel.SetActive(false);
        shadowPanel.SetActive(false);
		titleText.SetActive(false);
	}

    public void Introduction()
    {
        shadowPanel.SetActive(true);
        overlayText.SetActive(true);
		overlayString = "Chip, you have fallen to your death...\n\n" +
			"You have been granted five minutes to revive yourself!\n\n" +
				"You must solve all four trials in order to retrieve the artifacts from the chests!\n\n" +
				"Use WASD to move, E to push/interact and click on the clock to reset time.\n\n";
		titleText.SetActive(true);
		titleText.GetComponentInChildren<Text>().text = "REBIRTH";
		startButton.SetActive(true);
        exitButton.SetActive(false);
        restartButton.SetActive(false);
        pauseButton.SetActive(false);
		updateOverlay ();
    }

	public void updateOverlay() {
		if (overlayText.activeSelf) {
			overlayText2.SetActive (true);
			overlayText3.SetActive (true);
			overlayText.GetComponentInChildren<Text> ().text = overlayString;
			overlayText2.GetComponentInChildren<Text> ().text = overlayString;
			overlayText3.GetComponentInChildren<Text> ().text = overlayString;
		} else {
			overlayText2.SetActive (false);
			overlayText3.SetActive (false);
		}
	}

    public void MainGame()
    {
        overlayText.SetActive(false);
        shadowPanel.SetActive(false);
		startPanel.SetActive (false);
		titleText.SetActive(false);
        startButton.SetActive(false);
        pauseButton.SetActive(true);
        pauseButton.GetComponentInChildren<Text>().text = "Pause";
		updateOverlay ();
    }

    public void EndGame()
    {
        overlayText.SetActive(true);
        exitButton.SetActive(true);
		titleText.SetActive(true);
        restartButton.SetActive(true);
        pauseButton.SetActive(false);
		startPanel.SetActive (true);
		updateOverlay ();
    }

    public void GameOver()
    {
		overlayString = "You ran out of time!";
		EndGame();;
    }

    public void Success()
    {
		continueButton.SetActive(true);
		overlayText.SetActive(true);
		pauseButton.SetActive(false);
		startPanel.SetActive (true);
		overlayString = "Congratulations, you have rejoined the world of the living!\n\n" +
			"The god of time isn't done with you yet...\n\n" +
			"Your story is yet to unfold...\n\n" +
        	"Will you forever live in the shadow of your father?";
		updateOverlay ();
    }

	public void Credits() {
		continueButton.SetActive(false);
		overlayString = "Team Two Cubed will return...\n\n" +
			"Raluca Gaina\n" +
			"o-s-s\n" +
			"Paul Leonard\n" +
			"Jonathan Nichols\n" +
			"Olivier Thill\n" +
			"Ovidio Villarreal\n" +
			"Harvey Wigton\n";
		EndGame();
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
		updateOverlay ();
    }
}
