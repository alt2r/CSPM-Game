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

    public enum Upgrades{BURST_MODE, SHOTGUN, FIRE_RATE, KNOCKBACK}

    public const float BURST_MODE_TIME_BETWEEN_SHOTS = 0.025f;
    public const float BURST_MODE_FIRE_RATE_MODIFIER = 0.67f;
}