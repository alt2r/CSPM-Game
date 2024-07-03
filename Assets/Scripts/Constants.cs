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
    public const int NUMBER_OF_LEADERBOARD_ENTRIES = 20;
    public const int NUM_OF_ENEMIES_TO_INCREASE_DIFFICULTY = 4;
    public const int CHANCE_OF_BUFFED_ENEMY = 10; // do (1/value you want) + 1 so 0.1 = 11
    public const int INCREASE_HEALTH_EVERY_X_SPEED_INCREASES = 6;
    public enum SceneNames{GameScene, MenuScene, GameOverScene}
    public enum Upgrades{WEAPON, BARRIER, SCREENWIPE}
    public const float BURST_MODE_TIME_BETWEEN_SHOTS = 0.1f;
    public const float BURST_MODE_FIRE_RATE_MODIFIER = 0.8f;
    public const float SHOTGUN_FIRE_RATE_MODIFIER = 0.65f;
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
    public const float TURRET_SPAWN_POINT = -5.09f;
    public const float FIRE_RATE_INCREASE_MODIFIER = 1.33f;
    public const float FAST_ENEMY_SPEED_MULTIPLIER = 1.33f;
    public const float FAST_ENEMY_HEALTH_MULTIPLIER = 0.5f;
    public const float STRONG_ENEMY_HEALTH_MULTIPLIER = 2f;
    public const float STRONG_ENEMY_SPEED_MULTIPLIER = 0.5f;
    public const float STRONG_ENEMY_RADIUS_MODIFIER = 1.5f;
    public const float ENEMY_GRADUAL_SPEED_INCREASE = 0.35f;
    public const float ENEMY_DAMAGE = 1;
    public const float BULLET_DAMAGE = 1;
    public const string BULLET_TAG = "Bullet";

    public const float FIREWALL_1_POS = 5;
    public const float FIREWALL_2_POS = 5.75f;
    public const float FIREWALL_3_POS = 6.5f;

    public const float WIPER_MOVE_SPEED = 8;
    public const string SOLD_OUT_TEXT = "Sold out!";

    public const float HACKER_SPEED = 1;
    public const float HACKER_RADIUS = 0.875f;
    public const float HACKER_STOP_POINT = 7.5f;
    public const float HACKER_SPAWN_RATE = 3f;
    public const float HACKER_HEALTH = 3;

public const string YOU_DIED_TEXT = "You Died!\nName: ";
public const string SCORE_TEXT = "\nScore: ";
public const string SCORES_FILE_PATH = "players.txt";
public const string FIREWALL_G_O_NAME = "Firewall(Clone)";
public const string WIPER_G_O_NAME = "Wiper(Clone)";
public const string BULLET_G_O_NAME = "Bullet(Clone)";
public const string LEFT_WALL_NAME = "leftWall";
public const float FIREWALL_SPEED_MODIFIER = 0.3f;
public const float ENEMY_SPEED_REGAIN_RATE = 2;
public const float SCREEN_WIPE_SPAWN_POINT = -6.1f;
public const float PLAYER_MOVEMENT_BOUNDRIES = 4.5f;

public const float PLAYER_MOVEMENT_TOP_SPEED = 15;
public const float PLAYER_MOVEMENT_INITIAL_SPEED = 10;
public const float PLAYER_MOVEMENT_ACCELERATION = 5;
public const float PLAYER_MOVEMENT_FRICTION = 3.5f;
public const int SHOTGUN_BULLETS_FIRED = 3; 

public const string SMALL_TEXT_NAME = "Small";
public const string BIG_TEXT_NAME = "Big";
public const string COST_TEXT_NAME = "Cost";
public const string RIGHT_WALL_NAME = "rightWall";
public const float BULLET_SPAWN_OFFSET = 0.35f;
public const float INITIAL_EASY_MODE_SPAWN_RATE_MODIFIER = 0.4f;
public const float EASY_MODE_DIFFICULTY_INCREASE_PER_ENEMY = 0.08f; 



}