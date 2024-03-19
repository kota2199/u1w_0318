using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LoopingEnemy : MonoBehaviour
{
    private Vector2 startPos, endPos;

    [SerializeField]
    private float amoutOfMove = 1;

    [SerializeField]
    private float interval = 1.0f;

    public enum VectorOFMove
    {
        Vertical, Horizontal
    }

    public VectorOFMove vectorOFMove;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        switch (vectorOFMove)
        {
            case VectorOFMove.Vertical:
                endPos = startPos + new Vector2(0, amoutOfMove);
                break;
            case VectorOFMove.Horizontal:
                endPos = startPos + new Vector2(amoutOfMove, 0);
                break;
            default:
                break;
        }
        StartCoroutine(LoopMove(startPos, endPos));
    }

    private IEnumerator LoopMove(Vector2 start, Vector2 end)
    {
        Vector2 targetPos = end;
        while (true)
        {
            transform.DOMove(targetPos, interval).SetEase(Ease.Linear);
            yield return new WaitForSeconds(interval);

            if (targetPos == end)
            {
                targetPos = start;
            }
            else
            {
                targetPos = end;
            }
        }
    }
}
