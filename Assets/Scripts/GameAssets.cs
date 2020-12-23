using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum BuffType
{
    Haste,
    Burn,
    FireAura,
    Conceal,
    Enchant,
    Poisoned,
    PrayBuffer,
    Pray,
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
    Pray,
    HasteBuff,
    ConcealBuff,
    Buff,
    COUNT,
}

//잠시 물점

public class GameAssets : MonoBehaviour
{

    
    [SerializeField] public Sprite hasteBuffSprite;
    [SerializeField] public Sprite burnDebuffSprite;
    [SerializeField] public Sprite concealBuffSprite;
    [SerializeField] public Sprite poisonDebuffSprite;
    [SerializeField] public Sprite deadlyPoisonBuffSprite;
    [SerializeField] public Sprite diveSkillSprite;
    [SerializeField] public Sprite PrayBuffSprite;
    [SerializeField] public Sprite healSkillSprite;
    [SerializeField] public Sprite loopingTornadoSprite;
    [SerializeField] public Sprite fireAuraSprite;    
    [SerializeField] public Sprite chargeSkillSprite;


    [SerializeField] public GameObject deadlyPoisonBuffEffect;
    [SerializeField] public GameObject loopingTornado;
    [SerializeField] public GameObject hitSmoke;
    [SerializeField] public GameObject smokeExplosion;
    [SerializeField] public GameObject deathSkullExplosion;
    [SerializeField] public GameObject hasteVisualEffect;
    [SerializeField] public GameObject burnExplosionEffect;
    [SerializeField] public GameObject healEffect;
    [SerializeField] public GameObject prayEffect;

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
        case BuffType.Haste:
            buff = new HasteBuff(TypeValue, 15f);
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
        case BuffType.Pray:
            buff = new PrayBuff(TypeValue);
            buff.SetBuffSprite(PrayBuffSprite);
            return buff;
        case BuffType.PrayBuffer:
            buff = new AuraBufferBuff(TypeValue, CreateBuff(BuffType.Pray), new TargetingStrategy<Player>(), 20f, prayEffect, 4f);
            buff.SetBuffSprite(PrayBuffSprite);
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
            skill.SetRange(6f);
            skill.SetSkillState(new DiveSkillState(player, skill, hitSmoke, smokeExplosion));
            return skill;
        case SkillType.HasteBuff:
            skill.SetCanCancel(false);
            skill.SetCooldownTime(10f);
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
        case SkillType.Pray:
            skill.SetCanCancel(false);
            skill.SetCooldownTime(25f);
            skill.SetIsTargetSkill(false);
            skill.SetSkillSprite(PrayBuffSprite);
            skill.SetSkillState(new BuffSkillState(player, skill, CreateBuff(BuffType.PrayBuffer), null));
            return skill;
        default:
            Assert.IsTrue(true);
            Debug.LogError("Skill Type does not Exist!!!!");
            return skill;
        }
    }
}

