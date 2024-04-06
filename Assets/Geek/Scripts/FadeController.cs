using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeController : MonoBehaviour
{
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();

        FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn()
    {
        this.image.DOFade(endValue: 0f, duration: 1f);
    }

    public void FadeOut()
    {
        this.image.DOFade(endValue: 1f, duration: 1f);
    }
}
