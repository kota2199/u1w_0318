using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLaunchEnemyAnimation : MonoBehaviour
{
    [SerializeField]
    AnimationClip blueBird, greenBird, redBird, yellowBird;

    [SerializeField]
    BulletLaunchEnemy bulletLaunchEnemy;

    Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        SetAnimation();
    }

    public void SetAnimation()
    {
        switch (bulletLaunchEnemy.bulletType)
        {
            case BulletLaunchEnemy.BulletType.Damage:
                animator.CrossFade("BlueBird", 0);
                break;
            case BulletLaunchEnemy.BulletType.Inertia:
                animator.CrossFade("GreenBird", 0);
                break;
            case BulletLaunchEnemy.BulletType.Gravity:
                animator.CrossFade("RedBird", 0);
                break;
            case BulletLaunchEnemy.BulletType.SpeedDown:
                animator.CrossFade("YellowBird", 0);
                break;
            default:
                break;
        }
    }
}
