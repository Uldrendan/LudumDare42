using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainBehaviour : MonoBehaviour {

    public GameObject TrainDoor;
    public float AdvancementSpeed = 1f;

    Vector3 _startLocation;
    public Vector3 BoardingPlatform = new Vector3(0, 0, 0);
    public Vector3 FinalStop = new Vector3(-25, 0, 0);

    public enum moveState
    {
        Arriving,
        Advancing,
        Loading,
        Departing,
        Departed
    }
    public moveState TrainMoveState = moveState.Arriving;

    // Use this for initialization
    void Start () {
        _startLocation = transform.position;

        TrainMoveState = moveState.Arriving;
    }

    void AdvanceToDestination(Vector3 Destination)
    {
        // The step size is equal to speed times frame time.
        float step = AdvancementSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, Destination, step);
        if (transform.position == BoardingPlatform)
        {
            TrainMoveState = moveState.Loading;
        }
        else if (transform.position == FinalStop)
        {
            TrainMoveState = moveState.Departed;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (TrainMoveState == moveState.Arriving)
        {
            if (Input.GetKeyDown(KeyCode.A))
                TrainMoveState = moveState.Advancing;
        }
        else if (TrainMoveState == moveState.Advancing)
        {
            AdvanceToDestination(BoardingPlatform);
        }
        else if (TrainMoveState == moveState.Loading)
        {
            if (Input.GetKeyDown(KeyCode.A))
                TrainMoveState = moveState.Departing;
        }
        else if (TrainMoveState == moveState.Departing)
        {
            AdvanceToDestination(FinalStop);
        }
        else if (TrainMoveState == moveState.Departed)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.position = _startLocation;
                TrainMoveState = moveState.Arriving;
            }
        }

    }
}
