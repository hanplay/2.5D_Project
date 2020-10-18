using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Skill Data", menuName = "SkillData")]
public class SkillData : ScriptableObject
{
    [SerializeField]
    private Sprite diveSkillSprite;
    public Sprite DiveSkillSprite { get => diveSkillSprite; }

    [SerializeField]
    private Sprite healSkillSprite;
    public Sprite HealSkillSprite { get => healSkillSprite; }

    [SerializeField]
    private Sprite loopingTornadoSprite;
    public Sprite LoopingTornadeSprite { get => loopingTornadoSprite; }




    [SerializeField]
    private GameObject loopingTornado;
    public GameObject LoopingTornado { get => loopingTornado; }

    [SerializeField]
    private GameObject smoke;
    public GameObject Smoke { get => smoke;}


    public Skill CreateSkill(Player player, SkillType skillType)
    {
        Skill skill = new Skill(player);
        switch (skillType)
        {
        case SkillType.Dive:
            skill.SetCanCancel(true);
            skill.SetCooldownTime(5f);
            skill.SetIsTargetSkill(true);
            skill.SetSkillSprite(DiveSkillSprite);
            skill.SetSkillState(new DiveSkillState(player, skill, Smoke));
            return skill;
        default:
            Assert.IsTrue(true);
            Debug.LogError("Skill Type does not Exist!!!!");
            return skill;
        }
    }
}
