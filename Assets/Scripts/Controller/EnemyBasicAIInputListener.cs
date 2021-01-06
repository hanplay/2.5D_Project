using UnityEngine;


public class EnemyBasicAIInputListener : EnemyInputListener
{

    private StateSystem enemyStateSystem;
    private ChaseCommand chaseCommand;

    protected override void Start()
    {
        base.Start();
        enemyStateSystem = enemy.GetStateSystem();
        chaseCommand = new ChaseCommand();
    }
    void Update()
    {
        if(enemyStateSystem.GetIdleState() == enemyStateSystem.GetCurrentState())
        {
            Player randomPlayer = BattleSystem.Instance.GetPlayerWave().GetRandomUnit();
            if(null != randomPlayer)
                chaseCommand.Execute(enemy, randomPlayer);
        }
    }

}
