using System.Collections;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public static BattleSystem Instance { private set; get; }

    private float waitTime = 3f;
    private float lagTime;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Wave<Player> playerWave = new Wave<Player>();
    [SerializeField] private WaveContainer<Enemy> enemyWaveContainer = new WaveContainer<Enemy>();

    private State state = State.Idle;

    private enum State
    {
        Idle,
        Battle,
        End
    }

    private void Start()
    {
        playerWave.Init();
        enemyWaveContainer.Init();        
    }

    private void Update()
    {
        switch(state)
        {
        case State.Idle:
            lagTime += Time.deltaTime;
            if(lagTime > waitTime)
            {
                lagTime = 0f;
                state = State.Battle;
            }
            return;
        case State.Battle:
            if (enemyWaveContainer.IsCurrentWaveEnd())
                enemyWaveContainer.SkipToNextWave();

            if (enemyWaveContainer.IsAllWavesEnd())
                state = State.End;
            else
                state = State.Idle;
            return;
        case State.End:
            if(true == playerWave.IsDead())            
                print("Player Lose");
            
            if(true == enemyWaveContainer.IsAllWavesEnd())            
                print("Player Win!!");            

            gameObject.SetActive(false);
            return;
        }
    }

    public Wave<Player> GetPlayerWave()
    {
        return playerWave;
    }

    public Wave<Enemy> GetEnemyWave()
    {
        return enemyWaveContainer.GetCurrentWave();
    }
}
