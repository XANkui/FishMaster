using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroySelf : MonoBehaviour {

    public float delayTime = 0.5f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, delayTime);
	}
	
}
