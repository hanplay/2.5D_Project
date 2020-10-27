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
    TestBuff,
    FireAura,
    HasteBuff,
    ConcealBuff,
    Buff,
    COUNT,
}

[CreateAssetMenu(fileName = "New Skill Data", menuName = "SkillData")]
public class SkillData : ScriptableObject
{
    [SerializeField] private BuffData buffData;

    [SerializeField] private Sprite diveSkillSprite;
    [SerializeField] private Sprite healSkillSprite;
    [SerializeField] private Sprite loopingTornadoSprite;
    [SerializeField] private Sprite testBuffSprite;
    [SerializeField] private Sprite hasteBuffSprite;
    [SerializeField] private Sprite fireAuraSprite;
    [SerializeField] private Sprite concealBuffSprite;
    [SerializeField] private Sprite chargeSkillSprite;


    [SerializeField] private GameObject loopingTornado;
    [SerializeField] private GameObject hitSmoke;
    [SerializeField] private GameObject smokeExplosion;
    [SerializeField] private GameObject deathSkullExplosion;
    [SerializeField] private GameObject hasteVisualEffect;
    [SerializeField] private GameObject burnExplosionEffect;


    public Skill CreateSkill(Player player, SkillType skillType)
    {
        Skill skill;
        skill = new Skill(player);                   
        switch (skillType)
        {
        case SkillType.Dive:
            skill.SetCanCancel(true);
            skill.SetCooldownTime(5f);
            skill.SetIsTargetSkill(true);
            skill.SetSkillSprite(diveSkillSprite);
            skill.SetSkillState(new DiveSkillState(player, skill, hitSmoke, smokeExplosion));
            return skill;
        case SkillType.TestBuff:
            skill.SetCanCancel(false);
            skill.SetCooldownTime(10f);
            skill.SetIsTargetSkill(false);
            skill.SetSkillSprite(testBuffSprite);
            skill.SetSkillState(new BuffSkillState(player, skill, buffData.CreateBuff(BuffType.Test), deathSkullExplosion));
            return skill;
        case SkillType.HasteBuff:
            skill.SetCanCancel(false);
            skill.SetCooldownTime(15f);
            skill.SetIsTargetSkill(false);
            skill.SetSkillSprite(hasteBuffSprite);
            skill.SetSkillState(new BuffSkillState(player, skill, buffData.CreateBuff(BuffType.Haste), hasteVisualEffect));
            return skill;
        case SkillType.FireAura:
            skill.SetCanCancel(false);
            skill.SetCooldownTime(20f);
            skill.SetIsTargetSkill(false);
            skill.SetSkillSprite(fireAuraSprite);
            skill.SetSkillState(new BuffSkillState(player, skill, buffData.CreateBuff(BuffType.FireAura), null));
            return skill;
        case SkillType.ConcealBuff:
            skill.SetCanCancel(false);
            skill.SetCooldownTime(15f);
            skill.SetIsTargetSkill(false);
            skill.SetSkillSprite(concealBuffSprite);
            skill.SetSkillState(new BuffSkillState(player, skill, buffData.CreateBuff(BuffType.Conceal), null));
            return skill;
        case SkillType.Charge:
            skill.SetCanCancel(false);
            skill.SetCooldownTime(15f);
            skill.SetIsTargetSkill(false);
            skill.SetSkillSprite(chargeSkillSprite);
            skill.SetSkillState(new ChargeSkillState(player, skill, 4f, burnExplosionEffect));
            return skill;
        default:
            Assert.IsTrue(true);
            Debug.LogError("Skill Type does not Exist!!!!");
            return skill;
        }
    }
}
