using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainBehaviour : MonoBehaviour {

    public GameObject TrainDoor;
    Vector3 _currentDestination;

    float t;
    Vector3 startPosition;
    Vector3 target;
    float timeToReachTarget;
    float doorTimer;

    enum DoorState
    {
        open,
        closed
    }
    DoorState doorState = DoorState.closed;

    void Start()
    {
        startPosition = transform.position;
        target = transform.position;
    }

    IEnumerator AdvanceTrain()
    {
        if (doorState == DoorState.closed)
        {
            t = 0;
            while (transform.position != target)
            {
                t += Time.deltaTime / timeToReachTarget;
                transform.position = Vector3.Lerp(startPosition, target, t);
                yield return new WaitForEndOfFrame();
            }
            t = 0;
            //open door animation trigger
            TrainDoor.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            while (t < doorTimer)
            {
                t += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            doorState = DoorState.open;
        }
        else
        {
            t = 0;
            //close door animation trigger
            TrainDoor.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            while (t < doorTimer)
            {
                t += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            t = 0;
            doorState = DoorState.closed;
            TrainDoor.GetComponent<ScoreKeeper>().ShowScore();
            while (transform.position != target)
            {
                t += Time.deltaTime / timeToReachTarget;
                transform.position = Vector3.Lerp(startPosition, target, t);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    public void SetDestination(Vector3 destination, float travelTime, float doorOpenTime)
    {
        t = 0;
        startPosition = transform.position;
        timeToReachTarget = travelTime;
        doorTimer = doorOpenTime;
        target = destination;
        StartCoroutine(AdvanceTrain());
    }

    public void SetDestination(Vector3 destination)
    {
        t = 0;
        target = destination;
    }
}
