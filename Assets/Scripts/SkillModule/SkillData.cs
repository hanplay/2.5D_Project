using UnityEngine;
using UnityEngine.Assertions;

public enum SkillType
{
    Heal,
    Meteor,
    Aggro,
    Dive,
    Charge,
    RapidFire,
    PoisonedArrow,
    IceAge,
    Buff,
    COUNT,
}

[CreateAssetMenu(fileName = "New Skill Data", menuName = "SkillData")]
public class SkillData : ScriptableObject
{
    [SerializeField] private Sprite diveSkillSprite;
    [SerializeField] private Sprite healSkillSprite;
    [SerializeField] private Sprite loopingTornadoSprite;
    [SerializeField] private GameObject loopingTornado;
    [SerializeField] private GameObject hitSmoke;
    [SerializeField] private GameObject smokeExplosion;


    public Skill CreateSkill(Player player, SkillType skillType)
    {
        Skill skill = new Skill(player);
        switch (skillType)
        {
        case SkillType.Dive:
            skill.SetCanCancel(true);
            skill.SetCooldownTime(5f);
            skill.SetIsTargetSkill(true);
            skill.SetSkillSprite(diveSkillSprite);
            skill.SetSkillState(new DiveSkillState(player, skill, hitSmoke, smokeExplosion));
            return skill;
        default:
            Assert.IsTrue(true);
            Debug.LogError("Skill Type does not Exist!!!!");
            return skill;
        }
    }
}
