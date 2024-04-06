using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusController : MonoBehaviour
{
    [SerializeField]
    PlayerStatus playerStatus;
    [SerializeField]
    private int interval = 10;

    // PlayerStatusのデフォルト値
    private float defaultGravitationalAcceleration = -9.81f;
    private float defaultMaxSpeed = 5f;

    [SerializeField]
    float changedGravity = -5f, changedPlayerSpeed = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerStatus.gravitationalAcceleration = defaultGravitationalAcceleration;
        playerStatus.maxSpeed = defaultMaxSpeed;
        playerStatus.isIertia = false;
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.gravity = new Vector2(0, playerStatus.gravitationalAcceleration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Gravity":
                ChangeGravity(changedGravity);
                Invoke("InitGravity", interval);
                break;
            case "Ineritia":
                ChangeInertia();
                Invoke("InitInertia", interval);
                break;
            case "SpeedDown":
                // 引数の値によってスピードが変わるようにする
                ChangeSpeed(changedPlayerSpeed);
                Invoke("InitSpeed", interval);
                break;
            default:
                break;
        }
    }


    // 重力変更
    public void ChangeGravity(float gravity)
    {
        playerStatus.gravitationalAcceleration = gravity;
    }
    // 重力を元に戻す
    public void InitGravity()
    {
        playerStatus.gravitationalAcceleration = defaultGravitationalAcceleration;
    }


    // 慣性の変更
    public void ChangeInertia()
    {
        playerStatus.isIertia = true;
    }
    // 慣性を元に戻す
    public void InitInertia()
    {
        playerStatus.isIertia = false;
    }


    // Playerのスピード変更
    public void ChangeSpeed(float playerSpeed)
    {
        playerStatus.maxSpeed = playerSpeed;
    }
    // Playerのスピードを元に戻す
    public void InitSpeed()
    {
        playerStatus.maxSpeed = defaultMaxSpeed;
    }
}
