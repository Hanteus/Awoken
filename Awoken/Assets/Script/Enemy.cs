using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float walkSpeed = 1.0f;      // Walkspeed
    public float wallLeft;       // Define wallLeft
    public float wallRight;      // Define wallRight
    float walkingDirection;
    Vector2 walkAmount;
    Vector3 rotate;
    float originalX; // Original float value

    void Start () {
        this.originalX = this.transform.position.x;

        wallLeft = transform.position.x - 2.5f;
        wallRight = transform.position.x + 2.5f;

        Debug.Log ( "Position: " + originalX + " WallLeft: " + wallLeft + " WallRight: " + wallRight );

        /*if (this.transform.rotation.eulerAngles.y < 180 ) {
            //moves left
            walkingDirection = -1.0f;
        }
        else{
            //moves right
            walkingDirection = 1.0f;
        }*/
            
    }

    // Update is called once per frame
    void Update () {
        walkAmount.x = - walkSpeed * Time.deltaTime;

        Debug.Log ( "WA " + walkAmount.x );

        if (this.transform.position.x >= wallRight ) {
            //walkingDirection = -1.0f;
            Debug.Log ( "1" );
            Flip ( -1.0f );
        }
        else if (this.transform.position.x <= wallLeft ) {
            Flip ( 1.0f );
            Debug.Log ( "2" );
            //walkingDirection = 1.0f;
        }

        //Debug.Log ( "WD " + walkingDirection );

        this.transform.Translate ( walkAmount );
    }

    void Flip ( float dir ) {
        rotate.y += dir * 180f;

        this.transform.Rotate ( rotate );
    }

}
