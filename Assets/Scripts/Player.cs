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
    enum Upgrades
    {
        BURST_MODE,
        SHOTGUN,
        FULL_AUTO,
        KNOCKBACK
    }

    Dictionary<Upgrades, bool> hasUpgrade = new Dictionary<Upgrades, bool>(){
        {Upgrades.BURST_MODE, false},
        {Upgrades.SHOTGUN, false},
        {Upgrades.FULL_AUTO, false},
        {Upgrades.KNOCKBACK, false}
    };
    // Start is called before the first frame update
    private float lives = 0;
    public static Player instance;
    private TMP_Text pointsDisplay;
    private TMP_Text livesDisplay;
    public Player(TMP_Text pointsDisplay, TMP_Text livesDisplay)
    {
        lives = 5;
        if(instance == null)
        {
            instance = this;
            
        }
        this.pointsDisplay = pointsDisplay;
        this.livesDisplay = livesDisplay;
        updatePointsDisplay();
        updateLivesDisplay();
    }

    public void updatePointsDisplay()
    {
        pointsDisplay.text = Constants.POINTS_DISPLAY_TEXT + spendablePoints;
    }

    public void updateLivesDisplay()
    {
        livesDisplay.text = Constants.LIVES_DISPLAY_TEXT + lives;
    }

    // Update is called once per frame
    public void incrementScores()
    {
        score++;
        spendablePoints++;
        updatePointsDisplay();
        return;
    }

    public int getScore()
    {
        return score;
    }

    public void updateHealth(float changeInHealth)
    {
        lives += changeInHealth;
        if(lives <= 0)
        {
        loseGame();
        return;
        }
        updateLivesDisplay();
        return;
        
    }

    private void loseGame()
    {
        SceneManager.LoadSceneAsync("GameOverScene", LoadSceneMode.Single);
        instance = null; //remove this player
    }
}
