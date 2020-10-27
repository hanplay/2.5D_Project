using UnityEngine;

public class MoveCommand : Command
{
    private Vector3 destination;
    public MoveCommand(Player player, Vector3 destination) : base(player)
    {
        this.destination = destination;
    }

    public override void Visit(DieState dieState)
    {
        return;
    }

    public override void Visit(SkillState skillState)
    {
        if(skillState.IsEnd())
        {
            BasicState basicState = player.GetBasicState();
            basicState.MoveTo(destination);
            player.SetState(basicState);
        }
    }

    public override void Visit(BasicState basicState)
    {
        if(0.8f > Vector3.Distance(player.GetPosition(), destination))
        {
            basicState.Stop();
            return;
        }
        basicState.MoveTo(destination);
    }
}
