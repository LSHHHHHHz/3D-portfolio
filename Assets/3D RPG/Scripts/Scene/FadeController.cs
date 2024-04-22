using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public static FadeController instance;
    [SerializeField] Image fadeImage;
    [SerializeField] float fadeDuration = 1f;
    [SerializeField] Image topBossFadeImage;
    [SerializeField] Image bottonBossFadeImage;
    [SerializeField] float bossFadeDuration = 3f;
    Vector3 topBossImageStartPos;
    Vector3 bottonBossImageStartPos;
    Vector3 topBossImageEndPos;
    Vector3 bottonBossImageEndPos;

    private void Awake()
    {
        instance = this;

        topBossImageStartPos = new Vector3(0, 640, 0);
        bottonBossImageStartPos = new Vector3(0, -640, 0);
        topBossImageEndPos = new Vector3(0, 450, 0);
        bottonBossImageEndPos = new Vector3(0, -450, 0); 
    }
    public void FadeInOut()
    {
        StartCoroutine(FadeInOut(fadeDuration));        
    }
    IEnumerator FadeInOut(float duration)
    {
        fadeImage.gameObject.SetActive(true);
        float time = 0;
        while(time < duration)
        {
            time += Time.deltaTime;
            float alpha = time / duration;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            yield return null;
        }
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);

        yield return new WaitForSeconds(1f);
        time = 0;
        while(time < duration)
        {
            time += Time.deltaTime;
            float alpha = 1 - time/duration;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            yield return null;
        }
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
    }
    public void BossFadeIn()
    {
        StartCoroutine(BossFadeIn(bossFadeDuration));
    }
    IEnumerator BossFadeIn(float duration)
    {
        topBossFadeImage.gameObject.SetActive(true);
        bottonBossFadeImage.gameObject.SetActive(true);
        float time = 0;
        while(time < duration)
        {
            time += Time.deltaTime;
            topBossFadeImage.rectTransform.anchoredPosition = Vector3.Lerp(topBossImageStartPos, topBossImageEndPos, time/duration);
            bottonBossFadeImage.rectTransform.anchoredPosition = Vector3.Lerp(bottonBossImageStartPos, bottonBossImageEndPos, time/duration);
            yield return null;

        }
        topBossFadeImage.rectTransform.anchoredPosition = topBossImageEndPos;
        bottonBossFadeImage.rectTransform.anchoredPosition = bottonBossImageEndPos;
    }
    public void BossFadeOut()
    {
        StartCoroutine(BossFadeOut(bossFadeDuration));
    }
    IEnumerator BossFadeOut(float duration)
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            topBossFadeImage.rectTransform.anchoredPosition = Vector3.Lerp(topBossImageEndPos, topBossImageStartPos, time / duration);
            bottonBossFadeImage.rectTransform.anchoredPosition = Vector3.Lerp(bottonBossImageEndPos, bottonBossImageStartPos, time / duration);
            yield return null;

        }
        topBossFadeImage.rectTransform.anchoredPosition = topBossImageStartPos;
        topBossFadeImage.gameObject.SetActive(false);
        bottonBossFadeImage.rectTransform.anchoredPosition = bottonBossImageStartPos;
        bottonBossFadeImage.gameObject.SetActive(false);
    }
}
