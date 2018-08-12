using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour {

    public static QueueManager Instance;

    public GameObject[] passengers;

    public Queue<GameObject> passengerQueue;

    public Transform[] queuePositions;

    void Awake()
	{
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
	}

	void Start()
	{
        passengerQueue = new Queue<GameObject>();
        foreach(Transform pos in queuePositions){
            GameObject newPassenger = Instantiate(passengers[Random.Range(0, passengers.Length)], pos.position, Quaternion.identity);
            newPassenger.transform.parent = transform;
            passengerQueue.Enqueue(newPassenger);
        }
	}

	public GameObject NextInQueue(){
        GameObject nextPassenger = passengerQueue.Dequeue();
        int counter = 0;
        foreach(GameObject go in passengerQueue){
            go.transform.position = queuePositions[counter].position;
            counter += 1;
        }
        GameObject newPassenger = Instantiate(passengers[Random.Range(0, passengers.Length)],queuePositions[queuePositions.Length-1].position,Quaternion.identity);
        newPassenger.transform.parent = transform;
        passengerQueue.Enqueue(newPassenger);
        return nextPassenger;
    }
}
