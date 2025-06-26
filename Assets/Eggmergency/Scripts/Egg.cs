using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Eggmergency.Scripts;
using UnityEngine;

public class Egg : MonoBehaviour
{
    private ObjectPool _pool;
    public void TweenToAndQueue(Transform targetPosition, ObjectPool pool)
    {
        _pool=pool;
        transform.SetParent(targetPosition);
        transform.DOKill();
        transform.DOLocalMove(Vector3.zero,.15f).SetEase(Ease.OutQuad).OnComplete(Queue);
        transform.DOScale(Vector3.zero,.15f).SetEase(Ease.OutQuad);
    }

    private void Queue()
    {
        transform.localScale = Vector3.one;
        if (_pool != null)
        {
            _pool.Queue(gameObject);
        }
        transform.localScale = Vector3.one;

    }
}
