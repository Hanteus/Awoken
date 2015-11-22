using UnityEngine;
using System.Collections;

public class SpearScript : MonoBehaviour {

    public int damage = 1;

    LifeScript lifeScript;
    bool inside = false;

    void OnTriggerEnter2D(Collider2D entered) {
        if (entered.tag == "Player") {
            inside = true;
            lifeScript = entered.GetComponent<LifeScript>();
            StartCoroutine(DamageInTime());
        }
    }

    void OnTriggerExit2D(Collider2D exited) {
        if (exited.tag == "Player") {
            inside = false;
            StopCoroutine(DamageInTime());
        }
    }

    IEnumerator DamageInTime() {
        while(inside) {
            lifeScript.damagePlayer(damage);
            yield return new WaitForSeconds(1);
        }
    }

}