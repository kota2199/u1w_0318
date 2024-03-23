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
    private Image rightKeyImage, lefKeyImage;

    private string r_Key, l_Key;

    // Start is called before the first frame update
    void Start()
    {
        //Anim();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Anim(string keyMap)
    {
        char[] maps = keyMap.ToCharArray();

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

        Debug.Log("R is " + r_Key + ". L is "+l_Key);

        this.transform.DOScale(maxScale, 1f).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(1f);


        this.transform.DOScale(minScale, 1f).SetEase(Ease.InOutBack).SetDelay(2f);
    }
}
