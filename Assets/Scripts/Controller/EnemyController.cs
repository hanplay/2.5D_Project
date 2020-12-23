using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Player player;

    private ChaseCommand chaseCommand = new ChaseCommand();

    public static EnemyController Instance { private set; get; }
    private void Awake()
    {
        Instance = this;
    }



    private void Update()
    {
        chaseCommand.Execute(enemy, player);
    }
   
   
}
