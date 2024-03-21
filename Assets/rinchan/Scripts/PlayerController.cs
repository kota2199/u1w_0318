using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Rigidbody2D playerRigidbody2D;
    [SerializeField]
    PlayerStatus playerStatus;

    // 以下変数
    // 移動速度の速さを指定
    private float maxSpeed;
    // ジャンプする力の大きさを指定
    public float jumpPower = 10f;
    // Groundに設置しているかの判定処理
    private bool isGround;
    // EnumのOperation_Methodを定義
    Operation_Method operationMethod = Operation_Method.Horizontal;
    //  現在の操作方法の名前
    private string operationMethodName;
    // チェックポイントカウント
    private int checkPointCount = 0;

    // Start is called before the first frame update
    private void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Playerのスピードを指定
        maxSpeed = playerStatus.maxSpeed;
        // Playerの慣性を指定
        playerRigidbody2D.drag = playerStatus.linearDrag;

        // PlayerがGroundに接地しているかを判定
        // もしPlayerのy軸方向への加速度が0なら地面に接地していると判定する
        if (playerRigidbody2D.velocity.y == 0)
        {
            // 地面に接地していると判定
            isGround = true;
        }
        else
        {
            // 地面に接地していないと判定
            isGround = false;
        }

        // プレイヤーの操作
        //列挙子を文字列に変換
        operationMethodName = operationMethod.ToString();
        OperatePlayer(operationMethodName);

        // ジャンプの実行
        if (Input.GetButtonDown("Jump"))
        {
            //地面にいる場合のみ処理する
            if (isGround == true)
            {
                // ジャンプの処理
                playerRigidbody2D.AddForce(Vector2.up * jumpPower * 30);
            }
        }

        // 攻撃の実行
        if (Input.GetButtonDown("Attack"))
        {
            Debug.Log("Attack!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // チェックポイントだったら
        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            checkPointCount++;
            // 「Operation_Method」のcheckPointCount番目の操作方法に変更
            operationMethod = (Operation_Method)checkPointCount;
            Debug.Log("現在の操作方法：" + operationMethod);
        }
    }

    // Playerの移動
    private void OperatePlayer(string operation)
    {
        // 移動の横方向をInputから値で取得
        float horizontalInput = Input.GetAxis(operation);
        // 速度をセットする
        //playerRigidbody2D.velocity = new Vector2(horizontalInput * maxSpeed, playerRigidbody2D.velocity.y);
        playerRigidbody2D.AddForce(new Vector2(horizontalInput * maxSpeed, 0), ForceMode2D.Force);
    }
}
