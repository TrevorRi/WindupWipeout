using UnityEngine;
using System.Collections.Generic;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer enemyRenderer;
    [SerializeField] protected Bullet bulletPrefab;
    [SerializeField] protected float timeBetweenShoot;
    [SerializeField] protected float numBullets;

    List<Bullet> currentBullets;   // All bullets from this enemy that are currently in the scene
    //public List<Bullet> CurrentBullets { get; }
   
    protected float countdown;


    void Awake()
    {
        currentBullets = new List<Bullet>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemyRenderer.isVisible)
        {
            countdown -= Time.fixedDeltaTime;
            if (countdown <= 0)
            {
                ShootBullets();
                countdown = timeBetweenShoot;
            }
        }
        else if (countdown < timeBetweenShoot) countdown = timeBetweenShoot;
    }


    // Required methods for Enemies
    /// <summary>
    /// When called, the enemy's bullets will shoot out in its determined 
    /// behavior pattern.
    /// </summary>
    public abstract void ShootBullets();




    // Methods for possible bullet behaviors
    // Agents will use these in their ShootBullets() method
    /// <summary>
    /// THIS METHOD IS HOW ALL BULLETS WILL BE INSTANTIATED
    /// </summary>
    /// <param name="velocity">The start velocity of the bullet</param>
    /// <param name="bulletPrefab">Prefab of the bullet to spawn</param>
    protected void Shoot(Vector2 velocity, Bullet bulletPrefab)
    {
        Bullet newBullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity, transform);
        currentBullets.Add(newBullet);
        newBullet.StartVelocity = velocity;
    }

    protected void ShootOneAtPoint(Vector2 point, Bullet bulletPrefab)
    {
        Vector2 enemyPosition = transform.position;
        Shoot(point - enemyPosition, bulletPrefab);
    }

    protected void ShootOneAtPlayer(GameObject player, Bullet bulletPrefab)
    {
        ShootOneAtPoint(player.transform.position, bulletPrefab);
    }



    // Classes for skipping

    public void SkipForward()
    {
        foreach (Bullet b in currentBullets)
        {
            b.SkipForward();
        }
    }

    public void Rewind()
    {
        foreach (Bullet b in currentBullets)
        {
            b.SkipForward();
        }
    }

    public void NeedleDrop(float yOffset)
    {
        foreach (Bullet b in currentBullets)
        {
            //Vector3 newPos = b.transform.position;
            //newPos.y -= yOffset;
            //b.transform.position = newPos;
            b.NeedleDrop(yOffset);
        }
    }

    //protected void RecordScratch(objBullet)
    //{
    //    set speed to a fast negative value(like - 7) to have bullets move backwards until it is close to spawn position
    //possibly get check distance from enemy parent
    //set bullet speed to a positive fast value(move bullets fast forward until all bullets are inactive)
}
