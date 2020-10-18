using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Buff : MonoBehaviour
{
    private Buff buff;
    private Image uI_Blocker;
    private float durationTime = 5f;
    private float remainingTime;
    private void Awake()
    {
        remainingTime = durationTime;
        uI_Blocker = transform.Find("UI_Blocker").GetComponent<Image>();
    }
    
    public void Tick(float deltaTime)
    {
        remainingTime -= deltaTime;
        uI_Blocker.fillAmount = GetRemainingPart();
        if(remainingTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private float GetRemainingPart()
    {
        return 1f - remainingTime / durationTime;
    }


}
