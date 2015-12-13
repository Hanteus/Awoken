using UnityEngine;
using System.Collections;

public class BasicEnemyLifeScript : MonoBehaviour {

    public int maxLife;
    public int armor;

    int currentLife;

	// Use this for initialization
	void Start () {
        currentLife = maxLife;
    }

    public void damage(int damage) {
        Debug.Log("Ahia!");
        if (currentLife - damage / armor < 0)
            kill();
        else
            currentLife = currentLife - damage / armor;
    }

    void kill() {
        Destroy(gameObject);
    }

}