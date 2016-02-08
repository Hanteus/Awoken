using UnityEngine;

public class EyeScript : MonoBehaviour {

    public LineRenderer eyeRayLR;
    public bool active;
    public string layerMaskString;
    public AudioSource raySound;

    int layerMask;
    bool targetLocked;

    Vector2 rayStart;
    Vector2 rayDirection;
    Vector2 rayEnd;
    Vector3 rayStartVisual;
    Vector3 rotationVector;
    Vector3 visualEnd;

    RaycastHit2D hit;

    GameObject target;
    GameObject rayStartObject;
    Transform player;

    // Use this for initialization
    void Start() {
        visualEnd.z = 4.5f;
        active = false;
        layerMask = LayerMask.NameToLayer(layerMaskString);
        rayStartObject = this.transform.parent.FindChild("RayStart").gameObject;
        player = this.transform.parent;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            active = true;
            riseArm();
            showRay();
        }
        if (Input.GetMouseButtonUp(1)) {
            active = false;
            lowerArm();
            hideRay();
        }
        if (active && !targetLocked)
            updateRay();
        else if (active && targetLocked)
            updateRayLocked();
    }

    void riseArm() {
        raySound.Play();

        rotationVector = transform.rotation.eulerAngles;
        rotationVector.z = 0f;
        transform.rotation = Quaternion.Euler(rotationVector);
    }

    void lowerArm() {
        raySound.Pause();

        rotationVector = transform.rotation.eulerAngles;
        rotationVector.z = -65f;
        transform.rotation = Quaternion.Euler(rotationVector);
    }

    void updateRay() {
        rayStartVisual = rayStartObject.transform.position;
        rayStartVisual.z = 4.5f;
        rayStart = rayStartVisual;

        rayEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        rayDirection = rayEnd - rayStart;

        hit = Physics2D.Raycast(rayStart, rayDirection, Mathf.Infinity, (1 << layerMask));

        visualEnd.x = hit.point.x;
        visualEnd.y = hit.point.y;

        eyeRayLR.SetPosition(0, rayStartVisual);

        if (hit == true)
            eyeRayLR.SetPosition(1, visualEnd);
    }

    public bool reachable(GameObject g) {
        Vector3 gPosition = g.transform.position;
        Vector3 tempRayEnd = g.transform.position;

        rayDirection = tempRayEnd - player.position;

        hit = Physics2D.Raycast(player.position, rayDirection, Mathf.Infinity, (1 << layerMask));

        if (hit.transform.GetHashCode() == g.transform.GetHashCode())
            return true;

        return false;
    }

    void updateRayLocked() {
        eyeRayLR.SetPosition(0, rayStartObject.transform.position);
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

    public void unlockTarget() {
        targetLocked = false;
    }

}

// Debug.Log("Hit: " + hit.point.x + ", " + hit.point.y + " Hash: " + hit.transform.name);
// Mathf.Infinity, layerMask, Mathf.Infinity, Mathf.Infinity
// Debug.DrawLine(rayStart, rayEnd, Color.cyan);

// Debug.DrawLine(Vector3.zero, new Vector3(rayStart.x, rayStart.y, 0), Color.yellow);
// Debug.DrawLine(Vector3.zero, new Vector3(rayStartVisual.x, rayStartVisual.y, 0), Color.green);
// Debug.Log("Strat: " + rayStart.x + " " + rayStart.y);
// Debug.Log("End: " + rayEnd.x + " " + rayEnd.y);