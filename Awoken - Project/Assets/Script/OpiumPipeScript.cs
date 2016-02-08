using UnityEngine;
using System.Collections;

public class OpiumPipeScript : MonoBehaviour
{

    public int heal = 1;

    void OnTriggerEnter2D(Collider2D entered)
    {
        if (entered.tag == "Player")
        {
            entered.GetComponent<LifeScript>().healPlayer(heal);
            Destroy(transform.parent.gameObject);
        }
    }

}