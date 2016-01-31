using UnityEditor;
using UnityEngine;
using System.Collections;

public class PitScript : MonoBehaviour
{

    private TileEngine tileEngine;
	AudioSource audio;

    void Awake()
    {
        tileEngine = FindObjectOfType<TileEngine>();
		audio = this.GetComponent<AudioSource> ();
    }

	void OnTriggerStay2D(Collider2D other) {
		if (!audio.isPlaying)
			audio.Play();
		if (other.gameObject.tag == "Player") {
		    if (tileEngine.playerRoom() == 2)
		    {
                other.transform.position = new Vector3(29, -19, -1);				
		    }
		    else
		    {
                other.transform.position = new Vector3(21, -28, -1);				
		    }			
		}
		else if (other.gameObject.name.Equals("move")) {
			BoxCollider2D[] cols  = other.gameObject.GetComponents<BoxCollider2D> ();
			for (int i = 0; i < cols.Length; i++)
				cols[i].enabled = false;
			this.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		}
	}
}
