using UnityEngine;
using System;

public class OscillingLineScript : MonoBehaviour {

    public GameObject obj1;
    public GameObject obj2;
    public LineRenderer lineRenderer;
    public float noise;
    public float basicStep;
    public float depth = 4.5f;

    float deltaX;
    float deltaY;
    float xStep;
    float yStep;
    int nPoints;
    Vector3 position;

    // Use this for initialization
    void Start() {
        position.z = 5f;
        lineRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update() {
        // Si può mettere qui abs e sfoltire codice più sotto
        deltaX = obj1.transform.position.x - obj2.transform.position.x;
        deltaY = obj1.transform.position.y - obj2.transform.position.y;

        if (Math.Abs(deltaX) > Math.Abs(deltaY)) {
            nPoints = (int)(Math.Abs(deltaX) / basicStep);
        } else {
            nPoints = (int)(Math.Abs(deltaY) / basicStep);
        }

        if (nPoints == 0 | nPoints == 1)
            nPoints = 2;

        xStep = deltaX / nPoints;
        yStep = deltaY / nPoints;

        lineRenderer.SetVertexCount(nPoints + 1);

        for (int i = 1; i < nPoints; i++) {
            if (obj1.transform.position.x > obj2.transform.position.x) {
                if (obj1.transform.position.y > obj2.transform.position.y) {
                    position.x = obj2.transform.position.x + i * xStep + UnityEngine.Random.Range(-1f, 1f) * noise;
                    position.y = obj2.transform.position.y + i * yStep + UnityEngine.Random.Range(-1f, 1f) * noise;
                } else {
                    position.x = obj2.transform.position.x + i * xStep + UnityEngine.Random.Range(-1f, 1f) * noise;
                    position.y = obj2.transform.position.y + i * yStep + UnityEngine.Random.Range(-1f, 1f) * noise;
                }
            } else {
                if (obj1.transform.position.y > obj2.transform.position.y) {
                    position.x = obj2.transform.position.x + i * xStep + UnityEngine.Random.Range(-1f, 1f) * noise;
                    position.y = obj2.transform.position.y + i * yStep + UnityEngine.Random.Range(-1f, 1f) * noise;
                } else {
                    position.x = obj2.transform.position.x + i * xStep + UnityEngine.Random.Range(-1f, 1f) * noise;
                    position.y = obj2.transform.position.y + i * yStep + UnityEngine.Random.Range(-1f, 1f) * noise;
                }
            }

            position.z = depth;
            lineRenderer.SetPosition(i, position);
        }

        lineRenderer.SetPosition(0, obj2.transform.position);
        lineRenderer.SetPosition(nPoints, obj1.transform.position);
    }

    public void setObj1(GameObject obj) {
        obj1 = obj;
    }

    public void setObj2(GameObject obj) {
        obj2 = obj;
    }

    public void showRay() {
        lineRenderer.enabled = true;
    }

    public void hideRay() {
        lineRenderer.enabled = false;
    }

}
