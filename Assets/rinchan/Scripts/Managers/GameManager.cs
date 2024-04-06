using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageType
{
    Land,   // 陸のステージ
    Water   // 水のステージ
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;

    // ステージの種類
    public StageType stageType;
    // ゲームがプレイ中か判定する変数
    public bool isPlay = false;

    // Start is called before the first frame update
    void Start()
    {
        //isPlay = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlay)
        {
            playerController.enabled = false;
        }
        else
        {
            playerController.enabled = true;
        }
    }
}
