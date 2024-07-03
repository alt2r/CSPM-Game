using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public void Init()
    {
        player = Player.GetInstance();
        weaponUpgradeButton.onClick.AddListener(WeaponUpgrade);
        barrierUpgradeButton.onClick.AddListener(BarrierUpgrade);
        screenWipeButton.onClick.AddListener(ScreenWipe);
        mainMenuButton.onClick.AddListener(backToMenu);
        weapon = new Upgrade(weaponUpgradeButton, Constants.WEAPON_UPGRADE_COST, Constants.Upgrades.WEAPON, Constants.WEAPON_UPGRADE_TEXT, Constants.WEAPON_UPGRADE_SMALL_TEXT);
        barrier = new Upgrade(barrierUpgradeButton, Constants.BARRIER_COST, Constants.Upgrades.BARRIER, Constants.FIREWALL_TEXT, Constants.FIREWALL_SMALL_TEXT);
        screenwipe = new Upgrade(screenWipeButton, Constants.SCREEN_WIPE_COST, Constants.Upgrades.SCREENWIPE, Constants.SCREEN_WIPE_TEXT, Constants.SCREEN_WIPE_SMALL_TEXT);
        return;
    }

    void OnEnable()
    {
        player = Player.GetInstance();
        weapon.refreshButton(player);
        barrier.refreshButton(player);
        screenwipe.refreshButton(player);
        return;
    }

    public int getLowestCostForUpgrade()
    {
        int lowestCost;
        lowestCost = weapon.getCost();
        if(barrier.getCost() < lowestCost)
        lowestCost = barrier.getCost();
        if(screenwipe.getCost() < lowestCost)
        lowestCost = screenwipe.getCost();
        return lowestCost;
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
        return;
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
        return;
    }

    private void ScreenWipe()
    {
        screenwipe.DoUpgrade(player);
        wiper = true; //boolean to specify that we want to spawn the screen wiper when the shop is closed
        OnEnable();
        return;
    }

    void OnDisable() //instantiate the screen wiper when the shop is closed if it was bought
    {
        if(wiper)
        {
            wiper = false;
            Instantiate(wiperGO, new Vector2(Constants.SCREEN_WIPE_SPAWN_POINT, 0), new Quaternion(0, 0, 0, 0));
        }
        return;
    }

    public void backToMenu()
    {
        SceneManager.LoadSceneAsync(Constants.SceneNames.MenuScene.ToString(), LoadSceneMode.Single);
        return;
    }
}


