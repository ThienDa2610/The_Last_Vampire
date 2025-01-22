using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

[System.Serializable]
public class Ending
{
    public List<string> lines = new List<string>();
}

public class AfterCredit : MonoBehaviour
{
    public List<TMP_Text> texts = new List<TMP_Text>();
    public List<Ending> ending = new List<Ending>();
    public Canvas mainUI;
    private Canvas afterCredit;
    private Ending curEnd;
    private Animator animator;
    private bool isOpened = false;

    
    private void Start()
    {
        afterCredit = GetComponent<Canvas>();
        afterCredit.enabled = false;
        animator = GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }
    private void Update()
    {
        Debug.Log(isOpened);
        if (isOpened && Input.GetKeyDown(KeyCode.F))
        {
            Quit();
        }
    }

    public void Quit()
    {
        mainUI.enabled = true;
        afterCredit.enabled = false;
        isOpened = false;
        Time.timeScale = 1f;
        MapLoader.Instance.LoadMap("Menu");
    }

    public void StartEnding(int endIdx)
    {
        mainUI.enabled = false;
        afterCredit.enabled = true;

        curEnd = ending[endIdx];

        for (int i = 0; i < curEnd.lines.Count; i++)
        {
            texts[i].text = curEnd.lines[i];
        }

        Time.timeScale = 0f;

        StartCoroutine(FadeInLine());
    }
    private IEnumerator FadeInLine()
    {
        animator.SetTrigger("isOpen");
        yield return new WaitForSecondsRealtime(1f);

        isOpened = true;

        for (int i = 0; i < texts.Count; i++)
        {
            Animator lineAnimator = texts[i].GetComponent<Animator>();
            lineAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
            lineAnimator.SetTrigger("isOpen");
            yield return new WaitForSecondsRealtime(2.5f);
        }
        
    }
}
