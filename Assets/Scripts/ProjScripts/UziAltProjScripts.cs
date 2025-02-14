using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UziAltProjScripts : TestProjClass
{
    private ManiaControllerScript mania;

    private float lifeTime = 2f;
    private float speed;

    private void Start()
    {
        FindPlayer();
        FindDamage();
        mania = player.GetComponent<ManiaControllerScript>();
        float size = Mathf.Min(mania.maniaScore / 25f, 1f);
        transform.localScale = new Vector3(1f, 1.5f, 0.3f) * size;
        speed = mania.maniaScore / 10f + 10f;
    }

    private void Update()
    {
        float lifeCounter;
        lifeCounter = Time.deltaTime;
        if (lifeCounter >= lifeTime)
        {
            DestroyProj();
        }

        transform.position += transform.forward * Time.deltaTime * speed;
    }    
}
