using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSystem 
{
    private float baseSpeed;
    private float addedSpeed;
    private float totalSpeed;

    private IMoveStrategy baseMoveStrategy;
    private IMoveStrategy usingMoveStrategy;

    public MoveSystem() { }

    public void Init(IMoveStrategy moveStrategy, float speed)
    {
        baseMoveStrategy = moveStrategy;
        usingMoveStrategy = baseMoveStrategy;
        baseSpeed = speed;
        CalculateSpeed();
    }


    public void RevertBaseMoveStrategy()
    {
        usingMoveStrategy = baseMoveStrategy;
    }

    public void SetMoveStrategy(IMoveStrategy moveStrategy)
    {
        usingMoveStrategy = moveStrategy;
    }

    public IMoveStrategy GetUsingMoveStrategy()
    {
        return usingMoveStrategy;
    }

    public float GetBaseSpeed()
    {
        return baseSpeed;
    }

    public void AddSpeed(float value)
    {
        addedSpeed += value;
        CalculateSpeed();
    }

    public float GetSpeed()
    {
        return totalSpeed;
    }

    public void CalculateSpeed()
    {
        totalSpeed = addedSpeed + baseSpeed;
    }
}
