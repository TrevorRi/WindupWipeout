using UnityEngine;
using UnityEngine.UIElements;

public class BabyDoll : Enemy
{
    //Baby doll, single shot, medium
    [SerializeField] private GameObject player;

    public override void ShootBullets()
    {
        ShootOneAtPlayer(player, bulletPrefab);
    }
}
