using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

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
    private float lives;
    private static Player instance;
    private TMP_Text coinsDisplay;
    private TMP_Text livesDisplay;
    private TMP_Text scoreDisplay;
    private string playerName;
    private bool easyMode;
    public Player(string playerName, TMP_Text coinsDisplay, TMP_Text livesDisplay, TMP_Text scoreDisplay)
    {
        lives = Constants.PLAYER_LIVES;
        fireRate = Constants.PLAYER_BASE_FIRE_RATE;
        instance = this;
        this.coinsDisplay = coinsDisplay;
        this.livesDisplay = livesDisplay;
        this.playerName = playerName;
        this.scoreDisplay = scoreDisplay;
        UpdatePointsDisplay();
        UpdateLivesDisplay();
        return;
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
        return;
    }

    public void UpdatePointsDisplay()
    {
        coinsDisplay.text = Constants.COINS_DISPLAY_TEXT + spendablePoints;
        if(easyMode)
        scoreDisplay.text = Constants.SCORE_DISPLAY_TEXT + score;
        else
        scoreDisplay.text = Constants.SCORE_DISPLAY_TEXT + score * 2;
        return;
    }

    public void UpdateLivesDisplay()
    {
        livesDisplay.text = Constants.LIVES_DISPLAY_TEXT + lives;
        return;
    }

    public void IncrementScores()
    {
        score++;
        spendablePoints++;
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
        return;
    }

    private void LoseGame()
    {
        SceneManager.LoadSceneAsync(Constants.SceneNames.GameOverScene.ToString(), LoadSceneMode.Single);
        return;
    }

    public int getSpendablePoints()
    {
        return spendablePoints;
    }

    public void spendPoints(int spent)
    {
        spendablePoints -= spent;
        UpdatePointsDisplay();
        return;
    }

    public void setEasyMode(bool easymode)
    {
        easyMode = easymode;
        return;
    }

    public bool getEasyMode()
    {
        return easyMode;
    }
}
