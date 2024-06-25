using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        BoxCollider2D col = gameObject.AddComponent<BoxCollider2D>();
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        col.size = new Vector2(0.8f, 1);
        col.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
