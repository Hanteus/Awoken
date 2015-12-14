using UnityEngine;
using System.Collections;

public class PortalScript : MonoBehaviour {

    public float distanceToPlayer;
    public Transform teleportDestination;

    private float fadeTime;
    private Transform player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag ( "Player" ).transform;

        fadeTime = GameObject.FindGameObjectWithTag ( "GameController" ).GetComponent<FadingScene> ().beginFade ( 1 );

        if ( teleportDestination == null ) {
            Debug.LogError ( "Insert a destination portal!!!" );
            return;
        }
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log ( "Portal " + teleportDestination.position );

        distanceToPlayer = Vector2.Distance ( player.position , this.transform.position );

        if ( distanceToPlayer > 2 && Input.GetButtonDown ( "Interact" ) ) {
            Debug.Log ( "E key press" );
        }
        else if ( distanceToPlayer <= 2 && Input.GetButtonDown ( "Interact" ) ) {
            Debug.Log ( "E key press to open portal" );
            //Open another scene
            //Application.LoadLevel ( "TestScene" );
            //Or teleport Carter to another door
            player.transform.position = new Vector3 ( teleportDestination.position.x, teleportDestination.position.y);
        }
	}

    /*IEnumerator ChangeLevel () {        
        yield return new WaitForSeconds ( fadeTime );

        Application.LoadLevel ( "TestScene" );
    }*/
}
