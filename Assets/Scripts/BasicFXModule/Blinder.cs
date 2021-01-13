using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Blinder : MonoBehaviour
{
    public static Blinder Instance { private set; get; }
    private Image image;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        image = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
        BlockMousePointing(true);
        Paint(Color.black);
        FadeOut(1.5f);
    }

    public void Paint(Color color)
    {   
        image.color = color;
    }

    public void FadeIn(float duration)
    {
        int totalFrames = (int)duration * 40;
        StartCoroutine(FadeInCoroutine(1.5f, totalFrames));
    }

    public void FadeOut(float duration)
    {
        int totalFrames = (int)duration * 40;
        StartCoroutine(FadeOutCoroutine(1.5f, totalFrames));
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }


    public void BlockMousePointing(bool isBlocking)
    {
        canvasGroup.blocksRaycasts = isBlocking;
    }

    private IEnumerator FadeOutCoroutine(float duration, int updateCount)
    {
        float deltaAlpha = 1f / updateCount;
        float waitTime = duration / updateCount;
        canvasGroup.alpha = 1f;
        for (int i = 0; i < updateCount; i++)
        {
            yield return new WaitForSeconds(waitTime);
            canvasGroup.alpha -= deltaAlpha;
        }
        BlockMousePointing(false);
    }    
    private IEnumerator FadeInCoroutine(float duration, int updateCount)
    {
        BlockMousePointing(true);
        float deltaAlpha = 1f / updateCount;
        float waitTime = duration / updateCount;
        canvasGroup.alpha = 0f;
        for (int i = 0; i < updateCount; i++)
        {
            yield return new WaitForSeconds(waitTime);
            canvasGroup.alpha += deltaAlpha;
        }
    }

}
