using UnityEngine;
using System.Collections;

public class WatcherScript : MonoBehaviour {

    public LineRenderer watcherLR;

    private Animator anim;
    private bool active;
    private Collider2D detected;
    private RaycastHit2D hit;


    public LineRenderer eyeRayLR;
    public string layerMaskString;

    int layerMask;
    bool targetLocked;

    Vector2 rayStart;
    Vector2 rayDirection;
    Vector2 rayEnd;
    Vector3 rayStartVisual;
    Vector3 rotationVector;
    Vector3 visualEnd;
    
    GameObject target;
    GameObject rayStartObject;
    Transform player;

    // Use this for initialization
    void Start() {
        anim = this.gameObject.GetComponent<Animator> ();
        anim.enabled = false;
        active = false;

        visualEnd.z = 4.5f;
        layerMask = LayerMask.NameToLayer ( layerMaskString );

        rayStartObject = this.transform.FindChild ( "RayStart" ).gameObject;
        eyeRayLR = this.transform.FindChild ( "WLineRenderer" ).GetComponent<LineRenderer>();
        
        player = this.transform.parent;
    }

    // Update is called once per frame
    void Update() {
        if ( active ) {
            Debug.Log ( "Try to reach" );
            /*hit = Physics2D.Raycast(transform.position, detected.transform.position);

            watcherLR.SetPosition(0, transform.position);
            watcherLR.SetPosition(1, hit.point);

            Debug.DrawLine(transform.position, hit.point, Color.red);
            

            if (hit.transform.tag == "Player") {
                Debug.Log("Die!");
            }*/
            updateRay ();
            showRay ();
        }
        else {
            hideRay ();
        }
    }

    void OnTriggerEnter2D(Collider2D c) {
        if (c.tag.Equals("Player")) {
            Debug.Log("I see you...");
            detected = c;

            active = true;
            anim.enabled = true;

            this.anim.SetBool ( "InRange" , false );
        }
    }

    void OnTriggerExit2D() {
        Debug.Log ( "I lost you..." );

        this.anim.SetBool ( "InRange" , true );
        active = false;
    }

    void updateRay() {
        rayStartVisual = rayStartObject.transform.position;
        rayStartVisual.z = 4.5f;
        rayStart = rayStartVisual;

        rayEnd = new Vector2(detected.gameObject.transform.position.x, detected.gameObject.transform.position.y);
        Debug.Log ( "RayEnd" + rayEnd.ToString() );

        rayDirection = rayEnd - rayStart;

        hit = Physics2D.Raycast(rayStart, rayDirection, Mathf.Infinity, (1 << layerMask));
        

        visualEnd.x = hit.point.x;
        visualEnd.y = hit.point.y;

        eyeRayLR.SetPosition(0, rayStartVisual);
        
        if ( hit == true ) 
            eyeRayLR.SetPosition ( 1 , visualEnd );
    }

    /*public bool reachable(GameObject g) {
        Vector3 gPosition = g.transform.position;
        Vector3 tempRayEnd = g.transform.position;

        rayDirection = tempRayEnd - player.position;

        hit = Physics2D.Raycast(player.position, rayDirection, Mathf.Infinity, (1 << layerMask));

        if (hit.transform.GetHashCode() == g.transform.GetHashCode())
            return true;

        return false;
    }*/

    void updateRayLocked() {
        eyeRayLR.SetPosition(0, rayStartObject.transform.position);
        eyeRayLR.SetPosition(1, target.transform.position);
    }

    void showRay() {
        eyeRayLR.enabled = true;
    }

    void hideRay() {
        eyeRayLR.enabled = false;
        unlockTarget();
    }

    public void lockOnTarget(GameObject target) {
        targetLocked = true;
        this.target = target;
    }

    public void unlockTarget() {
        targetLocked = false;
    }

}