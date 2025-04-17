using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjClass : MonoBehaviour
{
    public GameObject player;
    public ManiaControllerScript mania;

    public float damage;

    public bool lifeWeapon = false;

    private void Start()
    {
        FindPlayer();
        FindDamage();
    }

    private void OnCollisionEnter(Collision collision)
    {
        DestroyProj();
        if (lifeWeapon)
        {
            mania.maniaScore -= damage;
        }
    }

    public void DestroyProj()
    {
        Destroy(gameObject);
    }

    public void FindPlayer()
    {
        player = GameObject.Find("Player");
    }

    public void FindDamage()
    {
        damage = player.GetComponent<ProjWeaponClass>().damage;
        mania = player.GetComponent<ManiaControllerScript>();
    }
}
