using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rBody;
    [SerializeField] private float speed = 10;
    [SerializeField] private float damagePerHit = 5;

    private Vector2 velocity;
    public Vector2 StartVelocity { set { velocity = value; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (velocity == null) velocity = new Vector2(1, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos += velocity.normalized * speed * Time.fixedDeltaTime;
        rBody.MovePosition(pos);
    }

    /// <summary>
    /// Handles when the bullet collides with another object, to determine if it should
    /// harm the player before despawning the bullet
    /// </summary>
    /// <param name="collision">Object that was collided with</param>
    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player") collision.gameObject.GetComponent<EnemyTestPlayer>().TakeDamage(damagePerHit);

    //    if (collision.gameObject.tag != "Bullet") Destroy(this.gameObject);
    //    Debug.Log("bullet destroyed");
    //}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Player") collision.gameObject.GetComponent<EnemyTestPlayer>().TakeDamage(damagePerHit);

        if (collision.gameObject.tag != "Bullet")
        {
            Destroy(this.gameObject);
            Debug.Log("bullet destroyed");
        }
    }




    // Skipping methods

    public void SkipForward()
    {
        //transform.position += transform.position / 2;
        Vector2 pos = transform.position;
        pos += velocity.normalized * speed / 2.0f;
        rBody.MovePosition(pos);
    }

    public void Rewind()
    {
        //transform.position -= transform.position / 2;
        Vector2 pos = transform.position;
        pos -= velocity.normalized * speed / 2.0f;
        rBody.MovePosition(pos);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="yOffset">The amout to make y drop down by (a positive value)</param>
    public void NeedleDrop(float yOffset)
    {
        //Vector3 newPos = b.transform.position;
        //newPos.y -= yOffset;
        //b.transform.position = newPos;
        rBody.MovePosition(
                            new Vector3(0,
                                        -(yOffset),
                                        0)
                           );
    }
}
