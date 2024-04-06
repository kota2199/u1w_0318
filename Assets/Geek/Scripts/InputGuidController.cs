using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InputGuidController : MonoBehaviour
{
    [SerializeField]
    private float maxScale = 1.2f;

    [SerializeField]
    private float minScale = 1f;

    [SerializeField]
    private Image rightKeyImage, leftKeyImage, jumpKeyImage, attackKeyImage;

    [SerializeField]
    private Sprite j, l, g, i, m, r, v, q, w, p, WP, XM, space, enter;

    private string r_Key, l_Key;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Awake()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    public IEnumerator Anim(string keyMap)
    {
        char[] maps = keyMap.ToCharArray();

        if(maps.Length < 3)
        {
            for(int i = 0; i < maps.Length; i++)
            {
                if(i == 0)
                {
                    r_Key = maps[0].ToString();
                }
                if(i == 1)
                {
                    l_Key = maps[1].ToString();
                }
            }
        }
        else
        {
            for (int i = 0; i < maps.Length; i++)
            {
                if (i == 0)
                {
                    r_Key = maps[0].ToString() + maps[1].ToString();
                }
                if (i == 1)
                {
                    l_Key = maps[2].ToString() + maps[3].ToString();
                    Debug.Log("LKey" + l_Key);
                }
            }
        }

        this.transform.DOScale(maxScale, 1f).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(1f);

        KeyConfig();

        this.transform.DOScale(minScale, 1f).SetEase(Ease.InOutBack).SetDelay(2f);
    }

    private void KeyConfig()
    {
        switch (r_Key)
        {
            case "J":
                rightKeyImage.sprite = j;
                break;
            case "L":
                rightKeyImage.sprite = l;
                break;
            case "G":
                rightKeyImage.sprite = g;
                break;
            case "I":
                rightKeyImage.sprite = i;
                break;
            case "M":
                rightKeyImage.sprite = m;
                break;
            case "R":
                rightKeyImage.sprite = r;
                break;
            case "V":
                rightKeyImage.sprite = v;
                break;
            case "Q":
                rightKeyImage.sprite = q;
                break;
            case "W":
                rightKeyImage.sprite = w;
                break;
            case "P":
                rightKeyImage.sprite = p;
                break;
            case "WP":
                rightKeyImage.sprite = WP;
                break;
            default:
                break;
        }

        switch (l_Key)
        {
            case "J":
                leftKeyImage.sprite = j;
                break;
            case "L":
                leftKeyImage.sprite = l;
                break;
            case "G":
                leftKeyImage.sprite = g;
                break;
            case "I":
                leftKeyImage.sprite = i;
                break;
            case "M":
                leftKeyImage.sprite = m;
                break;
            case "R":
                leftKeyImage.sprite = r;
                break;
            case "V":
                leftKeyImage.sprite = v;
                break;
            case "Q":
                leftKeyImage.sprite = q;
                break;
            case "W":
                leftKeyImage.sprite = w;
                break;
            case "P":
                leftKeyImage.sprite = p;
                break;
            case "XM":
                leftKeyImage.sprite = XM;
                break;
            default:
                break;
        }

        if(playerController.spaceKey == SpaceKey.Jump)
        {
            jumpKeyImage.sprite = space;
            attackKeyImage.sprite = enter;
        }
        else
        {
            jumpKeyImage.sprite = enter;
            attackKeyImage.sprite = space;
        }
    }
}
