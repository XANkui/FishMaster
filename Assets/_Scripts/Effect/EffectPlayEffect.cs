using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPlayEffect : MonoBehaviour {

    public GameObject[] effectPrefabs;

    public void PlayEffcet() {
        foreach (GameObject effect in effectPrefabs) {
            Instantiate(effect);
        }
        
    }
}
