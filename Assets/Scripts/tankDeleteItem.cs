using UnityEngine;

public class tankDeleteItem : MonoBehaviour
{
    // public bool showBound = true;
    // void FixedUpdate()
    // {
    //     this.GetComponent<SpriteRenderer>().enabled = showBound;
    // }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "fish")
            Destroy(collision.gameObject);
    }
}
