using UnityEngine;

public enum InputType
{
    None,
    Move,
    Attack,
    Skill
}


public class InputBuffer 
{
    private InputType inputType = InputType.None;
    private Player player;
    private Unit targetUnit;
    private Vector3 destination;
    private Skill skill;

    public InputBuffer(Player player)
    {
        this.player = player;
    }


    public void Refresh()
    {
        inputType = InputType.None;
        targetUnit = null;
        skill = null;
    }

    public InputType GetInputType()
    {
        return inputType;
    }

    public Skill GetUsableSkill()
    {
        return skill;
    }


    public Unit GetTargetUnit()
    {
        return targetUnit;
    }

    public Vector3 Direction()
    {
        if (null == targetUnit)
        {
            Vector3 direction = destination - player.GetPosition();
            direction.y = 0f;
            direction.Normalize();
            return direction;
        }
        else
        {
            Vector3 direction = targetUnit.GetPosition() - player.GetPosition();
            direction.y = 0f;
            direction.Normalize();
            return direction;

        }
    }

    public Vector3 Destination()
    {
        return destination;
    }


    public void ReceiveMouseInput(Vector3 destination)
    {
        this.destination = destination;
        targetUnit = null;
        inputType = InputType.Move;
    }

    public void ReceiveMouseInput(Unit targetUnit)
    {
        this.targetUnit = targetUnit;
        inputType = InputType.Attack;
    }

    public void ReceiveSkillInput(int i)
    {
       
    }
}
