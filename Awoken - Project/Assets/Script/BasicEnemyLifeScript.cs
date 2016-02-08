using UnityEngine;
using System.Collections;

public class BasicEnemyLifeScript : MonoBehaviour {

    public int maxLife;
    public int armor;

    public Transform drop = null;
    public float dropRate = 1f;
    public bool mustDrop = false;
    public bool sound = false;

    public AudioSource damageSound;
    public AudioClip dieSound;

    public Vector2 curPos;
    public Vector2 lastPos;

    int currentLife;

    private SpriteRenderer myRenderer;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;

    // Use this for initialization
    void Start() {
        currentLife = maxLife;

        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default");
    }

    public void damage(int damage) {
        StartCoroutine(Blink());
        if (currentLife - damage / armor <= 0) {
            kill();
        } else {
            currentLife = currentLife - damage / armor;

            if (damageSound != null)
                damageSound.Play();
        }
    }

    void kill() {
        if (dieSound != null) {
            AudioSource.PlayClipAtPoint(dieSound, transform.position);
            Destroy(gameObject);
        } else
            Destroy(gameObject);

        if (mustDrop)
            if (UnityEngine.Random.Range(0f, 1f) > (1 - dropRate))
                Instantiate(drop, transform.position, Quaternion.identity);
    }

    IEnumerator Blink() {
        whiteSprite();
        yield return new WaitForSeconds(0.05f);
        normalSprite();
    }

    void whiteSprite() {
        if (myRenderer.material.shader != null) {

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