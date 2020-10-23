using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public enum BuffType
{
    Test,
    Haste,
    Burn,
    Conseal,
    Enchant, 

}

[CreateAssetMenu(fileName = "New Buff Data", menuName = "Buff Data")]
public class BuffData : ScriptableObject
{
    [SerializeField] Sprite testBuffSprite;
    [SerializeField] Sprite hasteBuffSprite;
    [SerializeField] Sprite BurnDebuffSprite;
    [SerializeField] Sprite ConsealBuffSprite;


    public Buff CreateBuff(Unit targetUnit, BuffType buffType)
    {
        Buff buff;
        switch (buffType)
        {
        case BuffType.Test:
            buff = new TestBuff(7f);
            buff.SetBuffSprite(testBuffSprite);
            return buff;
        case BuffType.Haste:
            buff = new HasteBuff(10f);
            buff.SetBuffSprite(hasteBuffSprite);
            return buff;
        default:
            Assert.IsTrue(true);
            Debug.LogError("Buff Type does not Exist!!!!");
            return new TestBuff(7f);
        }
    }
}
