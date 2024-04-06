using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class Indicate : MonoBehaviour
{
    Vector3 originPos;

    private void OnEnable()
    {
        originPos = transform.localPosition;
        MoveDownUp();
    }

    void MoveDownUp()
    {
        transform.DOLocalMoveY(originPos.y - 5, 0.7f).SetLoops(-1, LoopType.Restart);
    }

    private void OnDisable()
    {
        transform.DOKill();
        transform.localPosition = originPos;
    }
}
