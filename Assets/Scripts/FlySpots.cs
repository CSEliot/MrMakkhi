using UnityEngine;
using System.Collections;

public class FlySpots : MonoBehaviour {

    public Vector3[] flySpots;

    public Vector3 NewSpot() 
    {	
	    int randSpot = Random.Range(0, flySpots.Length - 1);
	
	    return flySpots[randSpot];
    }

	void Start () {

	}
	
	void Update () {
	
	}
}
