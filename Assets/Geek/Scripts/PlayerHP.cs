using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    private int hp = 3;

    [SerializeField]
    private Text hpText;

    // Start is called before the first frame update
    void Start()
    {
        OverwriteToText();
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
        OverwriteToText();
    }

    private void OverwriteToText()
    {
        hpText.text = "HP : " + hp.ToString();
    }
}
