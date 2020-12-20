using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum BuffType
{
    Test,
    Haste,
    Burn,
    FireAura,
    Conceal,
    Enchant,
    Poisoned,
    DeadlyPoison,
    None

}

public enum SkillType
{
    Heal,
    Meteor,
    Aggro,
    Dive,
    Charge,
    RapidFire,
    DeadlyPoisonBuff,
    IceAge,
    TestBuff,
    FireAura,
    HasteBuff,
    ConcealBuff,
    Buff,
    COUNT,
}

//잠시 물점

public class GameAssets : MonoBehaviour
{

    
    [SerializeField] private Sprite testBuffSprite;
    [SerializeField] private Sprite hasteBuffSprite;
    [SerializeField] private Sprite burnDebuffSprite;
    [SerializeField] private Sprite concealBuffSprite;
    [SerializeField] private Sprite poisonDebuffSprite;
    [SerializeField] private Sprite deadlyPoisonBuffSprite;
    [SerializeField] private Sprite diveSkillSprite;
    [SerializeField] private Sprite healSkillSprite;
    [SerializeField] private Sprite loopingTornadoSprite;
    [SerializeField] private Sprite fireAuraSprite;    
    [SerializeField] private Sprite chargeSkillSprite;


    [SerializeField] private GameObject deadlyPoisonBuffEffect;
    [SerializeField] private GameObject loopingTornado;
    [SerializeField] private GameObject hitSmoke;
    [SerializeField] private GameObject smokeExplosion;
    [SerializeField] private GameObject deathSkullExplosion;
    [SerializeField] private GameObject hasteVisualEffect;
    [SerializeField] private GameObject burnExplosionEffect;

    public static GameAssets Instance { private set; get; }

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    private Material unitMaterial;
    public Material UnitMaterial
    {
        get { return unitMaterial; }
    }

    public Buff CreateBuff(BuffType TypeValue)
    {
        Buff buff;
        switch (TypeValue)
        {
        case BuffType.Test:
            buff = new TestBuff(TypeValue, 7f);
            buff.SetBuffSprite(testBuffSprite);
            return buff;
        case BuffType.Haste:
            buff = new HasteBuff(TypeValue, 10f);
            buff.SetBuffSprite(hasteBuffSprite);
            return buff;
        case BuffType.Burn:
            buff = new BurnDebuff(TypeValue, 3f, burnExplosionEffect);
            buff.SetBuffSprite(burnDebuffSprite);
            return buff;
        case BuffType.Poisoned:
            buff = new PoisonDebuff(TypeValue, 5f);
            buff.SetBuffSprite(poisonDebuffSprite);
            return buff;
        case BuffType.DeadlyPoison:
            buff = new DeadlyPoisonBuff(TypeValue, 15f, CreateBuff(BuffType.Poisoned));
            buff.SetBuffSprite(deadlyPoisonBuffSprite);
            return buff;
        case BuffType.Conceal:
            buff = new ConcealBuff(TypeValue, 3);
            buff.SetBuffSprite(concealBuffSprite);
            return buff;
        default:
            Assert.IsTrue(true);
            Debug.LogError("Buff Type does not Exist!!!!");
            return new TestBuff(TypeValue, 7f);
        }
    }

    public Skill CreateSkill(Player player, SkillType skillType)
    {
        Skill skill;
        skill = new Skill();
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
            skill.SetSkillState(new BuffSkillState(player, skill, CreateBuff(BuffType.Test), deathSkullExplosion));
            return skill;
        case SkillType.HasteBuff:
            skill.SetCanCancel(false);
            skill.SetCooldownTime(15f);
            skill.SetIsTargetSkill(false);
            skill.SetSkillSprite(hasteBuffSprite);
            skill.SetSkillState(new BuffSkillState(player, skill, CreateBuff(BuffType.Haste), hasteVisualEffect));
            return skill;
        case SkillType.FireAura:
            skill.SetCanCancel(false);
            skill.SetCooldownTime(20f);
            skill.SetIsTargetSkill(false);
            skill.SetSkillSprite(fireAuraSprite);
            skill.SetSkillState(new BuffSkillState(player, skill, CreateBuff(BuffType.FireAura), null));
            return skill;
        case SkillType.ConcealBuff:
            skill.SetCanCancel(false);
            skill.SetCooldownTime(15f);
            skill.SetIsTargetSkill(false);
            skill.SetSkillSprite(concealBuffSprite);
            skill.SetSkillState(new BuffSkillState(player, skill, CreateBuff(BuffType.Conceal), null));
            return skill;
        case SkillType.Charge:
            skill.SetCanCancel(false);
            skill.SetCooldownTime(15f);
            skill.SetIsTargetSkill(false);
            skill.SetSkillSprite(chargeSkillSprite);
            skill.SetSkillState(new ChargeSkillState(player, skill, 4f, burnExplosionEffect));
            return skill;
        case SkillType.DeadlyPoisonBuff:
            skill.SetCanCancel(false);
            skill.SetCooldownTime(20f);
            skill.SetIsTargetSkill(false);
            skill.SetSkillSprite(deadlyPoisonBuffSprite);
            skill.SetSkillState(new BuffSkillState(player, skill, CreateBuff(BuffType.DeadlyPoison), deadlyPoisonBuffEffect));
            return skill;

        default:
            Assert.IsTrue(true);
            Debug.LogError("Skill Type does not Exist!!!!");
            return skill;
        }
    }
}

