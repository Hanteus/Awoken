using UnityEngine;
using System.Collections;

public class StaffHolderScript : MonoBehaviour {

    public GameObject staff;

    void OnTriggerEnter2D(Collider2D entered) {
        if (entered.tag == "Player") {
            entered.gameObject.GetComponent<Player>().activateDEye();
            staff.gameObject.SetActive(false);
        }
    }

}
