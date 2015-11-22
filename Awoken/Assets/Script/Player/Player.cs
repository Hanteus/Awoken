﻿using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

    public float speed = 20f;
    public float maxSpeed = 5f;
    public float jumpPower = 350f;
    public bool activateDJ = false;
    private Rigidbody2D rb2d;
    private Animator anim;

    void Start() {
        rb2d = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
    }
    
    void Update(){
        
        anim.SetFloat("Speed", Math.Abs(rb2d.velocity.x));

        if ( Input.GetAxis ( "Horizontal" ) < -0.1f ) {
            transform.localScale = new Vector3 ( -1 , 1 , 1 );
        }

        if ( Input.GetAxis ( "Horizontal" ) > 0.1f ) {
            transform.localScale = new Vector3 ( 1 , 1 , 1 );
        }

        if ( Input.GetButtonDown ( "Jump" )){

            if ( anim.GetBool ( "Grounded" ) ) {
                rb2d.AddForce ( Vector2.up * jumpPower );

                //activateDJ = true;
            }
            else {
                if ( activateDJ ) {
                    activateDJ = false;

                    rb2d.velocity = new Vector2 ( rb2d.velocity.x , 0 );
                    rb2d.AddForce ( Vector2.up * jumpPower );
                }
            }

        }

        if ( !this.transform.rotation.Equals ( new Quaternion ( 0 , 0 , 0 , 0 ) ) ) {
            this.transform.rotation = new Quaternion ( 0 , 0 , 0 , 0 );
        }
    }

    void FixedUpdate(){

        float h = Input.GetAxisRaw ( "Horizontal");

        rb2d.AddForce(Vector2.right * speed * h);

       if(rb2d.velocity.x > maxSpeed){

            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }

        if(rb2d.velocity.x < -maxSpeed){
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }
    }

    public Animator getPlayerAnim () {
        return anim;
    }

}