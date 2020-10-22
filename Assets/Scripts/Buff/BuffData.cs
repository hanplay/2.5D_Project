using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public enum BuffType
{
    Test,
    Haste,
    Enchant, 

}

[CreateAssetMenu(fileName = "New Buff Data", menuName = "Buff Data")]
public class BuffData : ScriptableObject
{
    [SerializeField] Sprite testBuffSprite;
    [SerializeField] Sprite hasteBuffSprite;



    public Buff CreateBuff(Unit targetUnit, BuffType buffType)
    {
        Buff buff;
        switch (buffType)
        {
        case BuffType.Test:
            buff = new TestBuff(targetUnit, 7f);
            buff.SetBuffSprite(testBuffSprite);
            return buff;
        case BuffType.Haste:
            buff = new HasteBuff(targetUnit, 10f);
            buff.SetBuffSprite(hasteBuffSprite);
            return buff;
        default:
            Assert.IsTrue(true);
            Debug.LogError("Buff Type does not Exist!!!!");
            return new TestBuff(targetUnit, 7f);
        }
    }
}
