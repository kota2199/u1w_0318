using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    [SerializeField]
    private FadeController fadeController;

    public float playerVector = 0;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlayBGM(SceneManager.GetActiveScene().name);

        maxMenuNumber = menus.Length;

        int progress = PlayerPrefs.GetInt("Progress");
        for (int i = 0; i < menus.Length; i++)
        {
            if(progress + 1 >= i)
            {
                menus[i].GetComponent<Image>().color = new Color(1, 1, 1, 1f);
            }
            else
            {
                menus[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.6f);
            }
        }
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
                playerVector = 1f;
            }

            if (Input.GetKeyDown(KeyCode.A) && target > 0)
            {
                canSelectStage = false;
                target--;
                StartCoroutine(PlayerMoveToTarget());
                playerVector = -1f;
            }
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(PlayerJump());

            player.GetComponent<MenuPlayerController>().Jump();
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

        playerVector = 0;
        canSelectStage = true;
    }

    private IEnumerator PlayerJump()
    {
        SoundManager.instance.PlaySE(1);

        if (!onMenu)
        {
            //Tutorial?????u??2?b??1???W?????v????????????
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
                player.transform.DOJump(playerPos, 1.0f, 1, 1.0f);

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
        if (target <= PlayerPrefs.GetInt("Progress") + 1)
        {
            fadeController.FadeOut();

            yield return new WaitForSeconds(1.5f);

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
