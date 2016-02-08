using UnityEngine;
using System.Collections;

public class TeleportScript : MonoBehaviour {

    public void Teleport() {
        Transform carterTransform = GameObject.Find("Carter").transform;
        float z = carterTransform.position.z;
        carterTransform.position.Set(transform.position.x, transform.position.y, z);
    }
}
