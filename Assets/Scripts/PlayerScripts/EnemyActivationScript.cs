using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivationScript : MonoBehaviour
{
    private TestDummyAIScript ai;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DamagesPlayer"))
        {
            ai = GetComponent<TestDummyAIScript>();
            ai.allowFolow = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DamagesPlayer"))
        {
            ai = GetComponent<TestDummyAIScript>();
            ai.allowFolow = false;
        }
    }
}
