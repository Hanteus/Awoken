using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float walkSpeed = 1.0f;      // Walkspeed
    public float wallLeft;       // Define wallLeft
    public float wallRight;      // Define wallRight
    public int damage = 1;

    private Vector2 walkAmount;
    private Vector3 rotateAmount;
    private Vector3 originalPos;
    
    private bool patroling = true;

    private float attackDelta = 3.0f;
    private float distanceToPlayer;
    private float originDelta = 0.5f;
    private float distanceToOrigin;
    private LifeScript lfs;

    private GameObject player;

    private Animator anim;
    
    void Start () {
        this.originalPos = this.transform.position;

        wallLeft = transform.position.x - 3.5f;
        wallRight = transform.position.x + 3.5f;

        Debug.Log ( "Position: " + originalPos.x + " WallLeft: " + wallLeft + " WallRight: " + wallRight );

        player = GameObject.FindGameObjectWithTag ( "Player" );

        anim = this.gameObject.GetComponent<Animator> ();

        lfs = player.GetComponent<LifeScript> ();

    }
    
    void Update () {
        //Evaluate distance to originalPos
        distanceToOrigin = Vector2.Distance ( this.transform.position , originalPos );

        //Check for the player position in range
        distanceToPlayer = Vector2.Distance ( this.transform.position , player.transform.position );
        

        if ( distanceToPlayer > 10) {
            
            if ( patroling ) {
                //patroling
                Debug.Log ( "Out of range" );

                //Move the enemy back and forth
                walkAmount.x = -walkSpeed * Time.deltaTime;

                if ( this.transform.position.x >= wallRight ) {
                    Debug.Log ( "Out of range, rotate to left" );
                    rotateAmount.y -= 180.0f;
                    this.transform.Rotate ( rotateAmount );
                }
                else if ( this.transform.position.x <= wallLeft ) {
                    Debug.Log ( "Out of range, rotate to right" );
                    rotateAmount.y += 180.0f;
                    this.transform.Rotate ( rotateAmount );
                }

                this.transform.Translate ( walkAmount );
            }
            else {
                //the player is escaped
                //return to its position
                anim.SetBool ( "Attack" , false );
                Debug.Log ( "Carter escaped! Return to originalPos." );

                if ( originalPos.x < this.transform.position.x ) {

                    if ( this.transform.rotation.eulerAngles.y >= 180 ) {
                        rotateAmount.y -= 180.0f;
                        this.transform.Rotate ( rotateAmount );
                    }
                }
                else if ( this.transform.position.x < originalPos.x ) {

                    if ( this.transform.rotation.eulerAngles.y < 180 ) {
                        rotateAmount.y += 180.0f;
                        this.transform.Rotate ( rotateAmount );
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

            Debug.Log ( "In range" );
            
            if ( player.transform.position.x < this.transform.position.x ) {

                if ( this.transform.rotation.eulerAngles.y >= 180 ) {
                    Debug.Log ( "In range, rotate to left" );
                    rotateAmount.y -= 180.0f;
                    this.transform.Rotate ( rotateAmount );
                }
            }
            else if ( this.transform.position.x < player.transform.position.x ) {

                if ( this.transform.rotation.eulerAngles.y < 180 ) {
                    Debug.Log ( "In range, rotate to right" );
                    rotateAmount.y += 180.0f;
                    this.transform.Rotate ( rotateAmount );
                }
            }

            if ( distanceToPlayer <= attackDelta ) {
                Debug.Log ( "Attack!!!" );
                anim.SetBool ( "Attack" , true );
                EnemyAttackDamage ();
            }
            else {
                walkAmount.x = -walkSpeed * Time.deltaTime;

                this.transform.Translate ( walkAmount );
            }
        }       
    }

    IEnumerator EnemyAttackDamage () {
            lfs.damagePlayer ( damage );
            yield return new WaitForSeconds ( 1 );
    }
}
