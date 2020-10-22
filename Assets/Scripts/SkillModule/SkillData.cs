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
    HasteBuff,
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



    [SerializeField] private GameObject loopingTornado;
    [SerializeField] private GameObject hitSmoke;
    [SerializeField] private GameObject smokeExplosion;
    [SerializeField] private GameObject deathSkullExplosion;
    

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
            skill.SetSkillState(new BuffSkillState(player, skill, buffData.CreateBuff(null, BuffType.Test), deathSkullExplosion));
            if(null == skill.GetSkillState())
            {
                Debug.Log("Test Buff State null");
            }
            return skill;
        case SkillType.HasteBuff:
            skill.SetCanCancel(false);
            skill.SetCooldownTime(15f);
            skill.SetIsTargetSkill(false);
            skill.SetSkillSprite(hasteBuffSprite);
            skill.SetSkillState(new BuffSkillState(player, skill, buffData.CreateBuff(null, BuffType.Haste), deathSkullExplosion));
            if (null == skill.GetSkillState())
            {
                Debug.Log("Haste Buff State null");
            }
            return skill;
        default:
            Assert.IsTrue(true);
            Debug.LogError("Skill Type does not Exist!!!!");
            return skill;
        }
    }
}
