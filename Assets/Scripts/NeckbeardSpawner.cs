using UnityEngine;
using System.Collections;

public class NeckbeardSpawner : MonoBehaviour {

	public float timerTime = 30;
	private float time = 0;
	public GameObject neckbeard;

	private bool firstspawned = false;

	// Update is called once per frame
	void Update () {
		if (!firstspawned) {
						Instantiate (neckbeard, transform.position, transform.rotation);
						firstspawned = true;
				}
		time += Time.deltaTime;
		if(time > timerTime){
			time = 0;
			Instantiate(neckbeard, transform.position, transform.rotation);
		}
	}
}
