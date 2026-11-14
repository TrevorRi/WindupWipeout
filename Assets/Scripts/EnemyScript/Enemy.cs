using System.Threading;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer enemyRenderer;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float timeBetweenShoot;
    [SerializeField] private float numBullets;

    private float countdown;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemyRenderer.isVisible)
        {
            countdown -= Time.fixedDeltaTime;
            if (countdown <= 0) ShootBullets();
        }
        else if (countdown < timeBetweenShoot) countdown = timeBetweenShoot;
    }


    // Methods for Enemies
    /// <summary>
    /// When called, the enemy's bullets will shoot out in its determined 
    /// behavior pattern.
    /// </summary>
    public abstract void ShootBullets();


}
