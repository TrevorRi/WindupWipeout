using UnityEngine;

public class OneAtPlayerEnemy : Enemy
{
    [SerializeField] private GameObject player;


    public override void ShootBullets()
    {
        //ShootOneAtPlayer(player, bulletPrefab);
        ShootMultipleAtPoint(player.transform.position, bulletPrefab, numBullets, 12.25f);
        Debug.Log("shot");
    }
}
