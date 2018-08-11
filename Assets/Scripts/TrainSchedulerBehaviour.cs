using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSchedulerBehaviour : MonoBehaviour {

    public float TrainTravelingTime = 5f;
    public float DoorOpenTime = 1f;
    public float TrainLoadingTime = 20f;
    public float RoundResetTime = 1f;
    public GameObject ArrivalLight;
    public GameObject DepartureLight;
    public GameObject Train;

    float advancementTimer;

    public Vector3 StartLocation = new Vector3(25, 0, 0);
    public Vector3 BoardingPlatform = new Vector3(0, 0, 0);
    public Vector3 FinalStop = new Vector3(-25, 0, 0);

    public enum TrainMoveState
    {
        Waiting,
        Arriving,
        Loading,
        Departing
    }

    TrainMoveState trainMoveState;

    // Use this for initialization
    void Start () {
        Train.transform.position = StartLocation;
        trainMoveState = TrainMoveState.Waiting;
        advancementTimer = RoundResetTime;

        StartCoroutine(TrainSchedulerRoutine());
    }

    IEnumerator TrainSchedulerRoutine()
    {
        while(true)
        {
            advancementTimer -= Time.deltaTime;
            if (advancementTimer <= 0)
            {
                if (trainMoveState == TrainMoveState.Waiting)
                {
                    ArrivalLight.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
                    Debug.Log("Arriving");
                    trainMoveState = TrainMoveState.Arriving;
                    Train.GetComponent<TrainBehaviour>().SetDestination(BoardingPlatform, TrainTravelingTime, DoorOpenTime);
                    advancementTimer = TrainTravelingTime + DoorOpenTime;
                }
                else if (trainMoveState == TrainMoveState.Arriving)
                {
                    ArrivalLight.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
                    Debug.Log("Loading");
                    trainMoveState = TrainMoveState.Loading;
                    advancementTimer = TrainLoadingTime;
                }
                else if (trainMoveState == TrainMoveState.Loading)
                {
                    DepartureLight.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
                    Debug.Log("Departing");
                    trainMoveState = TrainMoveState.Departing;
                    Train.GetComponent<TrainBehaviour>().SetDestination(FinalStop, TrainTravelingTime, DoorOpenTime);
                    advancementTimer = TrainTravelingTime + DoorOpenTime;
                }
                else if (trainMoveState == TrainMoveState.Departing)
                {
                    DepartureLight.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
                    Debug.Log("Waiting");
                    trainMoveState = TrainMoveState.Waiting;
                    Train.transform.position = StartLocation;
                    Train.GetComponent<TrainBehaviour>().SetDestination(StartLocation);
                    advancementTimer = RoundResetTime;
                }
            }
            yield return new WaitForEndOfFrame();
        }
	}
}
