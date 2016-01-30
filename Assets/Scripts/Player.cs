using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : MonoBehaviour
{

	public Animator anim_controller;

    public float speed = 1;
    public bool playerControl = false;	
	public GameObject cam;



	// Use this for initialization
	void Awake ()
	{
		anim_controller = this.GetComponent<Animator> ();
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
				anim_controller.SetBool("right", true);
				anim_controller.SetBool("left", false);
				anim_controller.SetBool("up", false);
				anim_controller.SetBool("down", false);
			}
            else if (Input.GetAxis("Horizontal") < 0) {
				anim_controller.SetBool("right", false);
				anim_controller.SetBool("left", true);
				anim_controller.SetBool("up", false);
				anim_controller.SetBool("down", false);
			}
            else if (Input.GetAxis("Vertical") > 0) {
				anim_controller.SetBool("right", false);
				anim_controller.SetBool("left", false);
				anim_controller.SetBool("up", true);
				anim_controller.SetBool("down", false);
			}
            else if (Input.GetAxis("Vertical") < 0) {
				anim_controller.SetBool("right", false);
				anim_controller.SetBool("left", false);
				anim_controller.SetBool("up", false);
				anim_controller.SetBool("down", true);
			
			} else {
				anim_controller.SetBool("right", false);
				anim_controller.SetBool("left", false);
				anim_controller.SetBool("up", false);
				anim_controller.SetBool("down", false);
			}

        }

		cam.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, -10);	

		
	}

    public void SetPlayerControl(bool toggle)
    {
        playerControl = toggle;
    }
	
	public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
