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
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    void Start()
    {
        _levelManager = FindFirstObjectByType<LevelManager>();
        _levelManager.LE.OnDeliveryComplete += HandleDeliveryCompletion;
    }

    private void HandleDeliveryCompletion()
    {
        Debug.Log("Delivery complete!");
    }
}
