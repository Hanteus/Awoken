using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

    private Player player;

    void Start() {
        player = gameObject.GetComponentInParent<Player>();
    }

    void OnTriggerEnter2D(Collider2D c) {
        if ( c.gameObject.tag == "Ground" ) {
            player.getPlayerAnim ().SetBool ( "Grounded" , true );
        }
        if (c.gameObject.tag == "Mobile Platform")
        {
            Debug.Log("IN");
            player.getPlayerAnim().SetBool("Grounded", true);
            transform.parent.parent = c.transform.parent;
        }
    }

    void OnTriggerStay2D ( Collider2D c ) {
        if ( c.gameObject.tag == "Ground" ) {
            player.getPlayerAnim ().SetBool ( "Grounded" , true );
        } 
    }

    void OnTriggerExit2D(Collider2D c) {
        if ( c.gameObject.tag == "Ground" ) {
            player.getPlayerAnim ().SetBool ( "Grounded" , false );
        }
        if (c.gameObject.tag == "Mobile Platform") {
            Debug.Log("OUT");
            transform.parent.parent = null;
        }
    }

}
