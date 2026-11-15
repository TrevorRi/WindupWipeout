using UnityEngine;

public class JackInBox : Enemy
{
    //Jack in the box. 1 big shot, maybe just 1 shot that is an instant death
    [Header("Jack in The Box - 1 big shot, possible instant death?.")]
    [SerializeField] private PlayerController player;

    public override void ShootBullets()
    {
        ShootOneAtPlayer(player, bulletPrefab);
    }
}
