using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneInit
{
    string playerName;
    GameObject controller;
    public GameSceneInit(string playerName) //this class only exists so i dont need to have loads of static stuff
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        controller.GetComponent<ControllerScript>().init(playerName);
    }
}
