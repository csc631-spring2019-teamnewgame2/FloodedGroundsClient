﻿using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    private bool isFading;

    public event Action BeforeSceneUnload;
    public event Action AfterSceneLoad;

    public CanvasGroup faderCanvasGroup;
    public GameObject loadingScreenObj;
    public Slider slider;
    public float fadeDuration = 1f;
    public string startingSceneName = "Desktop";
    private float _loadingProgress;
    public float LoadingProgress { get { return _loadingProgress; } }



    private IEnumerator Start()
    {
        faderCanvasGroup.alpha = 1f;
        
        yield return StartCoroutine(LoadSceneAndSetActive(startingSceneName));

        StartCoroutine(Fade(0f));
    }

    public void FadeAndLoadScene(string sceneName)
    {
        if (!isFading)
        {
            StartCoroutine(FadeAndSwitchScenes(sceneName));
        }
    }
    
    private IEnumerator FadeAndSwitchScenes(string sceneName)
    {
        yield return StartCoroutine(Fade(1f));

        if (BeforeSceneUnload != null) BeforeSceneUnload();

        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));

        if (AfterSceneLoad != null) AfterSceneLoad();

        yield return StartCoroutine(Fade(0f));
    }
    
    private IEnumerator LoadSceneAndSetActive(string sceneName)
    {
        AsyncOperation asyncScene =  SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        asyncScene.allowSceneActivation = false;
        loadingScreenObj.SetActive(true);
        while (!asyncScene.isDone)
        {
            // loading bar progress
            slider.value = (asyncScene.progress / 0.9f) * 100;
            // scene has loaded as much as possible, the last 10% can't be multi-threaded
            if (asyncScene.progress >= 0.9f)
            {
                slider.value = 1f;
                // we finally show the scenes
                asyncScene.allowSceneActivation = true;
            }

            yield return null;
        }

        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newlyLoadedScene);
    }

    public IEnumerator Fade(float finalAlpha)
    {
        isFading = true;
        faderCanvasGroup.blocksRaycasts = true;

        float fadeSpeed = Mathf.Abs(faderCanvasGroup.alpha - finalAlpha) / fadeDuration;

        while (!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha))
        {
            faderCanvasGroup.alpha = Mathf.MoveTowards(faderCanvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
            yield return null;
        }

        isFading = false;
        faderCanvasGroup.blocksRaycasts = false;
    }

}


    

