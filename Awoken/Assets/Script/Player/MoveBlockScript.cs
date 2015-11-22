using UnityEngine;
using System.Collections;

public class MoveBlockScript : MonoBehaviour {

	public bool HorizontalMovementEnabled;
	public bool VerticalMovementEnabled;
	public float speed;
	public GameObject limit1;
	public GameObject limit2;

	public bool inverted;
	public GameObject linkedBlock = null;
	MoveBlockScript linkedBlockScript = null;
    EyeScript eyeScript;

	bool over = false;
	bool selected = false;
	float xPercentagePosition;
	float yPercentagePosition;
	float xLimitDistance;
	float yLimitDistance;
	Vector3 mousePosition;
	Vector3 positionLimit1;
	Vector3 positionLimit2;
	public Vector3 tempPosition;
	
	// Use this for initialization
	void Start () {
		positionLimit1 = limit1.transform.position;
		positionLimit2 = limit2.transform.position;
		xLimitDistance = positionLimit2.x - positionLimit1.x;
		yLimitDistance = positionLimit2.y - positionLimit1.y;
		tempPosition.z = transform.position.z;
        eyeScript = GameObject.Find("Dreamer's Eye").GetComponent<EyeScript>();

        if (linkedBlock != null)
			linkedBlockScript = linkedBlock.GetComponent<MoveBlockScript>();
	}

	void OnMouseEnter() {
		over = true;
	}

	void OnMouseExit(){
		over = false;
	}

	// Update the block position if the player is moving it
	void Update () {
		if (over && Input.GetMouseButtonDown(1)) {
			selected = true;
            eyeScript.lockOnTarget(this.gameObject);
        } else if (!Input.GetMouseButton(1)) {
			selected = false;
            eyeScript.unlockTarget();
        }

		if (selected && VerticalMovementEnabled) {
			mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePosition.x = transform.position.x;
			mousePosition.z = transform.position.z;
		
			if (mousePosition.y - transform.position.y > 0.1 && transform.position.y < positionLimit2.y) {
				transform.position = Vector3.MoveTowards(transform.position, mousePosition, speed * Time.deltaTime);
				yUpdate();
			}
			else if (mousePosition.y - transform.position.y < -0.1 && transform.position.y > positionLimit1.y) {
				transform.position = Vector3.MoveTowards(transform.position, mousePosition, speed * Time.deltaTime);
				yUpdate();
			}
		} else if (selected && HorizontalMovementEnabled) {
			mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePosition.y = transform.position.y;
			mousePosition.z = transform.position.z;

			if (mousePosition.x - transform.position.x > 0.1 && transform.position.x < positionLimit2.x) {
				transform.position = Vector3.MoveTowards(transform.position, mousePosition, speed * Time.deltaTime);
				xUpdate();
			}
			else if (mousePosition.x - transform.position.x < -0.1 && transform.position.x > positionLimit1.x) {
				transform.position = Vector3.MoveTowards(transform.position, mousePosition, speed * Time.deltaTime);
				xUpdate();
			}
		}
	}

	// Update horizzontal
	void xUpdate() {
		xPercentageUpdate ();

		if (linkedBlockScript != null)
			linkedBlockScript.moveBlock (xPercentagePosition, xLimitDistance);
	}

	// Update vertical
	void yUpdate() {
		yPercentageUpdate ();

		if (linkedBlockScript != null)
			linkedBlockScript.moveBlock (yPercentagePosition, yLimitDistance);
	}

	// Update horizzontal percentage
	void xPercentageUpdate(){
		xPercentagePosition = (positionLimit2.x- transform.position.x) / xLimitDistance * 100;
		
		if (xPercentagePosition > 100)
			xPercentagePosition = 100;
		else if (xPercentagePosition < 0)
			xPercentagePosition = 0;
	}

	// Update vertical percentage
	void yPercentageUpdate(){
		yPercentagePosition = (positionLimit2.y - transform.position.y) / yLimitDistance * 100;
		
		if (yPercentagePosition > 100)
			yPercentagePosition = 100;
		else if (yPercentagePosition < 0)
			yPercentagePosition = 0;
	}

	// Invoked from the connected block to move this one
	void moveBlock (float percentagePosition, float limitDistance) {
		if (VerticalMovementEnabled) {
			tempPosition.x = transform.position.x;
			if (inverted)
				tempPosition.y = (percentagePosition) / 100 * yLimitDistance + positionLimit1.y;
			else
				tempPosition.y = (100 - percentagePosition) / 100 * yLimitDistance + positionLimit1.y;

			if (tempPosition.y < positionLimit1.y)
				tempPosition.y = positionLimit1.y;
			else if (tempPosition.y > positionLimit2.y)
				tempPosition.y = positionLimit1.y;
			
			transform.position = Vector3.MoveTowards (transform.position, tempPosition, yLimitDistance / limitDistance * speed * Time.deltaTime);
			yPercentageUpdate ();
		} else if (HorizontalMovementEnabled) {
			tempPosition.y = transform.position.y;
			if (inverted)
                tempPosition.x = (percentagePosition) / 100 * xLimitDistance + positionLimit1.x;
            else
                tempPosition.x = (100 - percentagePosition) / 100 * xLimitDistance + positionLimit1.x;

			if (tempPosition.x < positionLimit1.x)
				tempPosition.x = positionLimit1.x;
			else if (tempPosition.x > positionLimit2.x)
				tempPosition.x = positionLimit1.x;
			
			transform.position = Vector3.MoveTowards (transform.position, tempPosition, xLimitDistance / limitDistance * speed * Time.deltaTime);
			xPercentageUpdate ();
		}
	}

}