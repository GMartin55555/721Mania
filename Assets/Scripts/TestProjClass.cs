using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjClass : MonoBehaviour
{
    private GameObject player;

    public float damage;

    private void Start()
    {
        player = GameObject.Find("Player");
        damage = player.GetComponent<ProjWeaponClass>().damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
