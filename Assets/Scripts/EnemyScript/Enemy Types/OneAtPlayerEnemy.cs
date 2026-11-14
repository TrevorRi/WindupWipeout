using UnityEngine;

public class OneAtPlayerEnemy : Enemy
{
    [SerializeField] private PlayerController player;
    [SerializeField] protected int numBullets = 1;


    public override void ShootBullets()
    {
        ShootOneAtPlayer(player, bulletPrefab);
        ShootMultipleAtPoint(player.transform.position, bulletPrefab, numBullets, 12.25f);
        Debug.Log("shot");
    }
}
