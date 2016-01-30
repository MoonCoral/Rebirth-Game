using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StopWatch : MonoBehaviour {

	private GameObject stopwatch;
	private Image stopwatchImage;
	private GameObject shortHand;
	private Image shortHandImage;
	private GameObject longHand;
	private Image longHandImage;
	private float startTime = 2.5f;
	private float time = 0;

	void Awake() {
		stopwatch = GameObject.Find("StopWatch");
		stopwatchImage = stopwatch.GetComponent<Image>();
		shortHand = GameObject.Find("StopWatchShortHand");
		shortHandImage = stopwatch.GetComponent<Image>();
		longHand = GameObject.Find("StopWatchLongHand");
		longHandImage = stopwatch.GetComponent<Image>();
	}

	void isClicked() {
		time = startTime;
	}

	void Update() {
		if (time>0.0) {
			if (time<0.5){
				shortHand.transform.Rotate (Vector3.forward * -1);
				longHand.transform.Rotate (Vector3.forward * 2);
			} else if (time<1) {
				shortHand.transform.Rotate (Vector3.forward * -2);
				longHand.transform.Rotate (Vector3.forward * 4);
			} else if (time<1.5) {
				shortHand.transform.Rotate (Vector3.forward * -4);
				longHand.transform.Rotate (Vector3.forward * 8);
			} else if (time<2) {
				shortHand.transform.Rotate (Vector3.forward * -8);
				longHand.transform.Rotate (Vector3.forward * 16);
			} else if (time<2.5) {
				shortHand.transform.Rotate (Vector3.forward * -16);
				longHand.transform.Rotate (Vector3.forward * 32);
			}
			time-=Time.deltaTime;
		}
	}

}
