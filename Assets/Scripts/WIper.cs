using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wiper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        BoxCollider2D col = gameObject.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(Constants.WIPER_MOVE_SPEED * Time.deltaTime, 0));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == Constants.RIGHT_WALL_NAME)
        {
            Destroy(gameObject);
        }
    }
}
