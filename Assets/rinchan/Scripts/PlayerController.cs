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

    // 以下変数
    // ジャンプする力の大きさを指定
    public float jumpPower = 10f;
    // Groundに設置しているかの判定処理
    private bool isGround;
    // EnumのOperation_Methodを定義
    [SerializeField]
    private Operation_Method operationMethod = Operation_Method.Horizontal;
    //  現在の操作方法の名前
    private string operationMethodName;
    // チェックポイントカウント
    private int checkPointCount = 0;
    // PlayerSpriteの初期サイズを保存する変数
    private Vector3 defaultLocalScale;
    private float horizontalInput;
    private InputAction wpxmAction;
    private bool isPressed;

    // Start is called before the first frame update
    private void Start()
    {
        // カメラの初期位置
        cameraController.SetPosition(transform.position);
        // 初期状態でPlayerの大きさを保存
        defaultLocalScale = transform.localScale;

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
            // 地面に接地していると判定
            isGround = true;
        }
        else
        {
            // 地面に接地していないと判定
            isGround = false;
        }

        // アニメーションの再生
        playerAnimator.SetFloat("Vertical", playerRigidbody2D.velocity.y);
        playerAnimator.SetBool("isGround", isGround);

        // ジャンプの実行
        if (Input.GetButtonDown("Jump"))
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
        if (Input.GetButtonDown("Attack"))
        {
            Debug.Log("Attack!");
        }

        //Debug.Log(GetComponent<Rigidbody2D>().velocity);
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
            while (checkPointCount < 6)
            {
                checkPointCount++;
                // Operation_MethodのcheckPointCount番目の操作方法に変更
                operationMethod = (Operation_Method)checkPointCount;
                Debug.Log("現在の操作方法：" + operationMethod);

                StartCoroutine(inputGuidController.Anim(operationMethod.ToString()));
            }
        }
    }

    // Playerの移動
    private void OperatePlayer()
    {
        // アニメーションの再生
        playerAnimator.SetFloat("Horizontal", horizontalInput);

        if(horizontalInput != 0)
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


    /*public void OnWPXM(InputAction.CallbackContext context)
    {
        //horizontalInput = inputValue.Get<float>();
        //Debug.Log(horizontalInput);
        Debug.Log(context.ReadValue<float>());
        if (isWPSM == true)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                var inputValue = context.ReadValue<float>();
                horizontalInput = inputValue;
                OperatePlayer();
            }
        }
    }*/

    public void OnWPXM(InputValue value)
    {
        Debug.Log(isPressed);
        horizontalInput = value.Get<float>();
        OperatePlayer();
    }
}
