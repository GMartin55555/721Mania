using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjClass : MonoBehaviour
{
    private GameObject player;

    public int damage;

    private void Start()
    {
        player = GameObject.Find("Player");
        damage = player.GetComponent<ProjWeaponClass>().damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Invoke("Destroy", 0.1f);
        Destroy();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
