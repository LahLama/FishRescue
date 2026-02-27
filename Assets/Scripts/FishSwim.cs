using UnityEngine;
namespace LahLama
{
    public class FishSwim : MonoBehaviour
    {
        Rigidbody2D rb;
        PolygonCollider2D coll;
        int directionSwim = 1;
        public float swimStrength = 10;
        void Awake()
        {
            rb = this.GetComponent<Rigidbody2D>();
            coll = this.GetComponent<PolygonCollider2D>();
        }

        void FixedUpdate()
        {
            rb.AddForce(new Vector2(swimStrength * directionSwim, 0));
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            directionSwim *= -1;
        }


    }
}
