using UnityEngine;
using GameUtility;

public class MoveCommand : Command
{
    private Vector3 destination;
    public MoveCommand(Player player, Vector3 destination) : base(player)
    {
        this.destination = destination;
        commandMode = CommandMode.Basic;
    }

    public override bool CanExecute()
    {
        return true;
    }

    public override void Execute()
    {
        player.SetState(player.GetMoveToGroundState(destination));
        player.GetState().Begin();        
    }
}
