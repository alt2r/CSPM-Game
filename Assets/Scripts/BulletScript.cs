using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    bool paused;
    float yMovement = 0;
    TurretScript turretScript;
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
        transform.Translate(new Vector2(Constants.BULLET_SPEED * Time.deltaTime, yMovement * Time.deltaTime));
    }

    public void SetReferenceToTurretScript(TurretScript ts)
    {
        turretScript = ts;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name != gameObject.name && col.name != "Firewall(Clone)")
        {
            gameObject.SetActive(false);
            turretScript.addToInactiveBulletList(gameObject);
            yMovement = 0; //needs to be reset
        }
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

    public void setYMovement(float y)
    {
        yMovement = y;
    }
}
