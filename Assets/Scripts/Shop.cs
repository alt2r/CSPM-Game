using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Shop : MonoBehaviour
{
    [SerializeField] Button increaseFireRateButton;
    [SerializeField] Button knockbackButton;
    [SerializeField] Button shotgunModeButton;
    [SerializeField] Button burstModeButton;

    TMP_Text increaseFireRateText;
    TMP_Text knockbackText;
    TMP_Text shotgunModeText;
    TMP_Text burstModeText;

    int increaseFireRateCost = 10;
    int knockbackCost = 10;
    int shotgunModeCost = 25;
    int burstModeCost = 25;


    //Player player;

    void Start()
    {
        increaseFireRateButton.onClick.AddListener(IncreaseFireRate);
        knockbackButton.onClick.AddListener(Knockback);
        shotgunModeButton.onClick.AddListener(ShotgunMode);
        burstModeButton.onClick.AddListener(BurstMode);

        increaseFireRateText = increaseFireRateButton.GetComponentInChildren<TMP_Text>();
        knockbackText = knockbackButton.GetComponentInChildren<TMP_Text>();
        shotgunModeText = shotgunModeButton.GetComponentInChildren<TMP_Text>();
        burstModeText = burstModeButton.GetComponentInChildren<TMP_Text>();

        increaseFireRateText.text = "Increase fire rate\nCost: " + increaseFireRateCost;
        knockbackText.text = "Knock back malware\nCost: " + knockbackCost;
        shotgunModeText.text = "Shotgun Mode\nCost: " + shotgunModeCost;
        burstModeText.text = "Burst Mode\nCost: " + burstModeCost;

    }

    private void IncreaseFireRate()
    {
        Player.GetInstance().SetFireRate(Player.GetInstance().GetFireRate() * 1.5f);
        increaseFireRateCost *= 2;
        increaseFireRateText.text = "";

        Player.GetInstance().increaseValueInUpgradesDict(Constants.Upgrades.FIRE_RATE);
    }

    private void Knockback()
    {
        Player.GetInstance().increaseValueInUpgradesDict(Constants.Upgrades.KNOCKBACK);
    }

    private void ShotgunMode()
    {
        Player.GetInstance().increaseValueInUpgradesDict(Constants.Upgrades.SHOTGUN);
    }

    private void BurstMode()
    {
        Player player = Player.GetInstance();
        player.increaseValueInUpgradesDict(Constants.Upgrades.BURST_MODE);
        player.SetFireRate(player.GetFireRate() * Constants.BURST_MODE_FIRE_RATE_MODIFIER);
    }
}


