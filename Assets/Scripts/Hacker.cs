using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Hacker : MalwareScript
{
    // Start is called before the first frame update
    float counter = 0;
    
    public void HackerInit(float speed, float health, Controller controller, Color color, Vector2 position)
    {
        base.init(speed, Constants.HACKER_RADIUS, health, controller, color);
        transform.position = position;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(counter <= 0)
            SpawnMalware();
        if(!controller.getPaused())
            counter -= Time.deltaTime;
    }

    public override void Move()
    {
        if(transform.position.x > Constants.HACKER_STOP_POINT) //move to the stopping point
        {
            base.Move();
        }
        else if(transform.position.x != Constants.HACKER_STOP_POINT)
        {
            transform.position = new UnityEngine.Vector2(Constants.HACKER_STOP_POINT, transform.position.y);
        }
    }

    void SpawnMalware()
    {
        this.controller.SpawnMalware(transform.position, true, color);
        counter = Constants.HACKER_SPAWN_RATE;
        return;
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "leftWall")
        ReachedBase();

        if(col.gameObject.name == "Bullet(Clone)") //i dont think this is good practice, TODO research a better way
        {
            TakeDamage();
            Debug.Log(gameObject.name + "hit with bullet");
        }

        if(col.gameObject.name == "Firewall(Clone)")
        {
            speed = 0.3f;
        }

        if(col.gameObject.name == "Wiper(Clone)")
        {
            Debug.Log(gameObject.name + "hit with wall");
            health = 1;
            TakeDamage();
        }

        return;
    }
}
