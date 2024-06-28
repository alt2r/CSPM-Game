using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrade
{
    TMP_Text bigText;
    TMP_Text smallText;
    TMP_Text costText;
    int cost;
    Button button;
    Constants.Upgrades thisUpgrade;
    
    public Upgrade(Button button, int cost, Constants.Upgrades thisUpgrade, string bigTextString, string smallTextString)
    {
        this.cost = cost;
        this.thisUpgrade = thisUpgrade;
        foreach(TMP_Text text in button.GetComponentsInChildren<TMP_Text>())
        {
            switch(text.name)
            {
                case "Small":
                smallText = text;
                break;
                case "Big":
                bigText = text;
                break;
                case "Cost":
                costText = text;
                break;
                default:
                break;
            }
        }
        smallText.text = smallTextString;
        bigText.text = bigTextString;
        costText.text = Constants.COST_STRING + cost;

    this.button = button;
    }

    public void DoUpgrade(Player player)
    {
        player.spendPoints(cost);
        cost *= Constants.UPGRADE_COST_INCREASE_MULTIPLIER;
        player.increaseValueInUpgradesDict(thisUpgrade);
        costText.text = Constants.COST_STRING + cost;

    }

    public void refreshButton(Player player)
    {
        if(player.getSpendablePoints() < cost)
        {
            button.interactable = false;
        }
        else if(player.GetUpgradesDict()[thisUpgrade] > 2)
        {
            button.interactable = false;
            costText.text = Constants.SOLD_OUT_TEXT;
        }
        else
        {
            button.interactable = true;
        }
        return;
    }

}