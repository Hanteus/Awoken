using UnityEngine;
using System.Collections;

public class GunHolderScript : MonoBehaviour {

    public GameObject gun;

    void OnTriggerEnter2D(Collider2D entered) {
        if (entered.tag == "Player") {
            entered.gameObject.GetComponent<Player>().activateDMouth();
            gun.gameObject.SetActive(false);
        }
    }

}
