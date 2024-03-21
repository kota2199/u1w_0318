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

    private bool startLaunch = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Vector2 posGap = transform.position - player.transform.position;
        float posGapX = Mathf.Abs(posGap.x);
        float posGapY = Mathf.Abs(posGap.y);

        if(posGapX < radiusOfAttack && posGapY < radiusOfAttack)
        {
            if (!startLaunch)
            {
                startLaunch = true;
                StartCoroutine(Launch());
            }
        }
        else
        {
            startLaunch = false;
            StopCoroutine(Launch());
        }
    }

    private IEnumerator Launch()
    {
        while (startLaunch)
        {
            SetBullet();

            Vector3 launchPos = transform.position;

            GameObject b = Instantiate(bullet, launchPos, Quaternion.identity);

            EnemyBulletController bulletController = b.GetComponent<EnemyBulletController>();

            bulletController.launchVector = player.transform.position - transform.position;

            bulletController.speed = bulletSpeed;

            yield return new WaitForSeconds(interval);
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
