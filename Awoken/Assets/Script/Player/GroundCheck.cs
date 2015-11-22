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
    }

    void OnTriggerStay2D ( Collider2D c ) {
        if ( c.gameObject.tag == "Ground" ) {
            player.getPlayerAnim ().SetBool ( "Grounded" , true );
        }

        /*
            else if (c.gameObject.tag == "Platform") {
                player.getPlayerAnim().SetBool("Grounded", true);
                transform.parent.parent = c.transform;
            }
        */
    }

    void OnTriggerExit2D(Collider2D c) {
        if ( c.gameObject.tag == "Ground" ) {
            player.getPlayerAnim ().SetBool ( "Grounded" , false );
        }

        /*
            else if (c.gameObject.tag == "Platform") {
                transform.parent.parent = null;
        }*/
    }

}
