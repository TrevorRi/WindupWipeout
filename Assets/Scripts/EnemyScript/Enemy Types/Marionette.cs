using UnityEngine;

public class Marionette : Enemy
{
    [SerializeField] private GameObject player;

    public override void ShootBullets()
    {
        ShootOneAtPlayer(player, bulletPrefab);
    }
}
