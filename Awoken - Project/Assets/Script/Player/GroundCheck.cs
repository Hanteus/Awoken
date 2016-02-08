using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

    private Player player;
    private Vector2 pointA;
    private Vector2 pointB;

    public int inNumber = 0;
    public int i = 0;
    public GameObject a;
    public GameObject b;

    void Start() {
        player = gameObject.GetComponentInParent<Player>();
        pointA = a.transform.position;
        pointB = b.transform.position;
    }

    void OnTriggerEnter2D(Collider2D c) {
        if (c.gameObject.tag == "Ground") {
            inNumber++;
            player.getPlayerAnim().SetBool("Grounded", true);
        }
        if (c.gameObject.tag == "Mobile Platform") {
            transform.parent.parent = c.transform.parent;
        }
    }

    void OnTriggerStay2D(Collider2D c) {
        if (c.gameObject.tag == "Ground") {
            player.getPlayerAnim().SetBool("Grounded", true);
        }
    }

    void OnTriggerExit2D(Collider2D c) {
        if (c.gameObject.tag == "Ground") {
            inNumber--;
        }
        if (c.gameObject.tag == "Mobile Platform") {
            player.getPlayerAnim().SetBool("Grounded", false);
            transform.parent.parent = null;
        }

        if (inNumber == 0)
            player.getPlayerAnim().SetBool("Grounded", false);
    }

    void FixedUpdate() {
        i = 0;

        Collider2D[] cArray = Physics2D.OverlapAreaAll(pointA, pointB);

        foreach (Collider2D c in cArray) {
            if (c.transform.gameObject.tag == "Ground")
                i++;
        }

        if (i == 0) {
            player.getPlayerAnim().SetBool("Grounded", false);
        }

    }

    public void resetCounter() {
        inNumber = 0;
    }

}
