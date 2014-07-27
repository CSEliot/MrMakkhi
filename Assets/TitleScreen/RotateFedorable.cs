using UnityEngine;
using System.Collections;

public class RotateFedorable : MonoBehaviour {

	public float speed;
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3(0,speed,0));
	}
}
