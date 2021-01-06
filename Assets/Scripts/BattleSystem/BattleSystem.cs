using System.Collections;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public static BattleSystem Instance { private set; get; }


    private TMPro.TextMeshProUGUI textMesh;
    private Canvas canvas;

    private float waitTime = 3f;
    private float lagTime;

    private void Awake()
    {
        Instance = this;
        canvas = GetComponentInChildren<Canvas>();
        textMesh = canvas.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        canvas.gameObject.SetActive(false);
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
        enemyWaveContainer.HideAll();
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
            if (Wave<Enemy>.State.Hide == enemyWaveContainer.GetCurrentWave().GetState())
                enemyWaveContainer.GetCurrentWave().Show();

            if (enemyWaveContainer.IsCurrentWaveEnd())
                enemyWaveContainer.SkipToNextWave();

            if (enemyWaveContainer.IsAllWavesEnd() || Wave<Player>.State.End ==  playerWave.GetState())
                state = State.End;
            else
                state = State.Idle;
            return;
        case State.End:
            if (Wave<Player>.State.End == playerWave.GetState())
                PlayerLose();

            if (true == enemyWaveContainer.IsAllWavesEnd())
                PlayerWin();            
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

    private void PlayerWin()
    {
        canvas.gameObject.SetActive(true);
        textMesh.text = "WIN";        
        GetComponent<Animator>().Play("FadeIn");
    }

    private void PlayerLose()
    {
        canvas.gameObject.SetActive(true);
        textMesh.text = "LOSE";
        GetComponent<Animator>().Play("FadeIn");
    }
}
