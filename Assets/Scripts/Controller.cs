using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private GameObject malwareGO;
    [SerializeField]private TMP_Text pointsDisplay;
    [SerializeField]private TMP_Text playerNameDisplay;
    [SerializeField]private TMP_Text livesDisplay;
    [SerializeField] private GameObject turretGO;
    [SerializeField] private GameObject shopCanvas;
    [SerializeField] private Button closeShop;

    private System.Random random = new System.Random();



    private float malwareSpeed = 2;
    private float malwareRadius = 0.5f;
    private float timeDelay = 1.5f;
    private float malwareHealth = 2;
    private int enemiesSpawnedSinceLastIncrease = 0;
    private int enemyStatIncreaseCount = 0;
    bool paused = false;
    bool firstLoop;

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
        return;
     }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
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
                SpawnMalware();
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
        firstLoop = true;

        shopCanvas.SetActive(false);
    }

    public void setMalwareAsInactive(MalwareScript mws)
    {
        activeMalware.Remove(mws);
        inactiveMalware.Add(mws);
    }

    void SpawnMalware()
    {
        MalwareScript malwareScript;
        Constants.SceneNames sceneNameAsEnum;
        Enum.TryParse(SceneManager.GetActiveScene().name, out sceneNameAsEnum);
        //basically this is just a much better way of doing while(scenename = "gamescene")
        while(sceneNameAsEnum == Constants.SceneNames.GameScene)
        {
            if(firstLoop)
            {
                counter = timeDelay;
                firstLoop = false;
                return;
            }

            if(inactiveMalware.Count > 0)
            {
                malwareScript = inactiveMalware[0];
                malwareScript.gameObject.transform.position = new Vector3(Constants.ENEMY_SPAWN_DISTANCE, random.Next(-40, 40) / 10.0f, 0);
                malwareScript.SetActive(true);
                inactiveMalware.RemoveAt(0);
            }
            else
            {   
                malwareScript = Instantiate(malwareGO, new Vector3(Constants.ENEMY_SPAWN_DISTANCE, random.Next(-40, 40) / 10.0f, 0), new Quaternion(0, 0, 0, 0)).GetComponent<MalwareScript>();
            }
            activeMalware.Add(malwareScript);

            int randnum = rand.Next(Constants.CHANCE_OF_BUFFED_ENEMY); //0 - 10
            switch(randnum)
            {
                case 1:
                malwareScript.init(malwareSpeed * Constants.FAST_ENEMY_SPEED_MULTIPLIER, malwareRadius, malwareHealth * Constants.FAST_ENEMY_HEALTH_MULTIPLIER, this); //fast
                break;
                case 2:
                malwareScript.init(malwareSpeed * Constants.STRONG_ENEMY_SPEED_MULTIPLIER, malwareRadius * Constants.STRONG_ENEMY_RADIUS_MODIFIER, malwareHealth * Constants.STRONG_ENEMY_HEALTH_MULTIPLIER, this);  //high hp
                break;
                default:
                malwareScript.init(malwareSpeed, malwareRadius, malwareHealth, this);
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
            enemiesSpawnedSinceLastIncrease++;
            
            counter = timeDelay;
            return;
        }

   
    }
}
