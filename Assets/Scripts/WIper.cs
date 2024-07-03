using UnityEngine;

public class Wiper : MonoBehaviour
{
    void Start() //attach collision detection stuff
    {
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        BoxCollider2D col = gameObject.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
    }
    void Update() //move
    {
        transform.Translate(new Vector2(Constants.WIPER_MOVE_SPEED * Time.deltaTime, 0));
    }

    private void OnTriggerEnter2D(Collider2D col) //destroy self when we hit the off screen wall
    {
        if(col.name == Constants.RIGHT_WALL_NAME)
        {
            Destroy(gameObject);
        }
    }
}
