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

}

[CreateAssetMenu(fileName = "New Buff Data", menuName = "Buff Data")]
public class BuffData : ScriptableObject
{
    [SerializeField] Sprite testBuffSprite;
    [SerializeField] Sprite hasteBuffSprite;
    [SerializeField] Sprite burnDebuffSprite;
    [SerializeField] Sprite concealBuffSprite;


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
            buff = new BurnDebuff(TypeValue, 3f);
            buff.SetBuffSprite(burnDebuffSprite);
            return buff;

        case BuffType.Conceal:
            buff = new ConcealBuff(TypeValue,3);
            buff.SetBuffSprite(concealBuffSprite);
            return buff;
        default:
            Assert.IsTrue(true);
            Debug.LogError("Buff Type does not Exist!!!!");
            return new TestBuff(TypeValue, 7f);
        }
    }
}
