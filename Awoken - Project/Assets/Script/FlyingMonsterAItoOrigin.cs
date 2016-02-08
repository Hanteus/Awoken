using UnityEngine;
using System.Collections;
using Pathfinding;
using System;

[RequireComponent ( typeof ( Rigidbody2D ) )]
[RequireComponent ( typeof ( Seeker ) )]

public class FlyingMonsterAItoOrigin : MonoBehaviour {

    public Transform target;

    //How many times each second we will update our path
    public float updateRate = 2.0f;

    //Caching
    private Seeker seeker;
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;

    //The calculate path
    public Path path;

    //The AI speed per second
    public float speed = 300.0f;
    public ForceMode2D fMode;

    [HideInInspector]
    public bool pathIsEnded = false;

    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWayPointDistance = 3.0f;

    //The waypoint we are currently moving towards
    private int currentWayPoint = 0;

    private bool searchingForOrigin = false;

    void Start () {
        seeker = GetComponent<Seeker> ();
        rb2d = GetComponent<Rigidbody2D> ();
        sr = this.GetComponent<SpriteRenderer> ();

        if ( target == null ) {

            if ( !searchingForOrigin ) {
                searchingForOrigin = true;
                StartCoroutine ( SearchForOrigin () );
            }

            return;
        }

        //Start a new path to the target position, return the result to the OnPathComplete method
        seeker.StartPath ( transform.position , target.position , OnPathComplete );

        StartCoroutine ( UpdatePath () );
    }

    void FixedUpdate () {
        if ( target == null ) {

            if ( !searchingForOrigin ) {
                searchingForOrigin = true;
                StartCoroutine ( SearchForOrigin () );
            }

            return;
        }

        if ( target.position.x < this.transform.position.x ) {
            sr.flipX = false;
        }
        else if ( target.position.x > this.transform.position.x ) {
            sr.flipX = true;
        }

        if ( path == null ) {
            return;
        }

        if ( currentWayPoint >= path.vectorPath.Count ) {
            if ( pathIsEnded )
                return;

            pathIsEnded = true;
            return;
        }

        pathIsEnded = false;

        //Direction to the next waypoint
        Vector3 dir = ( path.vectorPath [ currentWayPoint ] - transform.position ).normalized;
        dir *= speed * Time.fixedDeltaTime;

        //Move the AI
        rb2d.AddForce ( dir , fMode );

        float distance = Vector3.Distance ( transform.position , path.vectorPath [ currentWayPoint ] );

        if ( distance < nextWayPointDistance ) {
            currentWayPoint++;
            return;
        }
    }

    IEnumerator UpdatePath () {
        if ( target == null ) {

            if ( !searchingForOrigin ) {
                searchingForOrigin = true;
                StartCoroutine ( SearchForOrigin () );
            }

            yield break;
        }

        //Start a new path to the target position, return the result to the OnPathComplete method
        seeker.StartPath ( transform.position , target.position , OnPathComplete );

        yield return new WaitForSeconds ( 1 / updateRate );

        StartCoroutine ( UpdatePath () );
    }

    IEnumerator SearchForOrigin () {
        Transform sResult = this.gameObject.GetComponent<FlyingEnemy> ().getOrigin ();

        if ( sResult == null ) {
            yield return new WaitForSeconds ( 0.5f );
            StartCoroutine ( SearchForOrigin () );
        }
        else {
            searchingForOrigin = false;
            target = sResult.transform;
            StartCoroutine ( UpdatePath () );
            yield break;
        }

    }

    public void OnPathComplete ( Path p ) {
        if ( !p.error ) {
            path = p;
            currentWayPoint = 0;
        }
    }
}