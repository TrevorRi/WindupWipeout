using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    private static float speed = 10f;
    private static Vector2 resetPos = new Vector2(8.91f,0);
    private SpriteRenderer backgroundrenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backgroundrenderer = GetComponent<SpriteRenderer>();
        transform.position = resetPos; //Always starts at the resetPos
        backgroundrenderer.flipX = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <= -65)
        {
            backgroundrenderer.flipX = !backgroundrenderer.flipX; //Flips it
            transform.position = resetPos;
        }

        transform.Translate(-(speed * Time.deltaTime), 0, 0);

    }
}
