using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour
{

    public float delta = 0f;
    public float multiplier = 1f;

    Vector3 v3Pos;
    float fAngle;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        v3Pos = Input.mousePosition;
        v3Pos.z = (this.transform.position.z - Camera.main.transform.position.z);
        v3Pos = Camera.main.ScreenToWorldPoint(v3Pos);
        v3Pos = v3Pos - this.transform.position;

        fAngle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;
        if (fAngle < 0.0f)
            fAngle += 360.0f;

        Vector3 rotationVector = transform.rotation.eulerAngles;
        rotationVector.z = (fAngle + delta) * transform.parent.localScale.x * multiplier;
        transform.rotation = Quaternion.Euler(rotationVector);
    }

}
