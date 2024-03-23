using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStatusManager : MonoBehaviour
{
    [SerializeField]
    private GameObject goalCanvas, gameOverCanvas;

    [SerializeField]
    private int nextStageNumber;

    [SerializeField]
    private GameObject t_CountDown;

    private Text countDown;

    [SerializeField]
    private FadeController fadeController;

    private bool outOfGame = false;


    // Start is called before the first frame update
    private void Awake()
    {
        countDown = t_CountDown.GetComponent<Text>();
    }

    void Start()
    {
        canControllPlayer(false);
        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        if (!outOfGame) return;
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ToNextScene(SceneManager.GetActiveScene().name));
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(ToNextScene("Title&Menu"));
        }
    }

    private IEnumerator CountDown()
    {
        yield return new WaitForSeconds(2f);
        t_CountDown.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            countDown.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        countDown.text = "Go!";
        yield return new WaitForSeconds(1);

        canControllPlayer(true);

        t_CountDown.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Goal();
            PlayerPrefs.SetInt("Progress", nextStageNumber);
        }
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        outOfGame = true;
        canControllPlayer(false);
    }

    void Goal()
    {
        goalCanvas.SetActive(true);
        outOfGame = true;
        canControllPlayer(false);
    }

    public void canControllPlayer(bool able)
    {
        //playerの操作を可能にする
    }

    public IEnumerator ToNextScene(string sceneName)
    {
        fadeController.FadeOut();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }
}
