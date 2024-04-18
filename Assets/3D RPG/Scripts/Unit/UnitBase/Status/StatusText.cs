using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatusText : MonoBehaviour
{
    public TextMesh textMesh;
    Vector3 originPos;

    private void Start()
    {
     //   transform.localPosition = originPos;
      //  textMesh.color = new Color(1, 1, 1, 1);
    }
    private void OnEnable()
    {
    }
    private void OnDisable()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        Renderer renderer = textMesh.GetComponent<Renderer>();
        renderer.material.color = new Color(1, 1, 1, 1);
    }
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
    public void ShowStatusText(string text, string color)
    {
        transform.localPosition = transform.localPosition + new Vector3(0, 2, 0); 
        if (textMesh != null)
        {
            textMesh.text = text;
        }
        switch(color)
        {
            case "Red":
                textMesh.color = Color.red;
                break;
            case "Blue":
                textMesh.color = Color.blue;
                break;
        }
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(0.8f, 0.1f))
                .Append(transform.DOScale(0.5f, 0.1f))
                .Join(transform.DOLocalMoveY(transform.localPosition.y + 1, 1, false))
                .Join(textMesh.GetComponent<Renderer>().material.DOFade(0, 1));
        sequence.Play().OnComplete(()=>gameObject.SetActive(false));
    }
}


