using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectWaterWave : MonoBehaviour {

    public Texture[] texture;
    private Material material;
    private int index = 0;

	// Use this for initialization
	void Start () {
        material = this.gameObject.GetComponent<MeshRenderer>().material;
        //延迟0秒，间隔0.1秒变化texture
        InvokeRepeating("ChangeTexture", 0, 0.1f);
	}
	
	void ChangeTexture () {
        material.mainTexture = texture[index];
        //求模计算保证index不越界
        index = (index + 1) % texture.Length;
	}
}
