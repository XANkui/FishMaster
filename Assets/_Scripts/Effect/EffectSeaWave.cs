using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSeaWave : MonoBehaviour {

    private Vector3 targetPos;

    private void Start()
    {
        targetPos = -transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, 8 * Time.deltaTime);
    }
}
