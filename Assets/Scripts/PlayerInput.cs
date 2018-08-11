using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public GameObject[] passengers;

	void Update () {
        if(Input.GetMouseButtonDown(0)){
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(passengers[Random.Range(0, passengers.Length)], new Vector3(mousePos.x,mousePos.y,0), Quaternion.identity);
        }
	}
}
