using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class ManiaControllerScript : MonoBehaviour
{
    public float maniaScore;
    private float maniaMultiplier;
    private float maniaSoftCap = 100f;
    [SerializeField] private bool allowManiaPassive;

    [SerializeField] private TMP_Text maniaScoreText;
    [SerializeField] private Image maniaBar;
    [SerializeField] private Slider maniaSlider;

    // Start is called before the first frame update
    void Start()
    {
        maniaScore = 0;
        maniaMultiplier = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SetManiaScore(0);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            SetManiaScore(100);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (allowManiaPassive)
            {
                allowManiaPassive = false;
            }
            else
            {
                allowManiaPassive = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (maniaScore <= maniaSoftCap)
        {
            ManiaGrowth(0.1f);
        }

        maniaScoreText.text = maniaScore.ToString();
        ManiaDisplay();
    }

    private void ManiaGrowth(float growth)
    {
        if(allowManiaPassive)
        {
            maniaScore += growth * maniaMultiplier;
        }
    }

    private void ManiaDecay(float decay)
    {
        maniaScore -= decay;
    }

    private void SetManiaScore(float value)
    {
        maniaScore = value;
    }

    public void ManiaDisplay()
    {
        maniaSlider.value = maniaScore;
    }
}
