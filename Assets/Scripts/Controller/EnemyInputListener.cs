using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyInputListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private BasicFXVisualizer basicFXVisualizer;
    private Enemy enemy;
    private ChaseCommand chaseCommand;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        basicFXVisualizer = GetComponent<BasicFXVisualizer>();
        chaseCommand = new ChaseCommand();
    }
    void Update()
    {
        
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
