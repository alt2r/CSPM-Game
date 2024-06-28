using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using TMPro;
using Unity.VisualScripting;
public class Player
{
    int score;
    int spendablePoints;
    float fireRate;

    Dictionary<Constants.Upgrades, int> upgradeLevels = new Dictionary<Constants.Upgrades, int>(){
        {Constants.Upgrades.WEAPON, 0},
        {Constants.Upgrades.BARRIER, 0},
        {Constants.Upgrades.SCREENWIPE, 0},
    };
    // Start is called before the first frame update
    private float lives = 0;
    private static Player instance;
    private TMP_Text pointsDisplay;
    private TMP_Text livesDisplay;

    private string playerName;
    public Player(string playerName, TMP_Text pointsDisplay, TMP_Text livesDisplay)
    {
        lives = 5;
        fireRate = 4;
        instance = this;
        this.pointsDisplay = pointsDisplay;
        this.livesDisplay = livesDisplay;
        this.playerName = playerName;
        UpdatePointsDisplay();
        UpdateLivesDisplay();
    }

    public static Player GetInstance()
    {
        return instance;
    }

    public string GetPlayerName()
    {
        return playerName;
    }
    public float GetFireRate()
    {
        return fireRate;
    }
    public void SetFireRate(float fr)
    {
        fireRate = fr;
    }

    public void UpdatePointsDisplay()
    {
        pointsDisplay.text = Constants.POINTS_DISPLAY_TEXT + spendablePoints;
    }

    public void UpdateLivesDisplay()
    {
        livesDisplay.text = Constants.LIVES_DISPLAY_TEXT + lives;
    }

    // Update is called once per frame
    public void IncrementScores()
    {
        score++; // += 100;
        spendablePoints++; // += 100;
        UpdatePointsDisplay();
        return;
    }

    public int GetScore()
    {
        return score;
    }

    public void UpdateHealth(float changeInHealth)
    {
        lives -= changeInHealth;
        if(lives <= 0)
        {
        LoseGame();
        return;
        }
        UpdateLivesDisplay();
        return;
        
    }

    public Dictionary<Constants.Upgrades, int> GetUpgradesDict()
    {
        return upgradeLevels;
    }

    public void increaseValueInUpgradesDict(Constants.Upgrades upgradeToLevelUp)
    {
        upgradeLevels[upgradeToLevelUp]++;
    }

    private void LoseGame()
    {
        SceneManager.LoadSceneAsync("GameOverScene", LoadSceneMode.Single);
    }

    public int getSpendablePoints()
    {
        return spendablePoints;
    }

    public void spendPoints(int spent)
    {
        spendablePoints -= spent;
        UpdatePointsDisplay();
    }
}
