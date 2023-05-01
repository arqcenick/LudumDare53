using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private Transform _scaler;
    private void Start()
    {
        _scaler.transform.localScale = Vector3.one * 0.05f;
        _scaler.DOScale(Vector3.one, 0.25f);
    }
}
