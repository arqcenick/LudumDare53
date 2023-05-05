using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public LevelManager LevelManager => _levelManager;
    private LevelManager _levelManager;
    [SerializeField] private FadeScript fadeScript;
    [SerializeField] private Canvas _gameOverCanvas;
    [SerializeField] private TextMeshProUGUI reasonText;
    [SerializeField] private TextMeshProUGUI scoreText;
    private LevelManager.DeathReason deathReason;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
        fadeScript.FadeOut();


    }

    

    private void HandlePlayerDeath(LevelManager.DeathReason dr)
    {
        Invoke("FadeIin", 1.5f);
        Invoke("ShowGameOverScreen", 3f);
        deathReason = dr;
    }
    private void FadeIin()
    {
        fadeScript.FadeIn();
    }

    private void ShowGameOverScreen()
    {
        _gameOverCanvas.gameObject.SetActive(true);
        _gameOverCanvas.GetComponent<CanvasGroup>().DOFade(1, 1.5f);
        
        string result = "";

        if (deathReason == LevelManager.DeathReason.Collision)
            result = "You crashed!";
        else if (deathReason == LevelManager.DeathReason.OOB)
            result = "You abondened your post!";
        else if  (deathReason == LevelManager.DeathReason.OrderFail)
            result = "Customer ran out of patience!";

        scoreText.text = "Score: " + (FindFirstObjectByType<ScoreScript>().Score * 10).ToString("#0");
        reasonText.text = result;
    }

    void Start()
    {
        _levelManager = FindFirstObjectByType<LevelManager>();
        _levelManager.LE.OnDeliveryComplete += HandleDeliveryCompletion;
        LevelManager.LE.OnPlayerDeath += HandlePlayerDeath;

    }

    private void HandleDeliveryCompletion()
    {
        Debug.Log("Delivery complete!");
    }
}
