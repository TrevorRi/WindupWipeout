using UnityEngine;

public class BabyDoll : Enemy
{
    [SerializeField] private GameObject player;

    public override void ShootBullets()
    {
        ShootOneAtPlayer(player, bulletPrefab);
    }
}
