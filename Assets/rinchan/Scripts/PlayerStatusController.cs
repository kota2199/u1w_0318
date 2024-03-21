using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusController : MonoBehaviour
{
    [SerializeField]
    PlayerStatus playerStatus;
    [SerializeField]
    int interval = 10;

    // PlayerStatusのデフォルト値
    private float defaultGravitationalAcceleration = -9.81f;
    private int defaultlinearDrag = 5;
    private float defaultMaxSpeed = 4;

    // Start is called before the first frame update
    void Start()
    {
        
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
                ChangeGravity(20f);
                Invoke("InitGravity", interval);
                break;
            case "Inertia":
                ChangeInertia(3);
                Invoke("InitInertia", interval);
                break;
            case "SpeedDown":
                // 引数の値によってスピードが変わるようにする
                ChangeSpeed(10);
                Invoke("InitSpeed", interval);
                break;
            default:
                break;
        }
    }


    // 重力変更
    public void ChangeGravity(float gravity)
    {
        playerStatus.gravitationalAcceleration += gravity;
    }
    // 重力を元に戻す
    public void InitGravity()
    {
        playerStatus.gravitationalAcceleration = defaultGravitationalAcceleration;
    }


    // 慣性の変更
    public void ChangeInertia(int linearDrag)
    {
        playerStatus.linearDrag = linearDrag;
    }
    // 慣性を元に戻す
    public void InitInertia()
    {
        playerStatus.linearDrag = defaultlinearDrag;
    }


    // Playerのスピード変更
    public void ChangeSpeed(int playerSpeed)
    {
        playerStatus.maxSpeed = playerSpeed;
    }
    // Playerのスピードを元に戻す
    public void InitSpeed()
    {
        playerStatus.maxSpeed = defaultMaxSpeed;
    }
}
