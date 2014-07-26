using UnityEngine;
using System.Collections;

public class FlyAround2D : MonoBehaviour {

	public float rotateSpeed = 5;
	public float flySpeed = 0.1f;

	private float timer = 0;
	private bool spawned = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!spawned) {
						transform.Rotate (Vector3.forward * 90);
						spawned = true;
				}

		if (Random.value > 0.5f) transform.Rotate (Vector3.forward * rotateSpeed);
		else transform.Rotate (Vector3.forward * rotateSpeed * -1);

		transform.Translate (Vector2.right * flySpeed / 20);

		timer += Time.deltaTime;

		if (timer > 10) {
						Destroy (gameObject);
				}

	}
}
