using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float walkSpeed = 1.0f;      // Walkspeed
    public float wallLeft;       // Define wallLeft
    public float wallRight;      // Define wallRight
    public int damage = 1;
    public float patrolRange = 3.5f;

    private Vector2 walkAmount;
    private Vector3 rotateAmount;
    private Vector3 originalPos;
    
    private bool patroling = true;
    private bool rotateLeft = true;
    private bool rotateRight = false;
    private float attackDelta = 2.0f;
    private float distanceToPlayer;
    private float originDelta = 0.5f;
    private float distanceToOrigin;
    private float enemyRotation = 0.0f;
    private float enemyXPosition = 0.0f;
    private float rotateValue = 180;
    private float attackTime = 0.0f;

    private LifeScript lfs;
    private GameObject player;
    private Animator anim;
    
    void Start () {
        this.originalPos = this.transform.position;

        wallLeft = transform.position.x - patrolRange;
        wallRight = transform.position.x + patrolRange;

        player = GameObject.FindGameObjectWithTag ( "Player" );

        anim = this.gameObject.GetComponent<Animator> ();

        lfs = player.GetComponent<LifeScript> ();

        enemyXPosition = this.transform.position.x;
        enemyRotation = this.transform.rotation.eulerAngles.y;

        if ( enemyRotation >= 180.0f ) {
            rotateLeft = true;
            rotateRight = false;
        }
        else {
            rotateLeft = false;
            rotateRight = true;
        }
    }
    
    void Update () {
        enemyXPosition = this.transform.position.x;
        enemyRotation = this.transform.rotation.eulerAngles.y;

        attackTime += Time.deltaTime;
        
        //Evaluate distance to originalPos
        distanceToOrigin = Vector2.Distance ( this.transform.position , originalPos );

        //Check for the player position in range
        distanceToPlayer = Vector2.Distance ( this.transform.position , player.transform.position );

        if ( distanceToPlayer > 10) {
            
            if ( patroling ) {
                //patroling
                // Debug.Log ( "Out of range" );

                //Move the enemy back and forth
                walkAmount.x = -walkSpeed * Time.deltaTime;

                if ( enemyXPosition >= wallRight && rotateLeft) {
                    // Debug.Log ( "Out of range, rotate to left" );
                    rotateLeft = false;
                    rotateRight = true;
                    //rotateAmount.y -= rotateValue;
                    //this.transform.Rotate ( rotateAmount );
                    this.transform.RotateAround ( this.transform.position , transform.up , rotateValue );
                }
                else if ( enemyXPosition <= wallLeft && rotateRight) {
                    // Debug.Log ( "Out of range, rotate to right" );
                    rotateLeft = true;
                    rotateRight = false;
                    //rotateAmount.y += rotateValue;
                    //this.transform.Rotate ( rotateAmount );
                    this.transform.RotateAround ( this.transform.position , transform.up , rotateValue );
                }

                this.transform.Translate ( walkAmount );
            }
            else {
                //the player is escaped
                //return to its position
                // Debug.Log ( "Carter escaped! Return to originalPos." );

                if ( originalPos.x < enemyXPosition ) {

                    if ( enemyRotation >= rotateValue && rotateLeft) {
                        rotateLeft = false;
                        rotateRight = true;
                        //rotateAmount.y -= rotateValue;
                        //this.transform.Rotate ( rotateAmount );
                        this.transform.RotateAround ( this.transform.position , transform.up , rotateValue );
                    }
                }
                else if ( enemyXPosition < originalPos.x ) {

                    if ( enemyRotation < rotateValue && rotateRight) {
                        rotateLeft = true;
                        rotateRight = false;
                        //rotateAmount.y += rotateValue;
                        //this.transform.Rotate ( rotateAmount );
                        this.transform.RotateAround ( this.transform.position , transform.up , rotateValue );
                    }

                }

                walkAmount.x = -walkSpeed * Time.deltaTime;

                this.transform.Translate ( walkAmount );

                if ( distanceToOrigin < originDelta ) {
                    //restart to patrol
                    patroling = true;
                }
            }

        }
        else {
            //reach the player and attack
            patroling = false;

            // Debug.Log ( "In range" );

            if ( player.transform.position.x < enemyXPosition ) {

                if ( enemyRotation >= rotateValue && rotateLeft ) {
                    rotateLeft = false;
                    rotateRight = true;
                    // Debug.Log ( "In range, rotate to left" );
                    //rotateAmount.y -= rotateValue;
                    //this.transform.Rotate ( rotateAmount );
                    this.transform.RotateAround ( this.transform.position,transform.up, rotateValue );
                }
            }
            else if ( enemyXPosition < player.transform.position.x ) {

                if ( enemyRotation < rotateValue && rotateRight ) {
                    rotateLeft = true;
                    rotateRight = false;
                    // Debug.Log ( "In range, rotate to right" );
                    //rotateAmount.y += rotateValue;
                    //this.transform.Rotate ( rotateAmount );
                    this.transform.RotateAround ( this.transform.position , transform.up , rotateValue );
                }
            }

            if ( distanceToPlayer <= attackDelta ) {
                // Debug.Log ( "Attack!!!" );
                anim.SetBool ( "Attack" , true );

                if ( attackTime >= 1.0f ) {
                    lfs.damagePlayer ( damage );
                    attackTime = 0.0f;
                }
                
            }
            else {
                anim.SetBool ( "Attack" , false );
                walkAmount.x = -walkSpeed * Time.deltaTime;

                this.transform.Translate ( walkAmount );
            }
        }       
    }

}
