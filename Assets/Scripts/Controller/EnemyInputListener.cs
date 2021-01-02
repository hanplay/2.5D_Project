using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyInputListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private BasicFXVisualizer basicFXVisualizer;
    private Enemy enemy;
    private StateSystem enemyStateSystem;
    private ChaseCommand chaseCommand;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        enemyStateSystem = enemy.GetStateSystem();
        basicFXVisualizer = GetComponent<BasicFXVisualizer>();
        chaseCommand = new ChaseCommand();
    }
    void Update()
    {
        if(enemyStateSystem.GetIdleState() == enemyStateSystem.GetCurrentState())
        {
            Player randomPlayer = BattleSystem.Instance.GetPlayerWave().GetRandomUnit();
            chaseCommand.Execute(enemy, randomPlayer);
        }
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
