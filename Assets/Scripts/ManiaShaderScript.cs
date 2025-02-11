using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManiaShaderScript : MonoBehaviour
{
    [SerializeField] private ManiaControllerScript mania;
    [SerializeField] private Image maniaShader;
    [SerializeField] private float maxManiaShaderAlpha;
    private void Update()
    {
        var tempColor = maniaShader.color;
        tempColor.a = Mathf.Min(maxManiaShaderAlpha, mania.maniaScore / 120f);
        maniaShader.color = tempColor;
    }
}
