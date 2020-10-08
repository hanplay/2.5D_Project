using GameUtility;

public class AttackCommand : Command
{
    private Unit targetUnit;
    public AttackCommand(Player player, Unit targetUnit) : base(player)
    {
        commandMode = CommandMode.Basic;
        this.targetUnit = targetUnit;      
    }

    public override bool CanExecute()
    {
        return true;
    }

    public override void Execute()
    {
        if(player.GetBaseAttackStrategy().GetRange() < player.DistanceToUnit(targetUnit))
        {
            player.SetState(player.GetChaseTargetState(targetUnit));
            player.GetState().Begin();
        }
        else
        {
            player.SetState(player.GetBaseAttackState(targetUnit));
            player.GetState().Begin();
        }
    }
}
