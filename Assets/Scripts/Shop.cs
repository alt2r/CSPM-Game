using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.Mathematics;
public class Shop : MonoBehaviour
{
    [SerializeField] Button weaponUpgradeButton;
    [SerializeField] Button barrierUpgradeButton;
    [SerializeField] Button screenWipeButton;
    [SerializeField] Button mainMenuButton;
    [SerializeField] GameObject firewallGO;
    [SerializeField] GameObject wiperGO;

    Player player;
    Upgrade weapon, barrier, screenwipe;
    bool wiper = false;

    void Awake()
    {
        weaponUpgradeButton.onClick.AddListener(WeaponUpgrade);
        barrierUpgradeButton.onClick.AddListener(BarrierUpgrade);
        screenWipeButton.onClick.AddListener(ScreenWipe);

        mainMenuButton.onClick.AddListener(backToMenu);

        weapon = new Upgrade(weaponUpgradeButton, Constants.WEAPON_UPGRADE_COST, Constants.Upgrades.WEAPON, "planning", "weapon upgrades");
        barrier = new Upgrade(barrierUpgradeButton, Constants.BARRIER_COST, Constants.Upgrades.BARRIER, "firewall", "slows down enemies");
        screenwipe = new Upgrade(screenWipeButton, Constants.SCREEN_WIPE_COST, Constants.Upgrades.SCREENWIPE, "system update", "damages all malware on screen");

    }

    void OnEnable()
    {
        player = Player.GetInstance();
        //int points = player.getSpendablePoints();
        weapon.refreshButton(player);
        barrier.refreshButton(player);
        screenwipe.refreshButton(player);
    }

    private void WeaponUpgrade()
    {
        weapon.DoUpgrade(player);
        switch (player.GetUpgradesDict()[Constants.Upgrades.WEAPON])
        {
            case 1:
            player.SetFireRate(player.GetFireRate() * Constants.FIRE_RATE_INCREASE_MODIFIER);
            break;
            case 2:
            player.SetFireRate(player.GetFireRate() * Constants.SHOTGUN_FIRE_RATE_MODIFIER);
            break;
            case 3:
            player.SetFireRate(player.GetFireRate() * Constants.BURST_MODE_FIRE_RATE_MODIFIER);
            break;
            default:
            break;
        }


        OnEnable();
    }

    private void BarrierUpgrade()
    {
        barrier.DoUpgrade(player);
        switch (player.GetUpgradesDict()[Constants.Upgrades.BARRIER])
        {
            case 1:
            Instantiate(firewallGO, new Vector2(Constants.FIREWALL_1_POS, 0), new Quaternion(0,0,0,0));
            break;
            case 2:
            Instantiate(firewallGO, new Vector2(Constants.FIREWALL_2_POS, 0), new Quaternion(0,0,0,0));
            break;
            case 3:
            Instantiate(firewallGO, new Vector2(Constants.FIREWALL_3_POS, 0), new Quaternion(0,0,0,0));
            break;
            default:
            break;
        }

        OnEnable();
    }

    private void ScreenWipe()
    {
        screenwipe.DoUpgrade(player);
        wiper = true;
        OnEnable();
    }

    void OnDisable()
    {
        if(wiper)
        {
            wiper = false;
            Instantiate(wiperGO, new Vector2(-7f, 0), new Quaternion(0, 0, 0, 0));
        }
    }

    public void backToMenu()
    {
        SceneManager.LoadSceneAsync(Constants.SceneNames.MenuScene.ToString(), LoadSceneMode.Single);
    }
}


