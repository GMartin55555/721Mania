using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;

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


    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        SceneManager.LoadScene("TestScene");
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
