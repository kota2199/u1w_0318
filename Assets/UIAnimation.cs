using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScaleAnime(float maxScale, float minScale, float delayTime)
    {
        this.transform.DOScale(maxScale, 0.3f).SetEase(Ease.InOutBack);
        this.transform.DOScale(minScale, 0.2f).SetEase(Ease.InOutBack).SetDelay(delayTime);
    }
}
