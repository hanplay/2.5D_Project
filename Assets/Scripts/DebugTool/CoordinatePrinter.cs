using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoordinatePrinter : MonoBehaviour
{
    public static CoordinatePrinter Instance;
    private TMPro.TextMeshProUGUI textMesh;
    [SerializeField]
    private Transform targetTransform;
    private void Awake()
    {
        Instance = this;
        textMesh = transform.Find("Canvas").Find("Text").GetComponent<TMPro.TextMeshProUGUI>();
    }
    
    private void Update()
    {
        if (null == targetTransform)
            return;
        textMesh.text = targetTransform.position.ToString();
    }

    public void SetTargetTransform(Transform targetTransform)
    {
        this.targetTransform = targetTransform;
    }
}
