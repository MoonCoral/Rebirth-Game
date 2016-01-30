using UnityEngine;
using System.Collections;

public class MovingBlock : MonoBehaviour {


	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			if (Input.GetButtonUp ("Action")) {

				double x = getDirection(other.gameObject);

				if (x == 2) {
					Vector3 temp = transform.position;
					temp.x = temp.x + (1 * Input.GetAxis ("Horizontal"));					
					transform.position = temp;
				} 
				else if (x == 1) {
					Vector3 temp = transform.position;
					temp.y = temp.y + (1 * Input.GetAxis ("Vertical"));					
					transform.position = temp;
				}

                AudioSource audio = gameObject.GetComponent<AudioSource>();
                if (!audio.isPlaying)
                {
                    audio.Play();
                }
			}
			
		}
	}

	double getDirection(GameObject other) {

		if (other.transform.position.x > transform.position.x - 0.5
			&& other.transform.position.x < transform.position.x + 0.5) {
			return 1;
		}
		else if (other.transform.position.y > transform.position.y - 0.5
			&& other.transform.position.y < transform.position.y + 0.5) {
			return 2;
		}
		return 0;
	}

}
