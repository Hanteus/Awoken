using UnityEngine;

public class EyeScript : MonoBehaviour
{

    public LineRenderer eyeRayLR;
    public bool active;
    public string layerMaskString;

    int layerMask;
    bool targetLocked;

    Vector2 rayStart;
    Vector2 rayEnd;
    Vector3 rayStartVisual;
    Vector3 rotationVector;
    Vector3 extendedEnd;

    RaycastHit2D hit;

    GameObject target;
    GameObject rayStartObject;

    // Use this for initialization
    void Start()
    {
        active = false;
        extendedEnd.z = transform.position.z;
        layerMask = LayerMask.NameToLayer(layerMaskString);
        rayStartObject = this.transform.parent.FindChild("RayStart").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            active = true;
            riseArm();
            showRay();
        }
        if (Input.GetMouseButtonUp(1))
        {
            active = false;
            lowerArm();
            hideRay();
        }
        if (active && !targetLocked)
            updateRay();
        else if (active && targetLocked)
            updateRayLocked();
    }

    void riseArm()
    {
        rotationVector = transform.rotation.eulerAngles;
        rotationVector.z = 65f;
        transform.rotation = Quaternion.Euler(rotationVector);
    }

    void lowerArm()
    {
        rotationVector = transform.rotation.eulerAngles;
        rotationVector.z = 0f;
        transform.rotation = Quaternion.Euler(rotationVector);
    }

    void updateRay()
    {
        rayEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.parent.position;

        rayStartVisual = rayStartObject.transform.position;
        rayStart = - rayStartObject.transform.localPosition + transform.parent.localPosition;

        Debug.Log(rayStartObject.transform.localPosition + " - " + transform.parent.localPosition);

        hit = Physics2D.Raycast(rayStart, rayEnd, Mathf.Infinity, (1 << layerMask));

        eyeRayLR.SetPosition(0, rayStartVisual);

        if (hit == true)
        {
            eyeRayLR.SetPosition(1, hit.point);
        }
        else
        {
            extendedEnd.x = rayEnd.x;
            extendedEnd.y = rayEnd.y;
            extendedEnd.z = 5;

            eyeRayLR.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    void updateRayLocked()
    {
        eyeRayLR.SetPosition(0, transform.position);
        eyeRayLR.SetPosition(1, target.transform.position);
    }

    void showRay()
    {
        eyeRayLR.enabled = true;
    }

    void hideRay()
    {
        eyeRayLR.enabled = false;
        unlockTarget();
    }

    public void lockOnTarget(GameObject target)
    {
        targetLocked = true;
        this.target = target;
    }

    public void unlockTarget()
    {
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