using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    GameObject currentBullet; //declared as global to save the shoot() method from having to allocate memory. shoot() was causing a lot of lag
    float momentum = 0;
    bool paused = false;
    [SerializeField]
    GameObject bulletGO;

    List<GameObject> inactiveBulletList = new List<GameObject>();

    Player player;

    float shootCountdown = 0; //counts down to 0

    float burstCooldown = 0;
    int burstShotsFired;
    void Start()
    {
        player = Player.GetInstance();
    }

    void Update()
    {
        if(paused)
        {
            return;
        }
        if(shootCountdown > 0)   //turret shoot cooldown
        shootCountdown -= Time.deltaTime;

        if(burstCooldown > 0)
        {
            burstCooldown -= Time.deltaTime;
            if(burstCooldown <= 0)
            {
                Shoot();
            }
        
        }


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
            if(shootCountdown <= 0)
            {
                Shoot();
            }
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

    public void addToInactiveBulletList(GameObject b)
    {
        inactiveBulletList.Add(b);
    }
    void Shoot()
    {
        shootCountdown = 1 / player.GetFireRate();
        if(player.GetUpgradesDict()[Constants.Upgrades.WEAPON] > 1) //shotugn enables at level 2
        {
            shotgunShoot();
            
        }
        else
        {
            //major optimisation tried to reuse bullets instead of creating new ones
            if(inactiveBulletList.Count > 0)
            {
                currentBullet = inactiveBulletList[0];
                inactiveBulletList.RemoveAt(0);
                currentBullet.SetActive(true);
            }
            else
            {
                currentBullet = Instantiate(bulletGO);
                currentBullet.GetComponent<BulletScript>().SetReferenceToTurretScript(this);
            } 
        }



        currentBullet.transform.position = transform.position;
        if(player.GetUpgradesDict()[Constants.Upgrades.WEAPON] == 3 && burstShotsFired < 1)
        {
            burstCooldown = Constants.BURST_MODE_TIME_BETWEEN_SHOTS;
            burstShotsFired++;
        }
        else
        {
            burstShotsFired = 0;
        }
        
        return;

    }

    void shotgunShoot()
    {
        List<GameObject> bullets = new List<GameObject>();
        for (int i = 0; i < 3; i++)
        {
            BulletScript thisBulletScript;
            if(inactiveBulletList.Count > 0)
            {
                currentBullet = inactiveBulletList[0];
                inactiveBulletList.RemoveAt(0);
                currentBullet.SetActive(true);

                thisBulletScript = currentBullet.GetComponent<BulletScript>();
            }
            else
            {
                currentBullet = Instantiate(bulletGO);
                thisBulletScript = currentBullet.GetComponent<BulletScript>();
                thisBulletScript.SetReferenceToTurretScript(this);
            }
            currentBullet.transform.position = transform.position;
            thisBulletScript.setYMovement((i - 1) * Constants.SHOTGUN_SPREAD);
            bullets.Add(currentBullet);
            }
    }

}
