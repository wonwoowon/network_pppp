﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour {

    //speed of player bullet
    private float speed = 7f;
    private Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {
		
	}
	void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    //this object's direction define by player...
    public void shoot(int direction)
    {
        if(direction==1)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            rb2d.velocity = new Vector3(speed, 0, 0);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            rb2d.velocity = new Vector3(-speed, 0, 0);
        }
    }
    //if it collision with mainfloor, destroy...
    void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.CompareTag("MainFloor"))
        {
            Destroy(gameObject);
           // GameManager.instance.player.GetComponent<Player>().setAttackFlag();
        }
    }
}
