using UnityEngine;

public class OneAtPlayerEnemy : Enemy
{
    [SerializeField] private EnemyTestPlayer player;


    public override void ShootBullets()
    {
        ShootOneAtPlayer(player, bulletPrefab);
    }
}
