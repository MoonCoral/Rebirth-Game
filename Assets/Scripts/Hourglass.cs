using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hourglass : MonoBehaviour {

	private GameObject timer;
	private GameObject topSand;
	private GameObject bottomSand;
	private Image topSandImage;
	private Image bottomSandImage;

	public Temple temple;

	void Awake() {
		timer = GameObject.Find("Hourglass");

		topSand = GameObject.Find("TopSand");
		bottomSand = GameObject.Find("BottomSand");
		topSandImage = topSand.GetComponent<Image>();
		bottomSandImage = bottomSand.GetComponent<Image>();
	}

	// Update is called once per frame
	void Update () {
		topSandImage.fillAmount = temple.SecondsLeft()/120;
		bottomSandImage.fillAmount = (120-temple.SecondsLeft())/120;
	}
}
