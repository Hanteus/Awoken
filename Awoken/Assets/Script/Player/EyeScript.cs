using UnityEngine;
using System.Collections;

public class EyeScript : MonoBehaviour {

	public LineRenderer eyeRayLR;
    public bool active;
    public string layerMaskString;

    Vector3 rayEnd;
    Vector3 rayStart;
    Vector3 extendedEnd;
    RaycastHit2D hit;
    int layerMask;
    bool targetLocked;
    GameObject target;

    // Use this for initialization
    void Start () {
		active = false;
		rayEnd.z = transform.position.z;
        extendedEnd.z = transform.position.z;
        layerMask = LayerMask.NameToLayer(layerMaskString);
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetMouseButtonDown (1)) {
			active = true;
			showRay();
		}
		if (Input.GetMouseButtonUp (1)) {
			active = false;
			hideRay();
		}
        if (active && !targetLocked)
            updateRay();
        else if (active && targetLocked)
            updateRayLocked();
    }

	void updateRay() {
		rayEnd.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.parent.transform.position.x;
		rayEnd.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.parent.transform.position.y;

		rayStart = transform.position;

        hit = Physics2D.Raycast(rayStart, rayEnd, Mathf.Infinity, (1 << layerMask));
        eyeRayLR.SetPosition(0, rayStart);

        if (hit == true)
        {
            eyeRayLR.SetPosition(1, hit.point);
        }
        else {
            extendedEnd.x = rayEnd.x * 100;
            extendedEnd.y = rayEnd.y * 100;
            extendedEnd.z = 5;
            eyeRayLR.SetPosition(1, extendedEnd);
        }
    }

    void updateRayLocked() {
        eyeRayLR.SetPosition(0, transform.position);
        eyeRayLR.SetPosition(1, target.transform.position);
    }

    void showRay() {
		eyeRayLR.enabled = true;
	}

	void hideRay() {
		eyeRayLR.enabled = false;
        unlockTarget();
    }

    public void lockOnTarget(GameObject target) {
        targetLocked = true;
        this.target = target;
    }

    public void unlockTarget(){
        targetLocked = false;
    }

}

// Debug.Log("Hit: " + hit.point.x + ", " + hit.point.y + " Hash: " + hit.transform.name);
// Mathf.Infinity, layerMask, Mathf.Infinity, Mathf.Infinity
// Debug.DrawLine(rayStart, rayEnd, Color.cyan);