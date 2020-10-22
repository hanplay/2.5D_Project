using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsSystem : StatsSystem
{
    private EquipmentSystem equipmentSystem;
    private LevelSystem levelSystem;

    public PlayerStatsSystem(StatsDatum statsDatum, EquipmentSystem equipmentSystem, LevelSystem levelSystem) : base(statsDatum)
    {
        this.equipmentSystem = equipmentSystem;
        this.levelSystem = levelSystem;
    }
}
