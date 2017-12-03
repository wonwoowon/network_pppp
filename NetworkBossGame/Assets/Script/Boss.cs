﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public GameObject thorn_icile;
    public bool[] patternBoolean;

    private int health;
    private float moveSpeed = 10;
    private Rigidbody2D rb2d;
    private Animator anim;
    private bool activeBool;
    
    private BossBullet bulletManager;
    bool direction;
    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bulletManager = FindObjectOfType<BossBullet>();
        direction = true;
        activeBool = true;
        patternBoolean = new bool[2];
        for (int i = 0; i < 2; i++)
            patternBoolean[i] = true;
        StartCoroutine("DropThorn");
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.Alpha3)&&activeBool)
        {
            direction = true;
            StartCoroutine("move");
        }
        if (Input.GetKey(KeyCode.Alpha1)&&activeBool)
        {
            direction = false;
            StartCoroutine("move");
        }*/

        //dropThorn();
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle_Boss"))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && patternBoolean[0])
            {
                StartCoroutine("ShootBullet");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && patternBoolean[1])
            {
                StartCoroutine("ReadyBeam");

            }
        }
    }


    void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.tag == "UserBullet")
        {
            //Debug.Log("AAA");
            anim.SetTrigger("Damaged");
            StartCoroutine("Damaged");
        }
    }

    void dropThorn()
    {
        Vector3 position = GameManager.instance.player.transform.position;
        position.y = 5f;
        position.x = position.x + Random.Range(-0.5f, 0.5f);
        if (thorn_icile == null)
            Debug.Log("is NULL...");
        Instantiate(thorn_icile, position, Quaternion.identity);
    }
    IEnumerator ShootBullet()
    {
        patternBoolean[0] = false;
        anim.SetTrigger("BulletReady");
        yield return new WaitForSeconds(1f);
        bulletManager.shootBullet();
        yield return null;
    }
    IEnumerator DropThorn()
    {
        while (true)
        {
            dropThorn();
            yield return new WaitForSeconds(2f);
        }
    }
    IEnumerator ReadyBeam()
    {
        anim.SetTrigger("Ready");
        patternBoolean[1] = false;
        while (true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("BlinkEye") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
            {
                anim.SetTrigger("Shoot");

                GameManager.instance.beam.ShootBeam();
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
    IEnumerator Damaged()
    {
        while (true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Damaged") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                Debug.Log("set!");
                anim.ResetTrigger("Damaged");
                anim.SetTrigger("Idle");
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
    IEnumerator move()
    {
        Debug.Log("aa");
        activeBool = false;
        if (validInput_move())
        {
            if (direction)
            {
                rb2d.velocity = new Vector3(moveSpeed, 0, 0);
            }
            if (!direction)
            {
                rb2d.velocity = new Vector3(-moveSpeed, 0, 0);
            }
        }
        yield return new WaitForSeconds(0.6f);
        rb2d.velocity = new Vector3(0, 0, 0);
        activeBool = true;
    }
    public bool validInput_move()
    {
        if (rb2d.velocity.x != 0)
            return false;
        return true;
    }
    public void setHealth(int difficult)
    {
        switch (difficult)
        {
            case 1:
                health = 1000;
                break;
            case 2:
                health = 2000;
                break;
            case 3:
                health = 3000;
                break;
            default:
                health = 500;
                break;
        }
    }
}
