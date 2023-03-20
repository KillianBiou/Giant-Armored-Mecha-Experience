using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingBehaviour : MonoBehaviour
{

    private GameObject bullet;
    private float bulletSpeed;
    private int bulletPerSecond;
    private bool canFire = true;
    private int damage;

    public void Initialize(GameObject bullet, float bulletSpeed, int bulletPerSecond, int damage)
    {
        this.bullet = bullet;
        this.bulletSpeed = bulletSpeed;
        this.bulletPerSecond = bulletPerSecond;
        this.damage = damage;
    }

    private IEnumerator BulletCooldown()
    {
        yield return new WaitForSeconds(1f / bulletPerSecond);
        canFire = true;
    }

    public void Fire(GameObject target)
    {
        if(canFire)
        {
            foreach (Transform child in transform)
            {
                GameObject bulletInstance = Instantiate(bullet, child.position, Quaternion.identity);
                bulletInstance.GetComponent<BulletLogic>().InitilizeBullet(target, bulletSpeed, damage);
            }
            canFire = false;
            StartCoroutine(BulletCooldown());
        }
    }
}
