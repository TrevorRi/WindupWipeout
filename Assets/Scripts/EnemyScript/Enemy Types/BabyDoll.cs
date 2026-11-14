using UnityEngine;
using UnityEngine.UIElements;

public class BabyDoll : Enemy
{
    //Baby doll, single shot, medium
    [Header("Baby Doll - single shot, medium.")]
    [SerializeField] private PlayerController player;

    public override void ShootBullets()
    {
        ShootOneAtPlayer(player, bulletPrefab);
    }
}
