using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

[System.Serializable]
public class Line
{
    public string Text;
    public TextMeshProUGUI TMPro;
}

[System.Serializable]
public class Ending
{
    public List<Line> End = new List<Line>();
}

public class AfterCredit : MonoBehaviour
{
    public static AfterCredit Instance;
    public List<Ending> ending = new List<Ending>();
    public int endIdx = 0;
    private Ending curEnd;
    public Image BG;
    public float fadeDuration = 1f;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Set the singleton instance
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instance
        }
    }
    private void Start()
    {
        StartCoroutine(FadeIn());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Quit();
        }
    }
    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            Color bgColor = BG.color;
            bgColor.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            BG.color = bgColor;
            yield return null;
        }

        Color finalBgColor = BG.color;
        finalBgColor.a = 1f;
        BG.color = finalBgColor;

        curEnd = ending[endIdx];
        foreach (var line in curEnd.End)
        {
            Color lineColor = line.TMPro.color;
            lineColor.a = 0;
            line.TMPro.color = lineColor;
            line.TMPro.text = line.Text;
        }
        foreach (var line in curEnd.End)
        {
            StartCoroutine(FadeInLine(line));
        }
    }

    public void Quit()
    {
        MapLoader.Instance.LoadMap("Menu");
    }


    private IEnumerator FadeInLine(Line line)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            Color lineColor = line.TMPro.color;
            lineColor.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            line.TMPro.color = lineColor;
            yield return null;
        }

        Color finalLineColor = line.TMPro.color;
        finalLineColor.a = 1f;
        line.TMPro.color = finalLineColor;
    }
}
