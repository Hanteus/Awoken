using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class Checkpoint : MonoBehaviour {

    private GameController g;
    private Flowchart fc;

    private bool enableCheck = true;
    private bool canSave = false;

    private string objectName;
    private string placeHolderBlockName;

    public GameObject EKey;

    // Use this for initialization
    void Start () {
        fc = GameObject.FindGameObjectWithTag ( "Flowchart" ).GetComponent<Flowchart> ();

        g = GameObject.FindGameObjectWithTag ( "GameController" ).GetComponent<GameController> ();

        objectName = this.gameObject.name;

        switch ( objectName ) {
            case "CheckpointDoor":
                placeHolderBlockName = "PH CheckPointDoor";
                break;

            case "CheckpointEnigma":
                placeHolderBlockName = "PH CheckPointEnigma";
                break;
        }
    }

    void Update () {
        this.gameObject.transform.Rotate ( 0 , 0 , 5 );

        if ( Input.GetButtonDown ( "Interact" ) && canSave) {
            g.Save ();

            Debug.Log ( "Game Saved" );

            fc.ExecuteBlock ( "CheckPoint Block" );

            fc.SetBooleanVariable ( "enableCP" , true );

            EKey.SetActive ( false );

            enableCheck = false;

            canSave = false;
        }
    }

    void OnTriggerEnter2D ( Collider2D c ) {

        if ( c.gameObject.tag.Equals ( "Player" ) && enableCheck ) {

            EKey.SetActive ( true );

            if (fc != null)
                fc.ExecuteBlock ( placeHolderBlockName );
            
            canSave = true;
        }
    }

    void OnTriggerExit2D ( Collider2D c ) {

        if ( c.gameObject.tag.Equals ( "Player" )) {
            
            canSave = false;

            if (EKey.activeInHierarchy)
                EKey.SetActive ( false );           
        }
    }

}
