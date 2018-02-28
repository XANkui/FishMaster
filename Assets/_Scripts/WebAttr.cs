using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebAttr : MonoBehaviour {

    public float disappearTime = 0.5f;
    public int damage = 5;

    private void Start()
    {
        Destroy(gameObject, disappearTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Fish"))
        {
            collision.SendMessage("TakeDamage", damage);
        }
    }
}
