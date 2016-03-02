﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Clone : MonoBehaviour
{
	
	private Animator anim_controller;
	
	public float speed = 1;
    private Vector3 velocity;	
	
	// Use this for initialization
	void Awake ()
	{
		anim_controller = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void FixedUpdate()
	{
		Vector3 temp = transform.position;


        temp.x += speed * velocity.x;
        temp.y += speed * velocity.y;
	    temp.z += speed * velocity.z;

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
	
	public void SetVelocity(Vector3 velocity)
	{
	    this.velocity = velocity;
	}

    public Vector3 GetVelocity()
    {
        return velocity;
    }

	public void SetPosition(Vector3 position)
	{
		transform.position = position;
	}
	
	public Vector3 GetPosition()
	{
		return transform.position;
	}
}