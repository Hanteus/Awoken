using UnityEngine;

public class Enemy : MonoBehaviour {

    public float walkSpeed = 1.0f;      // Walkspeed
    public float wallLeft;       // Define wallLeft
    public float wallRight;      // Define wallRight

    private Vector2 walkAmount;
    private Vector3 rotateAmount;
    private Vector3 originalPos; // Original float value

    private bool attackCheck = false;
    private bool patroling = true;
    private bool facingRight;

    private float attackDelta = 2.0f;
    private float distanceToPlayer;
    private float originDelta = 0.5f;
    private float distanceToOrigin;

    private Transform player;
    
    void Start () {
        this.originalPos = this.transform.position;

        wallLeft = transform.position.x - 3.5f;
        wallRight = transform.position.x + 3.5f;

        Debug.Log ( "Position: " + originalPos.x + " WallLeft: " + wallLeft + " WallRight: " + wallRight );

        player = GameObject.FindGameObjectWithTag ( "Player" ).transform;

        if ( this.transform.rotation.eulerAngles.y < 180 ) {
            facingRight = false;
        }
        else if ( this.transform.rotation.eulerAngles.y >= 180 ) {
            facingRight = true;
        }

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
            
            if ( player.position.x < this.transform.position.x ) {

                if ( this.transform.rotation.eulerAngles.y >= 180 ) {
                    Debug.Log ( "In range, rotate to left" );
                    rotateAmount.y -= 180.0f;
                    this.transform.Rotate ( rotateAmount );
                }
            }
            else if ( this.transform.position.x < player.position.x ) {

                if ( this.transform.rotation.eulerAngles.y < 180 ) {
                    Debug.Log ( "In range, rotate to right" );
                    rotateAmount.y += 180.0f;
                    this.transform.Rotate ( rotateAmount );
                }
            }

            if ( distanceToPlayer <= attackDelta ) {
                Debug.Log ( "Attack!!!" );
            }
            else {
                walkAmount.x = -walkSpeed * Time.deltaTime;

                this.transform.Translate ( walkAmount );
            }
        }       
    }

}
