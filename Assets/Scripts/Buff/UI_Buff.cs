using UnityEngine;
using UnityEngine.UI;

public class UI_Buff : MonoBehaviour
{
    private Buff buff;
    private Image uI_Blocker;
    private void Awake()
    {
        uI_Blocker = transform.Find("UI_Blocker").GetComponent<Image>();
    }
    
    public void Tick(float deltaTime)
    {

    }



}
