using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjClass : MonoBehaviour
{
    public GameObject player;

    public float damage;

    private void Start()
    {
        FindPlayer();
        FindDamage();
    }

    private void OnCollisionEnter(Collision collision)
    {
        DestroyProj();
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
    }
}
