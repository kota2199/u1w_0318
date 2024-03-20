using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLaunchEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject DamageBullet, InertiaBullet, GravityBullet, SpeedDownBullet;
    
    private GameObject bullet;

    [SerializeField]
    private float bulletSpeed, interval;
    public enum BulletType
    {
        Damage, Inertia, Gravity, SpeedDown
    }

    public BulletType bulletType;

    private GameObject player;

    [SerializeField]
    private float radiusOfAttack = 5.0f;

    private Vector2 posGap;

    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        StartCoroutine(Launch());
    }

    void Update()
    {
        posGap = transform.position - player.transform.position;
        if(posGap.x < radiusOfAttack && posGap.y < radiusOfAttack)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

        Debug.Log(isAttacking);
    }

    private IEnumerator Launch()
    {
        while (isAttacking)
        {
            SetBullet();

            yield return new WaitForSeconds(interval);

            Vector3 launchPos = transform.position;

            GameObject b = Instantiate(bullet, launchPos, Quaternion.identity);

            EnemyBulletController bulletController = b.GetComponent<EnemyBulletController>();

            bulletController.launchVector =  player.transform.position - transform.position;

            bulletController.speed = bulletSpeed;    
        }
    }
    public void SetBullet()
    {
        switch (bulletType)
        {
            case BulletType.Inertia:
                bullet = InertiaBullet;
                break;

            case BulletType.Gravity:
                bullet = GravityBullet;
                break;

            case BulletType.SpeedDown:
                bullet = SpeedDownBullet;
                break;

            case BulletType.Damage:
                bullet = DamageBullet;
                break;

            default:
                break;
        }
    }
}
