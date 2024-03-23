using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatusManager : MonoBehaviour
{
    [SerializeField]
    private GameObject goalCanvas, gameOverCanvas;

    [SerializeField]
    private int nextStageNumber;

    // Start is called before the first frame update
    void Start()
    {
        canControllPlayer(false);
        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator CountDown()
    {
        yield return null;
        canControllPlayer(true);
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
        canControllPlayer(false);
    }

    void Goal()
    {
        goalCanvas.SetActive(true);
        canControllPlayer(false);
    }

    public void canControllPlayer(bool able)
    {
        //playerÇÃëÄçÏÇâ¬î\Ç…Ç∑ÇÈ
    }
}
