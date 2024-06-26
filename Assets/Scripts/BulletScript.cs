using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    bool paused;
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
        if(!paused)
        transform.Translate(new Vector2(Constants.BULLET_SPEED * Time.deltaTime, 0));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
        Destroy(this);
        return;
    }

    public void Pause()
    {
        paused = true;
    }

    public void Resume()
    {
        paused = false;
    }
}
