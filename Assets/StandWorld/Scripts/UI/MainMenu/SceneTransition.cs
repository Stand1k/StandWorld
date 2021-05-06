using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Image transitionImage;

    private void Start()
    {
        StartCoroutine(StartTransition());
    }

    public IEnumerator StartTransition()
    {
        transitionImage.DOFillAmount(1f, 1f);
        
        yield return new WaitForSeconds(1.5f);
        
        transitionImage.DOFillAmount(0f, 1f)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }
}