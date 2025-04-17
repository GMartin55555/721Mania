using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManiaEnableDisableController : MonoBehaviour
{
    [SerializeField] private ManiaControllerScript mania;
    [SerializeField] private float maniaThreshold;

    private GameObject[] shiftingEnvironment;
    private GameObject[] shiftingEnemies;

    private void Start()
    {
        shiftingEnvironment = GameObject.FindGameObjectsWithTag("ManiaControlledEnvironment");
        shiftingEnemies = GameObject.FindGameObjectsWithTag("ManiaControlledEnemy");
    }

    private void Update()
    {
        ShiftingEnvironment();
        ShiftingEnemies();
    }

    private void ShiftingEnvironment()
    {
        if (mania.maniaScore <= maniaThreshold)
        {
            foreach (GameObject obj in shiftingEnvironment)
            {
                obj.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject obj in shiftingEnvironment)
            {
                obj.SetActive(false);
            }
        }
    }

    private void ShiftingEnemies()
    {
        if (mania.maniaScore >= maniaThreshold)
        {
            foreach (GameObject obj in shiftingEnemies)
            {
                GameObject child = obj.transform.GetChild(0).gameObject;
                child.tag = "DamagesPlayer";
                var followScript = child.GetComponent<TestDummyAIScript>();
                followScript.allowFolow = true;
                var healthScript = child.GetComponent<EnemyHealthScript>();
                healthScript.canTakeDamage = true;
            }
        }
        else
        {
            foreach (GameObject obj in shiftingEnemies)
            {
                GameObject child = obj.transform.GetChild(0).gameObject;
                child.tag = "Untagged";
                var rb = child.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezePosition;
                var followScript = child.GetComponent<TestDummyAIScript>();
                followScript.allowFolow = false;
                var healthScript = child.GetComponent<EnemyHealthScript>();
                healthScript.canTakeDamage = false;
            }
        }
    }
}
