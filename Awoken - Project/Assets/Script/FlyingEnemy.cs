using UnityEngine;
using System.Collections;

public class FlyingEnemy : MonoBehaviour {
    private LifeScript lfs;
    private Transform player;
    private Animator anim;
    public Transform origin;

    private float attackTime = 0.0f;
    private float distanceToPlayer;
    private float attackDelta = 3.0f;
    private float attackRange = 20.0f;
    public int damage = 1;
    private float distanceToOrigin;
    private float originDelta = 2.0f;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag ( "Player" ).transform;
        anim = this.gameObject.GetComponent<Animator>();
        lfs = player.GetComponent<LifeScript> ();

        if ( origin == null ) {
            origin = this.transform;
        }
        
        this.gameObject.GetComponent<FlyingMonsterAItoPlayer> ().enabled = false;
        this.gameObject.GetComponent<FlyingMonsterAItoOrigin> ().enabled = false;

    }

    // Update is called once per frame
    void Update () {
        attackTime += Time.deltaTime;

        //Check for the player position in range
        distanceToPlayer = Vector2.Distance ( this.transform.position , player.position );

        if ( distanceToPlayer <= attackRange ) {

            this.gameObject.GetComponent<FlyingMonsterAItoPlayer> ().enabled = true;
            this.gameObject.GetComponent<FlyingMonsterAItoOrigin> ().enabled = false;

            if ( distanceToPlayer <= attackDelta ) {
                anim.SetBool ( "Attack" , true );

                if ( attackTime >= 1.0f ) {
                    lfs.damagePlayer ( damage );
                    attackTime = 0.0f;
                }

            }
            else {
                anim.SetBool ( "Attack" , false );
            }
        }
        else {
            this.gameObject.GetComponent<FlyingMonsterAItoPlayer> ().enabled = false;
            this.gameObject.GetComponent<FlyingMonsterAItoOrigin> ().enabled = true;

            distanceToOrigin = Vector2.Distance ( this.transform.position , origin.position );
            
            if ( distanceToOrigin <= originDelta) {
                this.gameObject.GetComponent<FlyingMonsterAItoOrigin> ().enabled = false;
            }
        }
            
    }

    public Transform getOrigin () {
        return origin;
    }
}
