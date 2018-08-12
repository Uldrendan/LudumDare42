using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour {

    public static QueueManager Instance;

    public GameObject[] passengers;

    public Queue<GameObject> passengerQueue;

    public int queSize;
    Transform[] queuePositions;


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
        for (int i = 0; i < queSize; i++)
        {
            GameObject newPassenger = Instantiate(passengers[Random.Range(0, passengers.Length)], new Vector2(transform.position.x+i,transform.position.y), Quaternion.identity);
            newPassenger.transform.parent = transform;
            passengerQueue.Enqueue(newPassenger);
        }
	}

	public GameObject NextInQueue(){
        GameObject nextPassenger = passengerQueue.Dequeue();
        int counter = 0;
        foreach(GameObject go in passengerQueue){
            go.transform.position = new Vector2(go.transform.position.x - 1, go.transform.position.y);
            counter += 1;
        }
        GameObject newPassenger = Instantiate(passengers[Random.Range(0, passengers.Length)],new Vector2(transform.position.x+queSize-1,transform.position.y),Quaternion.identity);
        newPassenger.transform.parent = transform;
        passengerQueue.Enqueue(newPassenger);
        return nextPassenger;
    }
}
