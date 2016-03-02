using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Overlay : MonoBehaviour
{

    private GameObject overlayText;
	private GameObject overlayText2;
	private GameObject overlayText3;
	private GameObject messageText;
	private GameObject messageText2;
	private GameObject messageText3;
	private GameObject nameText;
    private GameObject startButton;
    private GameObject exitButton;
    private GameObject restartButton;
    private GameObject pauseButton;
    private GameObject panel;
    private GameObject shadowPanel;
	private GameObject titleText;
	private GameObject continueButton;
	private GameObject startPanel;

	private GameObject chestText;

	private string overlayString;
	private string messageString;
	private string nameString;

	// Use this for initialization
	void Awake () {
        overlayText = GameObject.Find("Overlay Text");
		overlayText2 = GameObject.Find("Overlay Text 2");
		overlayText3 = GameObject.Find("Overlay Text 3");
		messageText = GameObject.Find("Message Text");
		messageText2 = GameObject.Find("Message Text 2");
		messageText3 = GameObject.Find("Message Text 3");
		nameText = GameObject.Find("Name Text");
		startButton = GameObject.Find("Start Button");
        exitButton = GameObject.Find("Exit Button");
        restartButton = GameObject.Find("Restart Button");
	    pauseButton = GameObject.Find("Pause Button");
	    panel = GameObject.Find("Panel");
		shadowPanel = GameObject.Find("Shadow Panel");
		titleText = GameObject.Find("Title Text");
		continueButton = GameObject.Find("Continue Button");
		startPanel = GameObject.Find("Start Panel");
		chestText = GameObject.Find ("ChestText");
        
        overlayText.SetActive(false);
		nameText.SetActive (false);
        startButton.SetActive(false);
        exitButton.SetActive(false);
        restartButton.SetActive(false);
		continueButton.SetActive(false);
		pauseButton.SetActive(false);
		panel.SetActive(false);
        shadowPanel.SetActive(false);
		titleText.SetActive(false);
		chestText.SetActive(false);
	}

	public void IntroCutScene()
	{
		nameText.SetActive (true);
		// add names and messages here
		updateConversation ();
	}

    public void Introduction()
    {
        shadowPanel.SetActive(true);
		nameText.SetActive (false);
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
		updateConversation ();
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

	public void updateConversation() {
		if (nameText.activeSelf) {
			messageText.SetActive (true);
			messageText2.SetActive (true);
			messageText3.SetActive (true);
			nameText.GetComponentInChildren<Text> ().text = nameString;
			messageText.GetComponentInChildren<Text> ().text = messageString;
			messageText2.GetComponentInChildren<Text> ().text = messageString;
			messageText3.GetComponentInChildren<Text> ().text = messageString;
		} else {
			messageText.SetActive (false);
			messageText2.SetActive (false);
			messageText3.SetActive (false);
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
		nameString = "Chip";
		messageString = "test";
		nameText.SetActive (true);
		updateOverlay ();
		updateConversation ();
    }

    public void EndGame()
    {
        overlayText.SetActive(true);
		nameText.SetActive (false);
        exitButton.SetActive(true);
		titleText.SetActive(true);
        restartButton.SetActive(true);
        pauseButton.SetActive(false);
		startPanel.SetActive (true);
		updateOverlay ();
		updateConversation ();
    }

    public void GameOver()
    {
		overlayString = "You ran out of time!";
		EndGame();
    }

    public void Success()
    {
		continueButton.SetActive(true);
		nameText.SetActive (false);
		overlayText.SetActive(true);
		pauseButton.SetActive(false);
		startPanel.SetActive (true);
		overlayString = "Congratulations, you have rejoined the world of the living!\n\n" +
			"The god of time isn't done with you yet...\n\n" +
			"Your story is yet to unfold...\n\n" +
        	"Will you forever live in the shadow of your father?";
		updateOverlay ();
		updateConversation ();
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

	public void setConversationText(string name, string message) {
		nameString = name;
		messageString = message;
	}

	public void ChestOpenText()
	{
		Debug.Log("chest unlocked");
		StartCoroutine (ChestOpenTextHelper());
		AudioSource audio = chestText.GetComponent<AudioSource>();
		if (!audio.isPlaying)
		{
			audio.Play();
		}
	}

	public IEnumerator ChestOpenTextHelper()
	{
		chestText.SetActive (true);
		yield return new WaitForSeconds(1);
		chestText.SetActive (false);
	}
}
