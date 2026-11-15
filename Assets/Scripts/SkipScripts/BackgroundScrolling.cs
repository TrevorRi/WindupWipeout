using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    [SerializeField] protected GameObject endPoint;
    [SerializeField] protected GameObject backgroundPiece;

    private static float speed = 5f;
    private  float resetPos;
    private SpriteRenderer backgroundRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backgroundRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.x <= -57)
        {
            transform.position = endPoint.transform.position;
            transform.Translate(-(-48), 0, 0);
        }

        transform.Translate(-(speed * Time.deltaTime), 0, 0);

    }
}
