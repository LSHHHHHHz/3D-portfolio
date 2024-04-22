using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneController : MonoBehaviour
{
    static string nextScene;
    [SerializeField]
    Image progressBar;
    [SerializeField]
    Text text;

    public Image[] backGroundImages;
    public Text[] sceneTexts;
    public static void LoadScene(string sceneName)
    {
        nextScene= sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    private void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }
    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0;
        while(!op.isDone)
        {
            yield return null; 
            if(op.progress<0.9f)
            {
                progressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.1f,1f,timer/3);
                if(progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
