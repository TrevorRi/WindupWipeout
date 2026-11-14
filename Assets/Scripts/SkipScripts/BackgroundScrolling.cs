using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    private static float speed = 10f;
    private static Vector2 resetPos = new Vector2(13,0);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <= -32)
        {
            transform.position = resetPos;
        }

        transform.Translate(-(speed * Time.deltaTime), 0, 0);

    }
}
