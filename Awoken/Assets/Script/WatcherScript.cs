using UnityEngine;
using System.Collections;

public class WatcherScript : MonoBehaviour {

    public LineRenderer watcherLR;

    bool active;
    Collider2D detected;
    RaycastHit2D hit;

    // Use this for initialization
    void Start() {
        active = false;
    }

    // Update is called once per frame
    void Update() {
        if (active) {
            hit = Physics2D.Raycast(transform.position, detected.transform.position);

            watcherLR.SetPosition(0, transform.position);
            watcherLR.SetPosition(1, hit.point);

            Debug.DrawLine(transform.position, hit.point, Color.red);
            Debug.Log(hit.point);

            if (hit.transform.tag == "Player") {
                Debug.Log("Die!");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D c) {
        if (c.tag.Equals("Player")) {
            Debug.Log("I see you...");
            detected = c;
            active = true;
        }
    }

    void OnTriggerExit2D() {
        active = false;
        Debug.Log("I lost you...");
    }

}