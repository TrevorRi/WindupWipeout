using UnityEngine;
using UnityEngine.UIElements;

public class Marionette : Enemy
{
    //Marionette, fast, maybe multiple shots
    [Header("Marionette - Fast, multiple shots.")]
    [SerializeField] private PlayerController player;
    [SerializeField] protected int numBullets = 1;
    [SerializeField] private float angleBetweenShots = 12.25f;

    public override void ShootBullets()
    {
        ShootMultipleAtPlayer(player, bulletPrefab, numBullets, angleBetweenShots);
    }
}
