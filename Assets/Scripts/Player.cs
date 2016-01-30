using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : MonoBehaviour
{

	public Animator anim;

    public float speed = 1;
    public bool playerControl = false;	
	public GameObject cam;

	// Use this for initialization
	void Awake ()
	{
		anim = this.GetComponent<Animator> ();
		cam = GameObject.Find ("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate()
    {

        Vector3 temp = transform.position;

        if (playerControl)
        {
            temp.x += (speed * Input.GetAxis("Horizontal"));
            temp.y += (speed * Input.GetAxis("Vertical"));
			transform.position = temp;
						
			if (Input.GetAxis("Horizontal") > 0) {
				anim.SetBool("right", true);
				anim.SetBool("left", false);
				anim.SetBool("up", false);
				anim.SetBool("down", false);
			}
            else if (Input.GetAxis("Horizontal") < 0) {
				anim.SetBool("right", false);
				anim.SetBool("left", true);
				anim.SetBool("up", false);
				anim.SetBool("down", false);
			}
            else if (Input.GetAxis("Vertical") > 0) {
				anim.SetBool("right", false);
				anim.SetBool("left", false);
				anim.SetBool("up", true);
				anim.SetBool("down", false);
			}
            else if (Input.GetAxis("Vertical") < 0) {
				anim.SetBool("right", false);
				anim.SetBool("left", false);
				anim.SetBool("up", false);
				anim.SetBool("down", true);
			} else {
				anim.SetBool("right", false);
				anim.SetBool("left", false);
				anim.SetBool("up", false);
				anim.SetBool("down", false);

			}

        }

		cam.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, -10);	

		
	}

    public void SetPlayerControl(bool toggle)
    {
        playerControl = toggle;
    }
}
