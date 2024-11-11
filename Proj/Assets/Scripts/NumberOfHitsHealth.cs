using System;
using System.Collections;
using UnityEngine;

public class NumberOfHitsHealth : MonoBehaviour, IHealth
{
    [SerializeField]
    private int healthInHits = 5;

    [SerializeField]
    private float invulnerabilityTimeAfterEachHit = 5f;

    private int hitsRemaining;
    private bool canTakeDamage = true;

    public event Action<float> OnHPPctChanged = delegate (float f) { };
    public event Action OnDied = delegate { };

    public float CurrentHpPct
    {
        get { return (float)hitsRemaining / (float)healthInHits; }
    }

    private void Start()
    {
        hitsRemaining = healthInHits;
    }

    public void TakeDamage(int amount)
    {
        if (canTakeDamage)
        {
            StartCoroutine(InvunlerabilityTimer());

            hitsRemaining--;

            OnHPPctChanged(CurrentHpPct);

            if (hitsRemaining <= 0)
                OnDied();
        }
    }
    public void TakePoisonDamage(int amount)
    {
        if (canTakeDamage)
        {
            StartCoroutine(InvunlerabilityTimer());

            StartCoroutine(Poison());
            OnHPPctChanged(CurrentHpPct);

            if (hitsRemaining <= 0)
                OnDied();
        }
    }
    private IEnumerator Poison()
    {
        canTakeDamage = false;
        for(int i = 0; i < 4; i++)
        {
            hitsRemaining--;
            OnHPPctChanged(CurrentHpPct);
            yield return new WaitForSeconds(0.5f);
        }
        canTakeDamage = true;
    }

    private IEnumerator InvunlerabilityTimer()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(invulnerabilityTimeAfterEachHit);
        canTakeDamage = true;
    }
}