using UnityEngine;
using System.Collections;
using Fungus;

public class PlaceHolder : MonoBehaviour {

    private float distanceToPlayer;
    private string objectName;
    private string blockName;
    private string placeHolderBlockName;
    private float timeToFade = 0f;
    public float deltaToActivate = 3.0f;
    public AudioSource useSound;
    private GameObject currentLevel;
    public GameObject nextLevel;

    private GameObject[] playerList;
    private GameObject player;

    public Flowchart fc;

    public GameObject EKey;
    private bool canChangeLVL = false;

    // Use this for initialization
    void Start() {
        playerList = GameObject.FindGameObjectsWithTag("Player");

        objectName = this.gameObject.name;

        switch (objectName) {
            case "Level 0 - Bed":
                blockName = "FT - Level 1";
                placeHolderBlockName = "PH Level 0";
                currentLevel = this.transform.root.gameObject;
                break;

            case "Level 1 - Door":
                blockName = "FT - Level 2";
                placeHolderBlockName = "PH Level 1";
                currentLevel = this.transform.root.gameObject;
                break;

            case "Level 2 - Door":
                blockName = "FT - Level 3";
                placeHolderBlockName = "PH Level 2";
                currentLevel = this.transform.root.gameObject;
                break;
        }

        foreach (GameObject g in playerList) {
            if (g.name == "Carter (clone)")
                Destroy(g);
            else if (g.name == "Carter")
                player = g;
        }

    }

    // Update is called once per frame
    void Update() {

        distanceToPlayer = Vector2.Distance(player.transform.position, this.transform.position);

        // Debug.Log("Length " + player.Length +  " Distance " + distanceToPlayer + " gameobject " + this.gameObject.name + " Carter " + player[0].transform.position);
        if (distanceToPlayer <= deltaToActivate) {

            EKey.SetActive(true);
            fc.ExecuteBlock(placeHolderBlockName);

            if (Input.GetButtonDown("Interact")) {
                useSound.Play();
                nextLevel.SetActive(true);
                fc.ExecuteBlock(blockName);
                canChangeLVL = true;
            }

        } else if (distanceToPlayer > deltaToActivate) {
            if (EKey.activeInHierarchy)
                EKey.SetActive(false);
        }

        if (nextLevel.activeInHierarchy && canChangeLVL) {
            timeToFade += Time.deltaTime;

            if (timeToFade >= 2) {
                currentLevel.SetActive(false); canChangeLVL = false;
            }
        }

    }

}
