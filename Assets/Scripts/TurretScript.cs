using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    // Start is called before the first frame update
    float momentum = 0;
    bool paused = false;
    [SerializeField]
    GameObject bulletGO;

    float shootCountdown = 0; //counts down to 0
    void Start()
    {
        //StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        if(paused)
        {
            return;
        }
        if(shootCountdown > 0)   //turret shoot cooldown
        shootCountdown -= Time.deltaTime;


        //movement
        if(Input.GetKey(KeyCode.DownArrow))
        {
            if(momentum > -15)
            momentum -= 5f * Time.deltaTime;
            else
            momentum = -10;
            if(momentum > 3.5f * Time.deltaTime)
            momentum -= 3.5f * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.UpArrow))
        {
            if(momentum < 15)
            momentum += 5f * Time.deltaTime;
            else
            momentum = 10;
            if(momentum < -3.5f * Time.deltaTime) //friction + movement
            momentum += 3.5f * Time.deltaTime;
            
        }
        else
        {
            if(momentum > 3.5f * Time.deltaTime)
            momentum -= 3.5f * Time.deltaTime; //friciton
            else if(momentum < -3.5f * Time.deltaTime)
            momentum += 3.5f * Time.deltaTime;
            else
            momentum = 0;
        }

        if(!(momentum > 0 && transform.position.y > 4.5f) && !(momentum < 0 && transform.position.y < -4.5f))
        {
            transform.Translate(new Vector2(0, momentum * Time.deltaTime));
        }
        else
        {
            if(transform.position.y > 4.5f)
            {
                transform.position = new Vector2(transform.position.x, 4.5f);
            }
            else if(transform.position.y < -4.5f)
            {
                transform.position = new Vector2(transform.position.x, -4.5f);
            }
            
            momentum = 0;
        }


        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
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


    //IEnumerator Shoot()
    void Shoot()
    {
        Player player = Player.GetInstance();
        
        if(shootCountdown <= 0)
        {
            shootCountdown = 1 / player.GetFireRate();
            GameObject thisBullet = Instantiate(bulletGO);
            thisBullet.transform.position = transform.position;
            if(player.GetUpgradesDict()[Constants.Upgrades.BURST_MODE] > 0)
            {
                StartCoroutine(BurstMode(player.GetUpgradesDict()[Constants.Upgrades.BURST_MODE]));
            }
        }
        return;

    }

    IEnumerator BurstMode(int level)
    {
        int shot = 0;
        bool firstRun = true;
        while(shot < level)
        {
            if(firstRun)
            {
                firstRun = false; //so that there arent 2 bullets on top of each other
                yield return new WaitForSeconds(Constants.BURST_MODE_TIME_BETWEEN_SHOTS);
            }
            shot++;
            GameObject thisBullet = Instantiate(bulletGO);
            thisBullet.transform.position = transform.position;
            yield return new WaitForSeconds(Constants.BURST_MODE_TIME_BETWEEN_SHOTS);
        }
    }
}
