using UnityEngine;

public class Hacker : MalwareScript
{
    float counter = 0;
    bool paused;
    
    public void HackerInit(float speed, float health, Controller controller, Color color, Vector2 position)
    {
        base.init(speed, Constants.HACKER_RADIUS, health, controller, color);
        transform.position = position;
        paused = false;
        return;
    }

    void Update()
    {
        if(paused)
            return;

        if(counter <= 0)
            SpawnMalware();
        if(!controller.getPaused())
            counter -= Time.deltaTime;

        if(transform.position.x > Constants.HACKER_STOP_POINT) //move to the stopping point
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        else if(transform.position.x != Constants.HACKER_STOP_POINT)
        {
            transform.position = new Vector2(Constants.HACKER_STOP_POINT, transform.position.y);
        }
        return;
    }

    void SpawnMalware()
    {
        this.controller.SpawnMalware(transform.position, true, color);
        counter = Constants.HACKER_SPAWN_RATE;
        return;
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {

        if(col.gameObject.name == Constants.BULLET_G_O_NAME) 
        {
            TakeDamage();
        }

        if(col.gameObject.name == Constants.FIREWALL_G_O_NAME)
        {
            speed = Constants.ENEMY_SPEED_REGAIN_RATE;
        }

        if(col.gameObject.name == Constants.WIPER_G_O_NAME)
        {
            health = 1;
            TakeDamage();
        }

        return;
    }

    protected override void Disappear()
    {
        gameObject.SetActive(false);
        controller.setHackerEnabled(false);
        return;
    }

    public void setPaused(bool x)
    {
        paused = x;
        return;
    }
}
