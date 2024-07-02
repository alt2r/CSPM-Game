using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Linq;
using System.Collections;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private GameObject malwareGO;
    [SerializeField] private GameObject hackerGO;
    [SerializeField]private TMP_Text pointsDisplay;
    [SerializeField]private TMP_Text playerNameDisplay;
    [SerializeField]private TMP_Text livesDisplay;
    [SerializeField] private GameObject turretGO;
    [SerializeField] private GameObject shopCanvas;
    [SerializeField] private Button closeShop;
    [SerializeField] private Button openShop;
    [SerializeField] private GameObject tutorialCanvas;
    [SerializeField] private Button closeTutorialButton;
    [SerializeField] private GameObject tutorialImage;
    [SerializeField] private GameObject destroyAllIncomingMalware;




    private float malwareSpeed = 2;
    private float malwareRadius = 0.5f;
    private float timeDelay = 1.5f;
    private float malwareHealth = 2;
    private int enemiesSpawnedSinceLastIncrease = 0;
    private int enemyStatIncreaseCount = 0;
    bool paused = true;
    Hacker hacker;
    bool hackerEnabled = false;

    private float counter = 0;

    private List<MalwareScript> activeMalware = new List<MalwareScript>();
    private List<MalwareScript> inactiveMalware = new List<MalwareScript>();

    GameObject turret;

    System.Random rand = new System.Random();

    void Start()
    {
        string playerName = Menu.GetPlayerName();
        Player player = new Player(playerName, pointsDisplay, livesDisplay);
        playerNameDisplay.text = playerName;
        turret = Instantiate(turretGO, new Vector2(Constants.TURRET_SPAWN_POINT, 0), new Quaternion(0, 0, 0, 0));
        closeShop.onClick.AddListener(Resume);
        openShop.onClick.AddListener(Pause);
        closeTutorialButton.onClick.AddListener(CloseTutorial);
        hacker = Instantiate(hackerGO).GetComponent<Hacker>();
        hacker.SetActive(false);
        return;
     }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Escape))
        {
            if(paused)
            {
                Resume();
            }
            else if(!paused)
            {
                Pause();
            }
        }


        //spawn malware
        if(!paused)
        {
            counter -= Time.deltaTime;
            if(counter <= 0)
            {
                SpawnMalware(new Vector3(Constants.ENEMY_SPAWN_DISTANCE, rand.Next(-40, 40) / 10.0f, 0), false, new Color(rand.Next(3) * 0.45f, rand.Next(3) * 0.45f, rand.Next(3) * 0.45f));
            }
        }

        //move malware (so that each malware doesnt need an update)
        if(!paused)
        {
            foreach(MalwareScript malware in activeMalware)
            {
                malware.Move();
            }
        }
    }

    IEnumerator FlashTextOnScreen()
    {
        for (int i = 0; i < 6; i++)
        {
            destroyAllIncomingMalware.SetActive(!destroyAllIncomingMalware.activeSelf);
            yield return new WaitForSeconds(1.5f);
        }
        StopAllCoroutines(); //coroutines are not very efficient so want to make sure this one stops
    }

    private void CloseTutorial()
    {
        tutorialCanvas.SetActive(false);
        tutorialImage.SetActive(false);
        StartCoroutine(FlashTextOnScreen());
        Resume();
    }

    public void setHackerEnabled(bool x)
    {
        hackerEnabled = x;
    }

    public bool getPaused()
    {
        return paused;
    }

    public void Pause()
    {
        paused = true;
        GameObject[] bullets = GameObject.FindGameObjectsWithTag(Constants.BULLET_TAG);
        foreach(GameObject bullet in bullets)
        {
            bullet.GetComponent<BulletScript>().Pause();
        }
        turret.GetComponent<TurretScript>().Pause();

        shopCanvas.SetActive(true);
        hacker.setPaused(true);
    }

    public void Resume()
    {
        paused = false;
        GameObject[] bullets = GameObject.FindGameObjectsWithTag(Constants.BULLET_TAG);
        foreach(GameObject bullet in bullets)
        {
            bullet.GetComponent<BulletScript>().Resume();
        }
        turret.GetComponent<TurretScript>().Resume();

        shopCanvas.SetActive(false);
        hacker.setPaused(false);
    }

    public void setMalwareAsInactive(MalwareScript mws)
    {
        activeMalware.Remove(mws);
        inactiveMalware.Add(mws);
    }

    public void SpawnMalware(Vector3 spawnPos, bool spawnedByHacker, Color color)
    {
        MalwareScript malwareScript = null;
        Constants.SceneNames sceneNameAsEnum;
        Enum.TryParse(SceneManager.GetActiveScene().name, out sceneNameAsEnum);
        //basically this is just a much better way of doing while(scenename = "gamescene")
        while(sceneNameAsEnum == Constants.SceneNames.GameScene)
        {
            int randnum;
            if(!spawnedByHacker)
            {
                randnum = rand.Next(Constants.CHANCE_OF_BUFFED_ENEMY);
            }
            else
            {
                randnum = 50; //number with no power ups
            }

            if(inactiveMalware.Count > 1 && (randnum != 5 || hackerEnabled))  //5 is the number for a hacker
            { // > 1 in case this script runs twice in one frame which causes the enemies vanish inexplicably
            //a hacker could spawn an enemy on the same frame as a regular enemy spawns
                malwareScript = inactiveMalware[0];
                malwareScript.gameObject.transform.position = spawnPos;
                malwareScript.SetActive(true);
                inactiveMalware.RemoveAt(0);
                activeMalware.Add(malwareScript);
            }
            else if (randnum != 5 || hackerEnabled)
            {
                malwareScript = Instantiate(malwareGO, spawnPos, new Quaternion(0, 0, 0, 0)).GetComponent<MalwareScript>();
                activeMalware.Add(malwareScript);
                
            }

            switch(randnum)
            {
                case 1:
                case 2: //email
                malwareScript.init(malwareSpeed * Constants.FAST_ENEMY_SPEED_MULTIPLIER, malwareRadius, malwareHealth * Constants.FAST_ENEMY_HEALTH_MULTIPLIER, this, color); //fast
                malwareScript.EnableEmail();
                break;
                case 3:
                case 4: //dollar
                malwareScript.init(malwareSpeed * Constants.STRONG_ENEMY_SPEED_MULTIPLIER, malwareRadius * Constants.STRONG_ENEMY_RADIUS_MODIFIER, malwareHealth * Constants.STRONG_ENEMY_HEALTH_MULTIPLIER, this, color);  //high hp
                malwareScript.EnableDollar();
                break;
                case 5: 
                if(!hackerEnabled) 
                {
                    hacker.SetActive(true);
                    hackerEnabled = true;
                    hacker.HackerInit(Constants.HACKER_SPEED, Constants.HACKER_HEALTH, this, color, spawnPos);
                }
                else
                malwareScript.init(malwareSpeed, malwareRadius, malwareHealth, this, color);
                break;
                case 50: //enemies spawned by hacker have this number set to 50
                malwareScript.init(malwareSpeed, malwareRadius, malwareHealth/2, this, color); //reduce the hp of enemies spawned by hackers
                break; 
                default:
                malwareScript.init(malwareSpeed, malwareRadius, malwareHealth, this, color);
                break;
            }
           
            if(enemiesSpawnedSinceLastIncrease > Constants.NUM_OF_ENEMIES_TO_INCREASE_DIFFICULTY)
            {
                enemiesSpawnedSinceLastIncrease = 0;
                enemyStatIncreaseCount++;
                malwareSpeed += Constants.ENEMY_GRADUAL_SPEED_INCREASE;

                if(enemyStatIncreaseCount % Constants.INCREASE_HEALTH_EVERY_X_SPEED_INCREASES == 0)
                {
                    malwareHealth++;
                }

            }

            if(!spawnedByHacker)
            enemiesSpawnedSinceLastIncrease++;
            
            counter = timeDelay;
            return;
        }

   
    }
}
