using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : MonoBehaviour
{

	

    public float speed = 1;
    public bool playerControl = false;

    public Sprite up;
    public Sprite down;
    public Sprite right;
    public Sprite left;
    public SpriteRenderer renderer;
	public GameObject cam;

	// Use this for initialization
	void Awake ()
	{
        renderer = this.GetComponent<SpriteRenderer>();
        renderer.sprite = down;
		cam = GameObject.Find ("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
		cam.transform.position = new Vector3 (transform.position.x, transform.position.y, -10);	
	}

    void FixedUpdate()
    {

        Vector3 temp = transform.position;

        if (playerControl)
        {
            temp.x += (speed * Input.GetAxis("Horizontal"));
            temp.y += (speed * Input.GetAxis("Vertical"));

            if (Input.GetAxis("Horizontal") > 0)
                renderer.sprite = right;
            else if (Input.GetAxis("Horizontal") < 0)
                renderer.sprite = left;
            else if (Input.GetAxis("Vertical") > 0)
                renderer.sprite = up;
            else if (Input.GetAxis("Vertical") < 0)
                renderer.sprite = down;

            transform.position = temp;
        }

    }

    public void SetPlayerControl(bool toggle)
    {
        playerControl = toggle;
    }
}
