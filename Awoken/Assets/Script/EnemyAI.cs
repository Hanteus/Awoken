using UnityEngine;
using Pathfinding;
using System.Collections;

[RequireComponent ( typeof ( Rigidbody2D ) )] 
[RequireComponent ( typeof ( Seeker ) )]
public class EnemyAI : MonoBehaviour {

    public Transform target;

    //How many times each second we will update our path
    public float updateRate = 2f;
    
    private Seeker seeker;
    private Rigidbody2D rb2d;

    //The calculate path
    public Path path;

    //The AI's speed per second
    public float speed = 300f;
    public ForceMode2D fmode;

    [HideInInspector]
    public bool pathIsEnded = false;

    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWayPointDistance = 3;

    // The waypoint we are currently moving towards
    private int currentWayPoint = 0;

    private bool searchingForPlayer = false;

    public float wallLeft;       // Define wallLeft
    public float wallRight;      // Define wallRight

    private Vector2 walkAmount;
    private Vector3 originalPos; // Original float value
    private bool patrolling = true;
    private float distance;
    
    void Start () {
        seeker = GetComponent<Seeker> ();
        rb2d = GetComponent<Rigidbody2D> ();

        if ( target == null ) {
            target = GameObject.FindGameObjectWithTag ( "Player" ).transform;
        }
  	}

    IEnumerator SearchForPlayer () {
        GameObject sResult = GameObject.FindGameObjectWithTag ( "Player" );

        if ( sResult == null ) {
            yield return new WaitForSeconds ( 0.5f );

            StartCoroutine ( SearchForPlayer () );
        }
        else {
            target = sResult.transform;
            searchingForPlayer = false;

            StartCoroutine ( UpdatePath () );

            yield break;
        }
    }

    IEnumerator UpdatePath () {
        if ( target == null ) {

            if ( !searchingForPlayer ) {
                searchingForPlayer = true;

                StartCoroutine ( SearchForPlayer () );
            }
            yield break;
        }

        //Start a new Path to the target position, return the result to the OnPathComplete method
        seeker.StartPath ( this.transform.position , target.position , OnPathComplete );

        yield return new WaitForSeconds ( 1f / updateRate );

        StartCoroutine ( UpdatePath() );
    }

    public void OnPathComplete ( Path p ) {
        Debug.Log ("We got a path. Did it have an error?" + p.error);

        if ( !p.error ) {
            path = p;
            currentWayPoint = 0;
        }
    }

    void Update () {

    }

    void FixedUpdate () {
        if ( target == null ) {

            if ( !searchingForPlayer ) {
                searchingForPlayer = true;

                StartCoroutine ( SearchForPlayer () );
            }
            return;
        }

        distance = Vector2.Distance ( this.transform.position , target.position );
        Debug.Log ( "Distance:" + distance );

        if ( distance > 10 ) {
            Debug.Log ( "Patroling" );
            walkAmount.x = -speed * Time.deltaTime;

            /*if ( this.transform.position.x >= wallRight ) {
                //rotate to left
                Flip ( -1.0f );
            }
            else if ( this.transform.position.x <= wallLeft ) {
                //rotate to right
                Flip ( 1.0f );
            }*/

            this.transform.Translate ( walkAmount );
        }
        else {

            if ( path == null ) {
                //Start a new Path to the target position, return the result to the OnPathComplete method
                seeker.StartPath ( this.transform.position , target.position , OnPathComplete );

                StartCoroutine ( UpdatePath () );
            }            

            Debug.Log ( "Target in range" );

            Flip ();

            if ( path == null )
                return;

            if ( currentWayPoint >= path.vectorPath.Count ) {
                if ( pathIsEnded )
                    return;

                Debug.Log ( "End of path reached." );

                pathIsEnded = true;

                return;
            }

            pathIsEnded = false;

            //Direction to the next waypoint

            Vector3 dir = ( path.vectorPath [ currentWayPoint ] - this.transform.position ).normalized;
            dir *= speed * Time.fixedDeltaTime;

            //Move the AI
            rb2d.AddForce ( dir , fmode );

            float dist = Vector3.Distance ( this.transform.position , path.vectorPath [ currentWayPoint ] );
            if ( dist < nextWayPointDistance ) {
                currentWayPoint++;

                return;
            }
        }

        
    }

    void Flip () {
        Vector3 rotate = Vector3.zero;

        if ( target.position.x < this.transform.position.x ) {

            if ( this.transform.rotation.eulerAngles.y >= 180 ) {
                rotate.y -= 180.0f;
                this.transform.Rotate ( rotate );
            }
        }
        else if ( this.transform.position.x < target.position.x ) {

            if ( this.transform.rotation.eulerAngles.y < 180 ) {
                rotate.y += 180.0f;
                this.transform.Rotate ( rotate );
            }
        }
    }

    void Flip ( float dir ) {
        Vector3 rotate = Vector3.zero;

        rotate.y += dir * 180f;

        this.transform.Rotate ( rotate );
    }
}
