using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Fungus;

public class LifeScript : MonoBehaviour {

    private int currentLife;
    private int maxLife = 6;
    public Sprite eyeOpen;
    public Sprite eyeMid;
    public Sprite eyeClosed;

    // Variables for the life
    public GameObject canvas;
    List<GameObject> eyeArray;
    int childNumber = 0;

    // Variables for the life-bar
    public GameObject lifeBar;
    float lifeBarPositionX;
    float lifeBarSizeX;

    public Flowchart fc;
    // Use this for initialization

    private SpriteRenderer myRenderer;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;

    public AudioSource damageSound;

    void Start() {
        // Update maxLife with the one in the save file, if needed
        currentLife = maxLife;

        // Initialize life-bar variables
        // lifeBarPositionX = lifeBar.transform.localPosition.x;
        // lifeBarSizeX = lifeBar.transform.localScale.x;

        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default");

        // Pupulate lifeArray    
        eyeArray = new List<GameObject>();

        foreach (Transform child in canvas.transform)
            childNumber++;

        // Debug.Log(childNumber);

        for (int i = 1; i <= childNumber; i++) {
            foreach (Transform child in canvas.transform) {
                if (child.name == "Eye (" + i + ")") {
                    eyeArray.Add(child.gameObject);
                    // Debug.Log("Eye (" + i + ")");
                }
            }
        }

        updateLifeGUI();
    }

    public void damagePlayer(int damage) {
        if (currentLife - damage <= 0) {
            this.transform.GetComponent<Player>().setAllowMovementFalse();
            currentLife = 0;
            updateLifeGUI();
            killPlayer();
        } else {
            currentLife = currentLife - damage;
            StartCoroutine(Blink());
            damageSound.Play();
        }

        updateLifeGUI();
        // updateLifeBar();
    }

    public void healPlayer(int heal) {
        currentLife = currentLife + heal;

        if (currentLife > maxLife)
            currentLife = maxLife;

        updateLifeGUI();
        // updateLifeBar();
    }

    public void updateLifeGUI() {
        int displayedLife = 0;

        if (currentLife % 2 == 0) {
            // Se currentLife è pari setto currentLife / 2 a 2 assieme a quelli precedenti, quelli sucessivi a 0
            displayedLife = currentLife / 2;

            for (int i = 0; i < displayedLife; i++) {
                eyeArray[i].GetComponent<UnityEngine.UI.Image>().sprite = eyeOpen;
            }

            for (int i = displayedLife; i < maxLife / 2; i++) {
                eyeArray[i].GetComponent<UnityEngine.UI.Image>().sprite = eyeClosed;
            }
        } else {
            // Se currentLife è dispari setto currentLife / 2 a 1, quelli precedenti a 2, quelli sucessivi a 0
            displayedLife = currentLife / 2;

            for (int i = 0; i < displayedLife; i++) {
                eyeArray[i].GetComponent<UnityEngine.UI.Image>().sprite = eyeOpen;
            }

            eyeArray[displayedLife].GetComponent<UnityEngine.UI.Image>().sprite = eyeMid;

            for (int i = displayedLife + 1; i < maxLife / 2; i++) {
                eyeArray[i].GetComponent<UnityEngine.UI.Image>().sprite = eyeClosed;
            }
        }
    }

    public void updateLifeBar() {
        lifeBar.transform.localScale = new Vector3((currentLife * 1f) / maxLife * lifeBarSizeX, lifeBar.transform.localScale.y, lifeBar.transform.localScale.x);
        lifeBar.transform.localPosition = new Vector3(lifeBarPositionX - (1 - (currentLife * 1f) / maxLife) * lifeBarSizeX * 2, lifeBar.transform.localPosition.y, lifeBar.transform.localPosition.x);
    }


    public void killPlayer() {
        damageSound.Play();
        StartCoroutine(Blink());
        fc.ExecuteBlock("DeathBlock");
    }

    IEnumerator Blink() {
        whiteSprite();
        yield return new WaitForSeconds(0.05f);
        normalSprite();
    }

    void whiteSprite() {
        if (myRenderer.material.shader != null) {
            Debug.Log("White");
            myRenderer.material.shader = shaderGUItext;
            myRenderer.color = Color.white;

            foreach (Transform child in transform) {
                try {
                    child.gameObject.GetComponent<SpriteRenderer>().material.shader = shaderGUItext;
                    child.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                } catch { }
            }
        }
    }

    void normalSprite() {
        Debug.Log("Normal");

        myRenderer.material.shader = shaderSpritesDefault;
        myRenderer.color = Color.white;

        foreach (Transform child in transform) {
            try {
                child.gameObject.GetComponent<SpriteRenderer>().material.shader = shaderSpritesDefault;
                child.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            } catch { }
        }
    }

}