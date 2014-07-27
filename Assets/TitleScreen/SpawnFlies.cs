using UnityEngine;
using System.Collections;

public class SpawnFlies : MonoBehaviour {

	public GameObject fly;
	
	// Update is called once per frame
	void Update () {
		if(Random.value*10 < 0.1f) Instantiate (fly, new Vector3 (0, -10, 0), new Quaternion (0, 0, 0,0));
	}
}
