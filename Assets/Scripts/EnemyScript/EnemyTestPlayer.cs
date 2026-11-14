using UnityEngine;

public class EnemyTestPlayer : MonoBehaviour
{
    [SerializeField] float health = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damageAmount)
    {
        if (health > 0) health -= damageAmount;
        if (health < 0) health = 0;

        Debug.Log(health);
    }
}
