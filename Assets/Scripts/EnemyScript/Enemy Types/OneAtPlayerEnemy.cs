using UnityEngine;

public class OneAtPlayerEnemy : Enemy
{
    [SerializeField] private GameObject player;


    public override void ShootBullets()
    {
        ShootOneAtPlayer(player, bulletPrefab);
    }
}
