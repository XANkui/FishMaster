﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCollect : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Gold")) {
            Destroy(collision.gameObject);
        }
    }
}
