using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskChaseObject : MonoBehaviour
{
    [SerializeField]
    private GameObject chaseObj;

    private Vector3 targetPos;

    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = chaseObj.transform.position;
        Vector2 screenPos = Camera.main.WorldToScreenPoint(targetPos);
        rectTransform.position = screenPos;
    }
}
