﻿using UnityEngine;
using System.Collections;

public class NeckbeardKill : MonoBehaviour {

	private float timer = 0;
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		
		if (timer > 10) {
			Destroy (gameObject);
		}
	}
}
