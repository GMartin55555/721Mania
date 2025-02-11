using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    [SerializeField] private int maxHealth;

    private int currentHealth;
    public bool canTakeDamage = true;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DamagesEnemy"))
        {
            TakeDamage(collision.gameObject.GetComponent<TestProjClass>().damage);
            Debug.Log("Enemy Health: " + currentHealth);
        }
    }

    private void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }

    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
