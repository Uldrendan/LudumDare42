using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public GameObject[] passengers;
    public GameObject trajectoryDot;
    public float speed = 10;
    public int numDots = 10;
    public float timeStep = 0.05f;

    List<GameObject> dotTrail;
    GameObject currentPassenger;
    bool aiming;
    Vector3 aimVector;
    Vector3 startPoint;

	void Update () {
        if (dotTrail != null)
        {
            foreach (GameObject go in dotTrail)
                Destroy(go);
        }
        if (aiming)
        {
            Vector2 heading = Camera.main.ScreenToWorldPoint(Input.mousePosition) - startPoint;
            float distance = heading.magnitude;
            aimVector = (heading / distance) * -1 * speed;

            dotTrail = new List<GameObject>();
            for (int i = 0; i < numDots; i++)
            {
                Vector2 newPosition = PredictPosition(timeStep * i);
                if (float.IsNaN(newPosition.x) || float.IsNaN(newPosition.y))
                    continue;
                GameObject tempTrajectoryDot = Instantiate(trajectoryDot, newPosition,Quaternion.identity);
                dotTrail.Add(tempTrajectoryDot);
            }
        }

        if(Input.GetMouseButtonUp(0)){
            currentPassenger.GetComponent<Rigidbody2D>().gravityScale = 1;
            currentPassenger.GetComponent<Rigidbody2D>().velocity = aimVector;
            currentPassenger.GetComponent<PolygonCollider2D>().enabled = true;
            aiming = false;
        }
        if(Input.GetMouseButtonDown(0)){
            currentPassenger = QueueManager.Instance.NextInQueue();
            currentPassenger.transform.position = transform.position;
            startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            aiming = true;
        }
	}

    Vector2 PredictPosition(float elapsedTime){
        return Physics.gravity * elapsedTime * elapsedTime * 0.5f +
                      aimVector * elapsedTime + transform.position;
    }
}