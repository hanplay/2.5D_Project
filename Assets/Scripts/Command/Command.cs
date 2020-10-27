using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVisitor
{
    void Visit(BasicState basicState);
    void Visit(DieState dieState);
    void Visit(SkillState skillState);
}

public abstract class Command : IVisitor
{
    protected Player player;

    protected Command(Player player)
    {
        this.player = player;
    }

    public abstract  void Visit(BasicState basicState);
    public abstract void Visit(DieState dieState);
    public abstract void Visit(SkillState skillState);


}
