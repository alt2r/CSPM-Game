using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player
{
    // Start is called before the first frame update
    private float health;
    public static Player instance;

    public Player()
    {
        if(instance == null)
        {
            instance = this;
            health = 5;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateHealth(float changeInHealth)
    {
        Debug.Log(health);
        health += changeInHealth;
        if(health <= 0)
        loseGame();
    }

    private void loseGame()
    {
        Debug.Log("dead");
    }
}
