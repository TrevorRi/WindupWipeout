using UnityEngine;
using UnityEngine.UIElements;

public class Marionette : Enemy
{
    //Marionette, fast, maybe multiple shots
    [SerializeField] private GameObject player;

    public override void ShootBullets()
    {
        ShootOneAtPlayer(player, bulletPrefab);
    }
}
