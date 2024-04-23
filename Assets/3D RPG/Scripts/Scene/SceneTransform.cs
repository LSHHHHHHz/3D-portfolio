using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransform : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 1.5f;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeOut(string sceneName)
    {
        float targetAlpha = 1.0f; 
        while (fadeImage.color.a < 0.99f)
        {
            Color newColor = fadeImage.color;
            newColor.a = Mathf.Lerp(newColor.a, targetAlpha, fadeSpeed * Time.deltaTime);
            fadeImage.color = newColor;
            yield return null;
        }
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);

        LoadSceneController.LoadScene(sceneName);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.Find("Player");
        GameObject camera = GameObject.Find("MainCamera");
        if (player != null && camera != null)
        {
            player.transform.position = new Vector3(-50, 5, 0);
            camera.transform.position = player.transform.position + new Vector3(0, 6, -6);
        }
    }
}
