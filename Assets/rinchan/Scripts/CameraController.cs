using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector2 basePosition; // 基点座標

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // カメラ移動
        Vector3 cameraPosition = transform.localPosition;
        // アクターの現在位置より少し右上を映すようにX・Y座標を補正
        cameraPosition.x = basePosition.x + 2.5f; // X座標
        cameraPosition.y = basePosition.y + 1.5f; // Y座標

        // 計算後のカメラ座標を反映
        transform.localPosition = Vector3.Lerp(transform.localPosition, cameraPosition, 0.08f);
    }

    public void SetPosition(Vector2 targetPosition)
    {
        basePosition = targetPosition;
    }
}
