using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
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
        Debug.Log(dr);
        Debug.Log("Death 2");
        var volume = FindObjectOfType<Volume>();
        if (volume != null)
        {
            //volume.profile.GetComponent<DepthOfField>();
        }
        //var dof = processVolume.profile.GetSetting<DepthOfField>();
        //dof.enabled.value = true;
        //DOTween.To(() => dof.focalLength, x => dof.focalLength.value = x, 107.0f, 3f);
        //if(dr== LevelManager.DeathReason.Collision)
        //{
        //    transform.DOShakePosition(0.2f, 1, 10, 90, false, true, ShakeRandomnessMode.Harmonic);
        //}
    }

    private void HandleDayProgressed(float obj)
    {
        _zoomLevel += 0.033f * Time.deltaTime;
        Camera.main.orthographicSize = _zoomLevel;
    }

    void Update()
    {
        
    }
}
