using System;
using System.Collections;
using UnityEngine;

public class StandardHealth : MonoBehaviour, IHealth
{
    [SerializeField] private int startingHealth = 100;
    private MeshRenderer m;

    private int currentHealth;

    public event Action<float> OnHPPctChanged = delegate { };
    public event Action OnDied = delegate { };

    private void Start()
    {
        currentHealth = startingHealth;
        m = GetComponent<MeshRenderer>();
    }

    public float CurrentHpPct
    {
        get { return (float)currentHealth / (float)startingHealth; }
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException("Invalid Damage amount specified: " + amount);

        currentHealth -= amount;

        OnHPPctChanged(CurrentHpPct);

        if (CurrentHpPct <= 0)
            Die();
    }
    public void TakePoisonDamage(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException("Invalid Damage amount specified: " + amount);

        StartCoroutine(Poison(amount));
        OnHPPctChanged(CurrentHpPct);

        if (CurrentHpPct <= 0)
            Die();
    }
    private IEnumerator Poison(int amount)
    {
        m.material.color = Color.green;
        for (int i = 0; i < 5; i++)
        {
            if (CurrentHpPct <= 0)
                Die();

            currentHealth -= amount;
            OnHPPctChanged(CurrentHpPct);
            yield return new WaitForSeconds(0.5f);
        }
        m.material.color = Color.yellow;
    }
    private void Die()
    {
        OnDied();
        GameObject.Destroy(this.gameObject);
    }
}