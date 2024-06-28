using UnityEditor.ShaderGraph.Internal;

public class Constants
{
    public const float MINIMUM_RELATIVE_MALWARE_SIZE = 0.7f;
    public const float COLLIDER_SIZE_MULTIPLIER = 1;
    public const float ENEMY_SPAWN_DISTANCE = 12;
    public const float PLAYER_LIVES = 5;
    public const string POINTS_DISPLAY_TEXT = "points: ";
    public const string LIVES_DISPLAY_TEXT = "lives: ";
    public const string PLAYERS_FILE_NAME = "players.txt";
    public const float BULLET_SPEED = 10;
    public const int NUMBER_OF_LEADERBOARD_ENTRIES = 10;
    public const int NUM_OF_ENEMIES_TO_INCREASE_DIFFICULTY = 5;
    public const int CHANCE_OF_BUFFED_ENEMY = 10; // do 1/value you want so 0.1 = 10
    public const int INCREASE_HEALTH_EVERY_X_SPEED_INCREASES = 6;
    public enum SceneNames{GameScene, MenuScene, GameOverScene}
    public enum Upgrades{WEAPON, BARRIER, SCREENWIPE}
    public const float BURST_MODE_TIME_BETWEEN_SHOTS = 0.1f;
    public const float BURST_MODE_FIRE_RATE_MODIFIER = 0.8f;
    public const float SHOTGUN_FIRE_RATE_MODIFIER = 0.5f;
    public const float SHOTGUN_SPREAD = 4f; 

    public const string WEAPON_UPGRADE_TEXT = "Contingency planning";
    public const string WEAPON_UPGRADE_SMALL_TEXT = "Upgrades your weapon";

    public const string FIREWALL_TEXT = "Firewall";
    public const string FIREWALL_SMALL_TEXT = "A wall to block malware";
    public const string SCREEN_WIPE_TEXT = "Patch management";
    public const string SCREEN_WIPE_SMALL_TEXT = "Destroys all currently active malware";

    public const string COST_STRING = "Cost: ";

    public const int BARRIER_COST = 10;
    public const int WEAPON_UPGRADE_COST = 15;
    public const int SCREEN_WIPE_COST = 10;
    public const int UPGRADE_COST_INCREASE_MULTIPLIER = 2;
    public const float TURRET_SPAWN_POINT = -5.12f;
    public const float FIRE_RATE_INCREASE_MODIFIER = 1.33f;
    public const float FAST_ENEMY_SPEED_MULTIPLIER = 1.33f;
    public const float FAST_ENEMY_HEALTH_MULTIPLIER = 0.5f;
    public const float STRONG_ENEMY_HEALTH_MULTIPLIER = 2f;
    public const float STRONG_ENEMY_SPEED_MULTIPLIER = 0.5f;
    public const float STRONG_ENEMY_RADIUS_MODIFIER = 1.5f;
    public const float ENEMY_GRADUAL_SPEED_INCREASE = 0.25f;
    public const float ENEMY_DAMAGE = 1;
    public const float BULLET_DAMAGE = 1;
    public const string BULLET_TAG = "Bullet";

    public const float FIREWALL_1_POS = 5;
    public const float FIREWALL_2_POS = 5.75f;
    public const float FIREWALL_3_POS = 6.5f;

    public const float WIPER_MOVE_SPEED = 8;
    public const string SOLD_OUT_TEXT = "Sold out!";

}