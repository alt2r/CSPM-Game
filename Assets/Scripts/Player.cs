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
    private TMP_Text pointsDisplay;
    private TMP_Text livesDisplay;
    private string playerName;
    private bool easyMode;
    public Player(string playerName, TMP_Text pointsDisplay, TMP_Text livesDisplay)
    {
        lives = Constants.PLAYER_LIVES;
        fireRate = Constants.PLAYER_BASE_FIRE_RATE;
        instance = this;
        this.pointsDisplay = pointsDisplay;
        this.livesDisplay = livesDisplay;
        this.playerName = playerName;
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
        pointsDisplay.text = Constants.POINTS_DISPLAY_TEXT + spendablePoints;
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
