using UnityEngine;
using System.Collections;

public class BoobieJigglerSpawner : MonoBehaviour {

	public GameObject neckbeard;
	public float spawnTime = 3;
	private float timer;

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > spawnTime) {
						Instantiate (neckbeard, transform.position, transform.rotation);
						timer = 0;
				}
	}
}
