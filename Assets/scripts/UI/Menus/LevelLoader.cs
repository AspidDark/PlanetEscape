﻿using System.Collections;
//using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;


    //public void LoadLevel(int sceneIndex)
    //{
    //    StartCoroutine(LoadAsyncronously(sceneIndex));

    //}

    //IEnumerator LoadAsyncronously (int sceneIndex)
    //{
    //    AsyncOperation operation = EditorSceneManager.LoadSceneAsync(sceneIndex);
    //    loadingScreen.SetActive(true);
    //    while (!operation.isDone)
    //    {
    //        float progress = Mathf.Clamp01(operation.progress / 0.9f);
    //        slider.value = progress;
    //        progressText.text = progress*100+"%";
    //        yield return null;
    //    }
    //}
}
