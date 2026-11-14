using UnityEngine;

public class JackInBox : Enemy
{
    //Jack in the box. 1 big shot, maybe just 1 shot that is an instant death
    [SerializeField] private GameObject player;

    public override void ShootBullets()
    {
        ShootOneAtPlayer(player, bulletPrefab);
    }
}
