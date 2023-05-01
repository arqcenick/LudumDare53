using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraController : MonoBehaviour
{
    private LevelManager _levelManager;
    private float _zoomLevel;
    private PostProcessVolume processVolume;
    void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _levelManager.LE.OnDayProgressed += HandleDayProgressed;
        _levelManager.LE.OnPlayerDeath += HandlePlayerDeath;
        _zoomLevel = Camera.main.orthographicSize;
        processVolume = GetComponent<PostProcessVolume>();
    }

    private void HandlePlayerDeath(LevelManager.DeathReason dr)
    {
        var dof = processVolume.profile.GetSetting<DepthOfField>();
        dof.enabled.value = true;
        DOTween.To(() => dof.focalLength, x => dof.focalLength.value = x, 107.0f, 3f);
    }

    private void HandleDayProgressed(float obj)
    {
        _zoomLevel += obj * 0.0002f;
        Camera.main.orthographicSize = _zoomLevel;
    }

    void Update()
    {
        
    }
}
