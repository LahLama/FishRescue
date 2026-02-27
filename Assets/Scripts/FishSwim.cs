using UnityEngine;
namespace LahLama
{
    public class FishSwim : MonoBehaviour
    {
        Rigidbody2D rb;
        PolygonCollider2D coll;
        int directionSwim = 1;
        public float swimStrength = 0.2f;
        float heightChanger;
        void Awake()
        {
            rb = this.GetComponent<Rigidbody2D>();
            coll = this.GetComponent<PolygonCollider2D>();
            swimStrength = Random.Range(0.4f, 0.8f);
        }

        void FixedUpdate()
        {
            heightChanger = Random.Range(-0.6f, 0.6f);
            // Sets a constant speed immediately
            rb.linearVelocity = new Vector2(directionSwim * swimStrength, rb.linearVelocity.y);

        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            directionSwim *= -1;
            transform.localScale = new Vector3(directionSwim, transform.localScale.y, transform.localScale.z);
        }


    }
}
