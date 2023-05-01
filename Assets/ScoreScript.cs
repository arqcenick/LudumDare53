using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;
    private float _score;
    private float _shownScore;
    void Start()
    {
        _score = 0;
        _textMesh = GetComponent<TextMeshProUGUI>();
        GameManager.Instance.LevelManager.LE.OnOrderCompleted += HandleOrderComplete;
        GameManager.Instance.LevelManager.LE.OnDayPassed += HandleDayPassed;
        //_textMesh.text = _shownScore.ToString("#");



    }

    private void HandleDayPassed(int obj)
    {
        _score += obj * obj * 100;
        //_textMesh.text = _shownScore.ToString("#");
    }

    private void HandleOrderComplete(OrderComponent obj)
    {
        var data = obj.OrderData;
        _score += (40 * Mathf.Pow(2, data.CargoTypes.Count) + (Time.time - data.CreationTime) * 20);

    }

    private void Update()
    {
        _shownScore = Mathf.Lerp(_shownScore, _score, Time.deltaTime * 7f);
        _textMesh.text = (Mathf.CeilToInt(_shownScore) * 10).ToString("#");

    }


}
