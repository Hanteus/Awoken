using UnityEngine;
using System.Collections;
using Fungus;

public class LoadDialogue : MonoBehaviour {

    private float distanceToPlayer;
    public float deltaToActivate = 3.0f;
    
    private Transform player;
    public Flowchart fc;

    public GameObject EKey;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag ( "Player" ).transform;
    }

    // Update is called once per frame
    void Update () {

        distanceToPlayer = Vector2.Distance ( player.position , this.transform.position );

        if ( distanceToPlayer <= deltaToActivate ) {

            EKey.SetActive ( true );
            fc.ExecuteBlock ( "PH Richard" );

            if ( Input.GetButtonDown ( "Interact" ) ) {
                player.GetComponent<Player>().setAllowMovementFalse();
                fc.ExecuteBlock ( "RichardDialogue" );
                fc.FindBlock ( "PH Richard" ).Stop ();
            }

        }
        else if ( distanceToPlayer > deltaToActivate ) {
            if ( EKey.activeInHierarchy )
                EKey.SetActive ( false );

            fc.FindBlock ( "PH Richard" ).Stop ();
        }
    }
}
