using System.Collections;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private Wave<Player> playerWave = new Wave<Player>();
    [SerializeField] private WaveContainer<Enemy> enemyWaveContainer = new WaveContainer<Enemy>();    

    private void Start()
    {
        playerWave.Init();
        enemyWaveContainer.Init();        
    }

    private void Update()
    {
        StartCoroutine(UpdateNextWave());
    }

    IEnumerator UpdateNextWave()
    {
        if(true == playerWave.IsDead())
        {
            print("Player Lose");
            gameObject.SetActive(false);
        }

        if(true == enemyWaveContainer.IsAllWavesEnd())
        {
            print("Player Win!!");
            gameObject.SetActive(false);
        }
        else
        {
            if (enemyWaveContainer.IsCurrentWaveEnd())
                enemyWaveContainer.SkipToNextWave();
        }

        yield return new WaitForSeconds(1f);
    }


}
