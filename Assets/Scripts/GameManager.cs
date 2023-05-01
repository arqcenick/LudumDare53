using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public LevelManager LevelManager => _levelManager;
    private LevelManager _levelManager;
    [SerializeField] private FadeScript fadeScript;
    [SerializeField] private Canvas _gameOverCanvas;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
        fadeScript.FadeOut();


    }

    

    private void HandlePlayerDeath(LevelManager.DeathReason obj)
    {
        Invoke("FadeIin", 1.5f);
        Invoke("ShowGameOverScreen", 3f);

    }
    private void FadeIin()
    {
        fadeScript.FadeIn();
    }

    private void ShowGameOverScreen()
    {
        _gameOverCanvas.gameObject.SetActive(true);
        _gameOverCanvas.GetComponent<CanvasGroup>().DOFade(1, 1.5f);


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
