using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManiaShaderScript : MonoBehaviour
{
    [SerializeField] private ManiaControllerScript mania;
    [SerializeField] private RawImage maniaShader;
    [SerializeField] private float maxManiaShaderAlpha;
    private void Update()
    {
        var tempColor = maniaShader.color;
        tempColor.a = Mathf.Min(maxManiaShaderAlpha, mania.maniaScore / 100f);
        maniaShader.color = tempColor;
    }
}
