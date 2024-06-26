using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private GameObject malwareGO;
    [SerializeField]private TMP_Text pointsDisplay;
    [SerializeField]private TMP_Text playerNameDisplay;
    [SerializeField]private TMP_Text livesDisplay;
    [SerializeField] private GameObject turretGO;
    [SerializeField] private GameObject shopCanvas;

    private System.Random random = new System.Random();

    private float malwareSpeed = 2;
    private float malwareRadius = 0.5f;
    private float timeDelay = 1.5f;
    private float malwareHealth = 2;
    private int enemiesSpawnedSinceLastIncrease = 0;
    private int enemyStatIncreaseCount = 0;
    bool paused = false;
    bool firstLoop;

    GameObject turret;

    System.Random rand = new System.Random();

    void Start()
    {
        string playerName = Menu.GetPlayerName();
        Player player = new Player(playerName, pointsDisplay, livesDisplay);
        playerNameDisplay.text = playerName;
        StartCoroutine(SpawnMalware());
        turret = Instantiate(turretGO, new Vector2(-5.72f, 0), new Quaternion(0, 0, 0, 0));
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

    }

    public void Pause()
    {
        paused = true;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Malware");
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<MalwareScript>().Pause();
        }
        foreach(GameObject bullet in bullets)
        {
            bullet.GetComponent<BulletScript>().Pause();
        }
        turret.GetComponent<TurretScript>().Pause();
        StopAllCoroutines();

        shopCanvas.SetActive(true);
    }

    public void Resume()
    {
        paused = false;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Malware");
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<MalwareScript>().Resume();
        }
        foreach(GameObject bullet in bullets)
        {
            bullet.GetComponent<BulletScript>().Resume();
        }
        turret.GetComponent<TurretScript>().Resume();
        StartCoroutine(SpawnMalware());
        firstLoop = true;

        shopCanvas.SetActive(false);
    }

    IEnumerator SpawnMalware()
    {
        Constants.SceneNames sceneNameAsEnum;
        Enum.TryParse(SceneManager.GetActiveScene().name, out sceneNameAsEnum);
        //basically this is just a much better way of doing while(scenename = "gamescene")
        while(sceneNameAsEnum == Constants.SceneNames.GameScene)
        {
            if(firstLoop)
            {
                Debug.Log("first loop apparently");
                yield return new WaitForSeconds(timeDelay); //was having some problems with pausing causing oads of enemys to spawn
                firstLoop = false;
            }

            GameObject thisGO = Instantiate(malwareGO, new Vector3(Constants.ENEMY_SPAWN_DISTANCE, random.Next(-40, 40) / 10.0f, 0), new Quaternion(0, 0, 0, 0));
            MalwareScript malwareScript = thisGO.GetComponent<MalwareScript>();

            int randnum = rand.Next(Constants.CHANCE_OF_BUFFED_ENEMY); //0 - 10
            switch(randnum)
            {
                case 1:
                malwareScript.init(malwareSpeed * 2, malwareRadius, malwareHealth / 2); //fast
                break;
                case 2:
                malwareScript.init(malwareSpeed / 2f, malwareRadius * 1.5f, malwareHealth * 2);  //high hp
                break;
                default:
                malwareScript.init(malwareSpeed, malwareRadius, malwareHealth);
                break;
            }
           
            if(enemiesSpawnedSinceLastIncrease > Constants.NUM_OF_ENEMIES_TO_INCREASE_DIFFICULTY)
            {
                enemiesSpawnedSinceLastIncrease = 0;
                enemyStatIncreaseCount++;
                malwareSpeed += 0.25f;

                if(enemyStatIncreaseCount % Constants.INCREASE_HEALTH_EVERY_X_SPEED_INCREASES == 0)
                {
                    malwareHealth++;
                }

            }
            enemiesSpawnedSinceLastIncrease++;
            
            yield return new WaitForSeconds(timeDelay); //run the next iteration of this loop after timedelay seconds
        }

   
    }
}
