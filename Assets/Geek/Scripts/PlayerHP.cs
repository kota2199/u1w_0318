using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    private int hp = 3;

    [SerializeField]
    private Text hpText;

    [SerializeField]
    private GameStatusManager statusManager;

    [SerializeField]
    private GameObject[] hpIcons;

    [SerializeField]
    private UIAnimation uiAnim;

    // Start is called before the first frame update
    void Start()
    {
        OverwriteToUI();
    }

    private void Update()
    {
        if(transform.position.y < -10f)
        {
            Damage(hp);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            Damage(1);
        }
    }

    public void Damage(int damageAmount)
    {
        hp -= damageAmount;

        uiAnim.ScaleAnime(0.7f, 1f, 0.3f);
        OverwriteToUI();

        if(hp <= 0)
        {
            statusManager.GameOver();
        }
    }

    private void OverwriteToUI()
    {
        for(int i = 0; i < hpIcons.Length; i++)
        {
            if(hp >= i + 1)
            {
                hpIcons[i].SetActive(true);
            }
            else
            {
                hpIcons[i].SetActive(false);
            }
        }

        hpText.text = "HP : " + hp.ToString();
    }
}
