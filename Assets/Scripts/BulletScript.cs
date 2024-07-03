using UnityEngine;

public class BulletScript : MonoBehaviour
{
    bool paused;
    float yMovement = 0;
    TurretScript turretScript;
    void Start()
    {
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        BoxCollider2D col = gameObject.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
        return;
    }
    
    void Update()
    {
        if(!paused)
        transform.Translate(new Vector2(Constants.BULLET_SPEED * Time.deltaTime, yMovement * Time.deltaTime));
        return;
    }

    public void SetReferenceToTurretScript(TurretScript ts)
    {
        turretScript = ts;
        return;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name != gameObject.name && col.name != Constants.FIREWALL_G_O_NAME)
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
        return;
    }

    public void Resume()
    {
        paused = false;
        return;
    }

    public void setYMovement(float y)
    {
        yMovement = y;
        return;
    }
}
