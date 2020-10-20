using UnityEngine;

public class BuffSkillState : SkillState
{
    private Buff buff;
    private GameObject buffEffect;

    public BuffSkillState(Player player, Skill skill, Buff buff, GameObject buffEffect) : base(player, skill)
    {
        this.buff = buff;
    }

    public override void Begin()
    {
        base.Begin();
        animator.Play("Spell");

    }

    public override void Initialize() 
    {
        
    }

    public override void TickAccept(float deltaTime, Command command)
    {
        command.Visit(this);
    }

    
}
