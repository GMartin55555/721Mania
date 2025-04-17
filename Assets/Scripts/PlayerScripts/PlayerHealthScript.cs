using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;
    [SerializeField] private ManiaControllerScript mania;

    private void Awake()
    {
        FullHeal();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DamagesPlayer"))
        {
            TakeDamage(collision.gameObject.GetComponent<DamageToPlayerScript>().damage);
            Debug.Log("Player Health: " + currentHealth);
        }
    }


    private void Update()
    {
        if (mania.maniaScore < 110f)
        {
            //Die();
        }
    }

    private void TakeDamage(int damage)
    {
        mania.maniaScore += damage;
    }

    private void Die()
    {
        SceneManager.LoadScene("TestArena");
    }

    private void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void OverHeal(int overHealAmount)
    {
        currentHealth += overHealAmount;
    }

    private void FullHeal()
    {
        currentHealth = maxHealth;
    }
}
