using UnityEngine;
using System.Collections;

public class PlayGameNow : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp ("space")) Application.LoadLevel ("TheActualJungle");
	}
}
