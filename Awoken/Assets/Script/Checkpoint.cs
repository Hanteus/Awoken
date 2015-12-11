using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
    
    private GameController g;

    private bool enableCheck = true;

    // Use this for initialization
    void Start () {
        //player = GameObject.FindGameObjectWithTag ( "Player" ).GetComponent<Player>();

        g = GameObject.FindGameObjectWithTag ( "GameController" ).GetComponent<GameController> ();
    }

    void OnTriggerEnter2D ( Collider2D c ) {

        if ( c.gameObject.tag.Equals ( "Player" ) && enableCheck ) {
            Debug.Log ( "Trigger perfect!" );
            g.Save ();
            Debug.Log ( "Game Saved" );

            enableCheck = false;
        }
    }
}
