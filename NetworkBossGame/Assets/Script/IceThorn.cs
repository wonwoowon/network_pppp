﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceThorn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.CompareTag("MainFloor") || _col.CompareTag("Floor")||_col.CompareTag("Wall"))
            Destroy(gameObject);
    }
}
