using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMoveToward : MonoBehaviour {

    private GameObject goldCollector;

	// Use this for initialization
	void Start () {
        goldCollector = GameObject.Find("GoldCollector");
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, goldCollector.transform.position, Time.deltaTime);
	}
}
