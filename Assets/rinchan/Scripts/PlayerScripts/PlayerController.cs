using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// 操作方法を示すEnum「Operation_Method」
public enum Operation_Method
{
    Horizontal,    // = 0
    JL,            // = 1
    GJ,            // = 2
    IM,            // = 3
    RV,            // = 4
    MQ,            // = 5
    WPXM,          // = 6
}

public enum SpaceKey
{
    Attack, Jump
}

public enum EnterKey
{
    Jump, Attack
}

public class PlayerController : MonoBehaviour
{
    // PlayerのRigidbody
    public Rigidbody2D playerRigidbody2D;
    // PlayerのAnimator
    public Animator playerAnimator;
    [SerializeField]
    PlayerStatus playerStatus;
    [SerializeField]
    CameraController cameraController;
    [SerializeField]
    InputGuidController inputGuidController;
    [SerializeField]
    PlayerInput playerInput;
    GameManager gameManager;

    // 以下変数
    // ジャンプする力の大きさを指定
    public float jumpPower = 10f;
    // Groundに設置しているかの判定処理
    private bool isGround;
    // EnumのOperation_Methodを定義
    [SerializeField]
    private Operation_Method operationMethod;
    // チェックポイントカウント
    public int checkPointCount = 0;
    //  現在の操作方法の名前
    private string operationMethodName;
    // PlayerSpriteの初期サイズを保存する変数
    private Vector3 defaultLocalScale;
    private float horizontalInput;
    private InputAction wpxmAction;
    // WPSMが押されているかを判定
    private bool isPressed;
    private float inputValue;

    public SpaceKey spaceKey;
    public EnterKey enterKey;

    // Start is called before the first frame update
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        switch (gameManager.stageType)
        {
            case StageType.Land:
                playerAnimator.SetTrigger("Land");
                break;
            case StageType.Water:
                playerAnimator.SetTrigger("Swim");
                break;
            default:
                break;
        }

        // カメラの初期位置
        cameraController.SetPosition(transform.position);
        // 初期状態でPlayerの大きさを保存
        defaultLocalScale = transform.localScale;
        // 操作方法をcheckPointの値によって初期化
        operationMethod = (Operation_Method)checkPointCount;
        // PlayerInputからWPSMを取得
        wpxmAction = playerInput.actions["WPXM"];
    }

    // Update is called once per frame
    private void Update()
    {
        // カメラにPlayerの座標を渡す
        cameraController.SetPosition(this.transform.position);

        // PlayerがGroundに接地しているかを判定
        // もしPlayerのy軸方向への加速度が0なら地面に接地していると判定する *たまにジャンプできないときがあったため範囲を指定
        if (playerRigidbody2D.velocity.y > -0.2 && playerRigidbody2D.velocity.y < 0.2)
        {
            if (horizontalInput != 0 && playerRigidbody2D.velocity.x == 0)
            {
                // 壁に張り付いたときの復帰を阻止！
                isGround = false;
                GetComponent<Rigidbody2D>().sharedMaterial.friction = 0;
            }
            else
            {
                // 地面に接地していると判定
                isGround = true;
            }
        }
        else
        {
            // 地面に接地していないと判定
            isGround = false;
        }
        //Debug.Log(playerRigidbody2D.velocity.x);

        // アニメーションの再生
        playerAnimator.SetFloat("Vertical", playerRigidbody2D.velocity.y);
        playerAnimator.SetBool("isGround", isGround);


        // EnterKeyが押されたとき
        if (Input.GetButtonDown("Enter"))
        {
            switch (enterKey)
            {
                case EnterKey.Attack:
                    Attack();
                    break;
                case EnterKey.Jump:
                    Jump();
                    break;
                default:
                    break;
            }
        }       
        // SpaceKeyが押されたとき
        if (Input.GetButtonDown("Space"))
        {
            switch (spaceKey)
            {
                case SpaceKey.Attack:
                    Attack();
                    break;
                case SpaceKey.Jump:
                    Jump();
                    break;
                default:
                    break;
            }
        }
    }

    private void FixedUpdate()
    {
        // プレイヤーの操作
        //列挙子を文字列に変換
        operationMethodName = operationMethod.ToString();    

        switch (operationMethod)
        {
            case Operation_Method.WPXM:
                wpxmAction.Enable();
                isPressed = wpxmAction.IsPressed();
                horizontalInput = inputValue;
                OperatePlayer();
                break;
            default:
                // 移動の横方向をInputから値で取得
                horizontalInput = Input.GetAxis(operationMethodName);
                OperatePlayer();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // チェックポイントだったら
        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            checkPointCount++;
            // Operation_MethodのcheckPointCount番目の操作方法に変更
            operationMethod = (Operation_Method)checkPointCount;
            Debug.Log("現在の操作方法：" + operationMethod);

            StartCoroutine(inputGuidController.Anim(operationMethod.ToString()));
        }
    }

    // Playerの移動
    private void OperatePlayer()
    {
        // アニメーションの再生
        playerAnimator.SetFloat("Horizontal", horizontalInput);

        if (horizontalInput != 0)
        {
            // キャラがどっちに向いているかを判定する
            float direction = Mathf.Sign(horizontalInput);
            // キャラの向きをキーの押された方向に指定する
            transform.localScale =
                new Vector3(defaultLocalScale.x * direction, defaultLocalScale.y, defaultLocalScale.z);
        }

        if (!playerStatus.isIertia)
        {
            // 速度をセットする
            playerRigidbody2D.velocity = new Vector2(horizontalInput * playerStatus.maxSpeed, playerRigidbody2D.velocity.y);
        }
        else if (playerStatus.isIertia)
        {
            // 速度が上がり続けるのを防ぐ
            if (playerRigidbody2D.velocity.magnitude < 10)
            {
                playerRigidbody2D.AddForce(new Vector2(horizontalInput * playerStatus.maxSpeed, 0), ForceMode2D.Force);
            }
        }
    }

    public void OnWPXM(InputValue value)
    {
        // 入力値を取得
        inputValue = value.Get<float>();
    }

    // ジャンプの実行
    private void Jump()
    {
        //地面にいる場合のみ処理する
        if (isGround == true)
        {
            // ジャンプの処理
            playerRigidbody2D.AddForce(Vector2.up * jumpPower * 30);
            // ジャンプアニメーションの再生
            playerAnimator.SetTrigger("Jump");
        }
    }
    // 攻撃の実行
    private void Attack()
    {
        Debug.Log("Attack!");
    }
}
