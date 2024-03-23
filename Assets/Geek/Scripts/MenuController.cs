using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject[] menus;

    [SerializeField]
    private string[] sceneName;

    [SerializeField]
    private float playerMoveSpeed;

    private bool onMenu = false;

    private bool canSelectStage = false;

    [SerializeField]
    private int target = 0;

    private int maxMenuNumber;

    // Start is called before the first frame update
    void Start()
    {
        maxMenuNumber = menus.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (onMenu)
        {
            if (Input.GetKeyDown(KeyCode.D) && target < maxMenuNumber - 1)
            {
                canSelectStage = false;
                target++;
                StartCoroutine(PlayerMoveToTarget());
            }

            if (Input.GetKeyDown(KeyCode.A) && target > 0)
            {
                canSelectStage = false;
                target--;
                StartCoroutine(PlayerMoveToTarget());
            }
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(PlayerJump());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToTitle();
        }
    }

    private IEnumerator PlayerMoveToTarget()
    {
        player.transform.DOMove(PlayerTargetPosition(), playerMoveSpeed);

        yield return new WaitForSeconds(playerMoveSpeed);
        canSelectStage = true;
    }

    private IEnumerator PlayerJump()
    {
        if (!onMenu)
        {
            //Tutorialの位置に2秒で1回ジャンプして移動する
            player.transform.DOJump(PlayerTargetPosition(), jumpPower: 3f, numJumps: 1, duration: 2f);

            yield return new WaitForSeconds(2);

            onMenu = true;
            canSelectStage = true;
        }

        else
        {
            if (canSelectStage)
            {
                canSelectStage = false;

                Vector3 playerPos = player.transform.position;
                player.transform.DOJump(
                playerPos,//終了時の位置
                1.0f,   //ジャンプの高さ
                1,      //ジャンプ総数
                1.0f);  //演出時間

                yield return new WaitForSeconds(1);

                StartCoroutine(ToNextScene());
            }
        }
    }

    private void ReturnToTitle()
    {
        player.transform.DOJump(new Vector3(0, -0.5f, 0), jumpPower: 3f, numJumps: 1, duration: 2f);
        target = 0;
        onMenu = false;
        canSelectStage = false;
    }

    private IEnumerator ToNextScene()
    {
        if (target <= PlayerPrefs.GetInt("Progress"))
        {
            yield return null;
            SceneManager.LoadScene(sceneName[target]);
        }
        else
        {
            canSelectStage = true;
        }

    }

    private Vector3 PlayerTargetPosition()
    {
        float targetPosX = menus[target].transform.position.x;
        float targetPosY = player.transform.position.y;

        Vector3 targetPos = new Vector3(targetPosX, targetPosY, 0);

        return targetPos;
    }
}
