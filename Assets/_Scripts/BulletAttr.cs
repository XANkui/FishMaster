using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttr : MonoBehaviour {

    public int speed = 10;
    public int damage = 5;
    public GameObject webGo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Border")) {
            Destroy(gameObject);
        }
        if (collision.tag.Equals("Fish"))
        {
            GameObject web = Instantiate(webGo);
            web.transform.SetParent(transform.parent, false);
            web.transform.position = transform.position;
            web.GetComponent<WebAttr>().damage = damage;
            Destroy(gameObject);
        }
    }
}
