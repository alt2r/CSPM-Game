using UnityEngine;

public class BaseScript : MonoBehaviour
{
    void Start()
    {
        BoxCollider2D col = gameObject.AddComponent<BoxCollider2D>();
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        col.size = new Vector2(Constants.POINT_AT_WHICH_MALWARE_HAS_REACHED_BASE, 1);
        col.isTrigger = true;
        return;
    }
}
