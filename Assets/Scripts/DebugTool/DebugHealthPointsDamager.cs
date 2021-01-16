using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHealthPointsDamager : MonoBehaviour
{
    private TrueDamageStrategy trueDamageStrategy = new TrueDamageStrategy();
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            List<Player> players = BattleSystem.Instance.GetPlayerWave().GetAllUnit();
            for(int i = 0; i < players.Count; i++)
            {
                trueDamageStrategy.Do(players[i], 100);
            }
        }
    }
}
