using UnityEngine;
using System.Collections;

public class MovingCamera : MonoBehaviour {
    public float speed = 10.0f;
    private int index = 0;
    private Vector3 tempPos;

    private Vector3 originalPos;
    public GameObject[] levels;
    public GameObject canvas;

    // Use this for initialization
    void Start() {
        originalPos = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {

            if (this.transform.parent != null)
                this.transform.parent = null;

            if (index < 3) {
                foreach (GameObject lvl in levels) {
                    if (lvl.activeInHierarchy) {
                        index++;
                    }
                }
                tempPos = levels[index].transform.position;

                levels[index].transform.root.gameObject.SetActive(true);

                levels[index - 1].transform.root.gameObject.SetActive(false);

                this.transform.position = new Vector3(tempPos.x, tempPos.y, -5);
            }

        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (Input.GetKey(KeyCode.L)) {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.J)) {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.K)) {
            transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.I)) {
            transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.F1)) {
            speed++;
        }
        if (Input.GetKey(KeyCode.F2)) {
            speed--;
        }
        if (Input.GetKey(KeyCode.F3)) {
            if (canvas.activeInHierarchy == true)
                canvas.SetActive(false);
            else
                canvas.SetActive(true);
        }
    }
}
