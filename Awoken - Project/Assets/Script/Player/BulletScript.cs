using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    public string layerMaskGroundString;
    public string layerMaskEnemyString;
    public float speed = 6f;
    public float liveTime = 3f;
    public int damage;

    int layerMaskGround;
    // int layerMaskEnemy;
    float startTime;
    float direction = 1;

    // Use this for initialization
    void Start() {
        layerMaskGround = LayerMask.NameToLayer(layerMaskGroundString);
        // layerMaskEnemy = LayerMask.NameToLayer(layerMaskGroundString);

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update() {
        transform.position = transform.position + direction * transform.up * speed * Time.deltaTime;
        if (Time.time - startTime > liveTime) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == layerMaskGround) {
            try {
                other.transform.parent.GetComponent<BasicEnemyLifeScript>().damage(damage);
            } catch { };
            Destroy(gameObject);
        } else if (other.gameObject.tag == "Enemy") {
            other.GetComponent<BasicEnemyLifeScript>().damage(damage);
            Destroy(gameObject);
        }
    }

    public void setDirection(float direction) {
        this.direction = direction;
    }

}