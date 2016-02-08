using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

    public float speed = 20f;
    public float maxSpeed = 5f;
    public float jumpPower = 350f;
    public float defaultGravityScale;
    public bool activateDJ = false;

    public AudioSource jumpSound;
    public AudioSource walkSound;

    private Rigidbody2D rb2d;
    private Animator anim;
    private Boolean allowMovement = true;

    public GameObject eyeGameObject;
    public GameObject mouthGameObject;
    public GameObject armLeftGameObject;
    public GameObject armRightGameObject;
    // public GameObject groundCheck;

    private int health = 6;

    bool facingRight;

    void Start() {
        facingRight = true;
        rb2d = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        defaultGravityScale = transform.gameObject.GetComponent<Rigidbody2D>().gravityScale;
    }

    void Update() {
        if (anim.GetBool("Grounded"))
            deactivateGravity();
        else
            activateGravity();

        anim.SetFloat("Speed", Math.Abs(rb2d.velocity.x));

        if (allowMovement) {
            if (Input.GetAxis("Horizontal") < -0.1f) {
                transform.localScale = new Vector3(-1, 1, 1);
                facingRight = false;
            }

            if (Input.GetAxis("Horizontal") > 0.1f) {
                transform.localScale = new Vector3(1, 1, 1);
                facingRight = true;
            }

            if (Input.GetButtonDown("Jump")) {
                if (anim.GetBool("Grounded")) {
                    jumpSound.Play();
                    rb2d.AddForce(Vector2.up * jumpPower);
                    //activateDJ = true;
                } else if (activateDJ) {
                    activateDJ = false;

                    rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                    rb2d.AddForce(Vector2.up * jumpPower);
                }
            }

            if (!this.transform.rotation.Equals(new Quaternion(0, 0, 0, 0))) {
                this.transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }
    }

    void FixedUpdate() {
        float h = Input.GetAxisRaw("Horizontal");

        /*if (Input.GetAxisRaw("Horizontal") != 0 && allowMovement) {
            walkSound.Play();
        } else
            walkSound.Stop();*/

        /*if (Input.GetAxis("Horizontal") < -0.1f)
            transform.position = transform.position - transform.right * speed * Time.deltaTime;
        else if (Input.GetAxis("Horizontal") > 0.1f)
            transform.position = transform.position + transform.right * speed * Time.deltaTime;*/

        if (allowMovement) {
            rb2d.AddForce(Vector2.right * speed * h);

            if (rb2d.velocity.x > maxSpeed) {

                rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
            }

            if (rb2d.velocity.x < -maxSpeed) {
                rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
            }

            if (anim.GetBool("Grounded") && Input.GetAxis("Horizontal") == 0f) {
                rb2d.velocity = Vector3.zero;
                rb2d.angularVelocity = 0f;
            }
        }
    }

    public Animator getPlayerAnim() {
        return anim;
    }

    public int getHealth() {
        return this.health;
    }

    public void setHealth(int h) {
        this.health = h;
    }

    public Vector3 getPos() {
        return this.transform.position;
    }

    public void setPos(Vector3 pos) {
        this.transform.position = pos;
    }

    public bool getFacingRight() {
        return facingRight;
    }

    public void activateGravity() {
        transform.gameObject.GetComponent<Rigidbody2D>().gravityScale = defaultGravityScale;
    }

    public void deactivateGravity() {
        transform.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    public void activateDMouth() {
        // Deactivate left
        armLeftGameObject.SetActive(false);
        // Activate eye
        mouthGameObject.SetActive(true);
    }

    public void activateDEye() {
        // Deactivate right
        armRightGameObject.SetActive(false);
        // Activate eye
        eyeGameObject.SetActive(true);
    }

    public void setAllowMovementTrue() {
        allowMovement = true;
    }

    public void setAllowMovementFalse() {
        allowMovement = false;
    }

    /*public void resetGroundcheck() {
        Vector3 tempPos = transform.FindChild("Ground Check").position;

        Destroy(transform.FindChild("Ground Check").gameObject);

        GameObject tempCheck = (GameObject) Instantiate(groundCheck, transform.position, Quaternion.identity);

        tempCheck.transform.parent = transform;
        tempCheck.transform.position = tempPos;
    }*/

}