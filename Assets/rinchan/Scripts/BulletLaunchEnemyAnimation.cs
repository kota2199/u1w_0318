using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public enum StageType
{
    Land,   // 陸のステージ
    Water   // 水のステージ
}

public class BulletLaunchEnemyAnimation : MonoBehaviour
{
    [SerializeField]
    AnimationClip blueBird, greenBird, redBird, yellowBird;
    
    [SerializeField]
    private BulletLaunchEnemy bulletLaunchEnemy;

    private Animator animator;
    [SerializeField]
    private AnimatorController animatorController;
    [SerializeField]
    private Transform playerTransform;

    // 以下変数
    AnimationClip[] clips;
    AnimationClip[] birdClips, sharkClips;
    AnimationClip jellyfishClip;
    // BulletLaunchEnemySpriteの初期サイズを保存する変数
    private Vector3 defaultLocalScale;
    private int direction = 1;
    // 補正値
    public float correctionValue = 1.0f;
    [SerializeField]
    private StageType stageType;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();

        birdClips = animatorController.animationClips[0..4];
        jellyfishClip = animatorController.animationClips[4];
        sharkClips = animatorController.animationClips[5..9];

        // 初期状態でBulletLaunchEnemyの大きさを保存
        defaultLocalScale = transform.localScale;

        switch (stageType)
        {
            case StageType.Land:
                clips = birdClips;
                break;
            case StageType.Water:
                clips = sharkClips;
                direction = -1;
                transform.localScale =
                    new Vector3(defaultLocalScale.x * direction, defaultLocalScale.y, defaultLocalScale.z);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // アニメーションを実行
        SetAnimation();

        AdjustDirection(direction);
    }

    public void SetAnimation()
    {
        switch (bulletLaunchEnemy.bulletType)            //    地上  ||   水中
        {
            case BulletLaunchEnemy.BulletType.Damage:    // BlueBird, BlackShark
                animator.CrossFade(clips[0].name, 0);
                break;
            case BulletLaunchEnemy.BulletType.Inertia:   // RedBird, BlueShark
                animator.CrossFade(clips[1].name, 0);
                break;
            case BulletLaunchEnemy.BulletType.Gravity:   // GreenBird, GreenShark
                animator.CrossFade(clips[2].name, 0);
                break;
            case BulletLaunchEnemy.BulletType.SpeedDown: // YellowBird, PinkShark
                animator.CrossFade(clips[3].name, 0);
                break;
            default:
                break;
        }
    }

    private void AdjustDirection(int direction)
    {
        if (this.transform.position.x < playerTransform.position.x)
        {
            transform.localScale = new Vector3(defaultLocalScale.x * -direction, defaultLocalScale.y, defaultLocalScale.z);
        }
        else
        {
            transform.localScale = new Vector3(defaultLocalScale.x * direction, defaultLocalScale.y, defaultLocalScale.z);
        }
    }
}
