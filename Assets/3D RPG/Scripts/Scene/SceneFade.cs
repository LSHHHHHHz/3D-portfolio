using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 1.5f;

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
}
