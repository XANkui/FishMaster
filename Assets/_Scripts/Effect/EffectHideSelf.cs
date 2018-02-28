using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHideSelf : MonoBehaviour {

    public IEnumerator HideSelf(float delayTime) {
        yield return new WaitForSeconds(delayTime);
        gameObject.SetActive(false);
    }
}
