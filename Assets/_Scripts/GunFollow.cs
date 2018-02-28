using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFollow : MonoBehaviour {

    public RectTransform uGUICanvas;
	
	// Update is called once per frame
	void Update () {
        //屏幕坐标转为世界坐标
        Vector3 mousePos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            uGUICanvas, new Vector2(Input.mousePosition.x, Input.mousePosition.y),
            Camera.main, out mousePos);
        
        float z;
        if (mousePos.x > transform.position.x)
        {
            z = -Vector3.Angle(Vector3.up, mousePos - transform.position);
        }
        else
        {
            z = Vector3.Angle(Vector3.up, (mousePos - transform.position));
        }

        transform.localRotation = Quaternion.Euler(0, 0, z);
    }
}
