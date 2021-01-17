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
    DivineCharge,
    Teleport,
    Stun,
    Slow,
    None

}

public enum BuffCode
{
    None,
    State,
    MoveStrategy,
    DamageStrategy,
}

public enum SkillType
{
    Cure,
    Meteor,
    Aggro,
    Dive,
    Charge,
    DeadlyPoisonBuff,
    IceNova,
    TestBuff,
    FireAura,
    Pray,
    HasteBuff,
    ConcealBuff,
    DivineChargeBuff,
    TeleportBuff,
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
    [SerializeField] public Sprite divineChargeSprite;
    [SerializeField] public Sprite loopingTornadoSprite;
    [SerializeField] public Sprite fireAuraSprite;    
    [SerializeField] public Sprite chargeSkillSprite;
    [SerializeField] public Sprite teleportSprite;
    [SerializeField] public Sprite StunSprite;
    [SerializeField] public Sprite slowDebufSprite;
    [SerializeField] public Sprite IceNovaSprite;
    [SerializeField] public Sprite CureSprite;



    [SerializeField] public GameObject deadlyPoisonBuffEffect;
    [SerializeField] public GameObject loopingTornado;
    [SerializeField] public GameObject hitSmoke;
    [SerializeField] public GameObject smokeExplosion;
    [SerializeField] public GameObject deathSkullExplosion;
    [SerializeField] public GameObject hasteVisualEffect;
    [SerializeField] public GameObject burnExplosionEffect;
    [SerializeField] public GameObject healEffect;
    [SerializeField] public GameObject prayEffect;
    [SerializeField] public GameObject divineChargeEffect;
    [SerializeField] public GameObject divineChargeBuffEffect;
    [SerializeField] public GameObject teleportBuffEffect;
    [SerializeField] public GameObject teleportEffect;
    [SerializeField] public GameObject stunEffect;
    [SerializeField] public GameObject magicSmoke;

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
        case BuffType.DivineCharge:
            buff = new DivineChargeBuff(TypeValue, divineChargeEffect, 5, 3f, 10f);
            buff.SetBuffSprite(divineChargeSprite);
            return buff;
        case BuffType.Teleport:
            buff = new TeleportBuff(TypeValue, 10f, teleportEffect);
            buff.SetBuffSprite(teleportSprite);
            return buff;
        case BuffType.Stun:
            buff = new StunBuff(TypeValue, 3f, stunEffect);
            buff.SetBuffSprite(StunSprite);
            return buff;
        case BuffType.Slow:
            buff = new SlowDebuff(TypeValue, 5f);
            buff.SetBuffSprite(slowDebufSprite);
            return buff;
        default:
            Assert.IsTrue(true);
            Debug.LogError("Buff Type does not Exist!!!!");
            return null;
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
            skill.SetCooldownTime(15f);
            skill.SetIsTargetSkill(true);
            skill.SetSkillSprite(diveSkillSprite);
            skill.SetRange(6f);
            skill.SetSkillState(new DiveSkillState(player, skill, 15, 2.5f, hitSmoke, smokeExplosion));
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
        case SkillType.DivineChargeBuff:
            skill.SetCanCancel(false);
            skill.SetCooldownTime(15f);
            skill.SetIsTargetSkill(false);
            skill.SetSkillSprite(divineChargeSprite);
            skill.SetSkillState(new BuffSkillState(player, skill, CreateBuff(BuffType.DivineCharge), divineChargeBuffEffect));
            return skill;
        case SkillType.TeleportBuff:
            skill.SetCanCancel(false);
            skill.SetCooldownTime(10f);
            skill.SetIsTargetSkill(false);
            skill.SetSkillSprite(teleportSprite);
            skill.SetSkillState(new BuffSkillState(player, skill, CreateBuff(BuffType.Teleport), teleportBuffEffect));
            return skill;
        case SkillType.IceNova:
            skill.SetCanCancel(false);
            skill.SetCooldownTime(25f);
            skill.SetIsTargetSkill(false);
            skill.SetSkillSprite(IceNovaSprite);
            skill.SetSkillState(new IceNovaSkillState(player, skill, CreateBuff(BuffType.Slow)));
            return skill;
        case SkillType.Cure:
            skill.SetCanCancel(false);
            skill.SetCooldownTime(20f);
            skill.SetIsTargetSkill(false);
            skill.SetSkillSprite(CureSprite);
            skill.SetSkillState(new CureSkillState(player, skill, divineChargeBuffEffect));
            return skill;
        default:
            Assert.IsTrue(true);
            Debug.LogError("Skill Type does not Exist!!!!");
            return skill;
        }
    }
}

