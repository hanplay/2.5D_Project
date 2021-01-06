using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class EnemyInputListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected BasicFXVisualizer basicFXVisualizer;
    protected Enemy enemy;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        enemy = GetComponent<Enemy>();
        basicFXVisualizer = GetComponent<BasicFXVisualizer>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        basicFXVisualizer.GlowFadeIn(new Color(0.2f, 0.0f, 0f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        basicFXVisualizer.GlowFadeOut(new Color(0.2f, 0f, 0f));
    }
}
