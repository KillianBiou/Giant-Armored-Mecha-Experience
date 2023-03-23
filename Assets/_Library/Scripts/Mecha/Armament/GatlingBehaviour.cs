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
    private AudioSource audio;

    public void Initialize(GameObject bullet, float bulletSpeed, int bulletPerSecond, int damage, AudioClip gatlingSound)
    {
        this.bullet = bullet;
        this.bulletSpeed = bulletSpeed;
        this.bulletPerSecond = bulletPerSecond;
        this.damage = damage;

        audio = gameObject.AddComponent<AudioSource>();
        audio.clip = gatlingSound;
        this.audio.volume = .3f;
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
            audio.Play();
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
