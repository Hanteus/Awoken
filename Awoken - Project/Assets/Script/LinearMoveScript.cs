using UnityEngine;
using System.Collections;

public class LinearMoveScript : MonoBehaviour {

    public bool vertical;
    public bool horizontal;
    public float lenght;
    public float speed;

    Vector3 limit1;
    Vector3 limit2;
    bool backHorizontal;
    bool backVertical;

    // Use this for initialization
    void Start() {
        limit1.x = transform.position.x + lenght;
        limit1.y = transform.position.y + lenght;
        limit2.x = transform.position.x - lenght;
        limit2.y = transform.position.y - lenght;
    }

    // Update is called once per frame
    void Update() {
        if (horizontal) {
            if (backHorizontal == false)
                transform.position = transform.position + transform.right * speed * Time.deltaTime;
            else
                transform.position = transform.position - transform.right * speed * Time.deltaTime;
            if (transform.position.x > limit1.x && backHorizontal == false)
                backHorizontal = true;
            else if (transform.position.x < limit2.x && backHorizontal == true)
                backHorizontal = false;
        }

        if (vertical) {
            if (backVertical == false)
                transform.position = transform.position + transform.up * speed * Time.deltaTime;
            else
                transform.position = transform.position - transform.up * speed * Time.deltaTime;
            if (transform.position.y > limit1.y && backVertical == false)
                backVertical = true;
            else if (transform.position.y < limit2.y && backVertical == true)
                backVertical = false;
        }
    }

}