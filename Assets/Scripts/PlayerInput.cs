using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public GameObject[] passengers;
    public float maximumForce = 10;
    public float minimumForce = 1;

    GameObject currentPassenger;
    bool aiming;
    Vector2 aimVector;
    Vector3 startPoint;

	void Update () {
        if (aiming)
        {
            Vector2 heading = Camera.main.ScreenToWorldPoint(Input.mousePosition) - startPoint;
            float distance = heading.magnitude;
            aimVector = (heading / distance) * -1;
        }

        if(Input.GetMouseButtonUp(0)){
            currentPassenger.GetComponent<Rigidbody2D>().gravityScale = 1;
            currentPassenger.GetComponent<Rigidbody2D>().AddForce(aimVector* 10,ForceMode2D.Impulse);
            aiming = false;
        }
        if(Input.GetMouseButtonDown(0)){
            currentPassenger = Instantiate(passengers[Random.Range(0, passengers.Length)], transform.position, Quaternion.identity);
            currentPassenger.GetComponent<Rigidbody2D>().gravityScale = 0;
            startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            aiming = true;
        }
	}
}