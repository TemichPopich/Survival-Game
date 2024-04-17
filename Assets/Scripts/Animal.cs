using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : InteractableObject, IDamageable
{
    private int _currentHealth;
    public Animator _animator;

    #region MONO

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    #endregion

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    public override void onClick()
    {
        ApplyDamage(20);
    }

    public void ApplyDamage(int _damage)
    {
        if (_currentHealth <= 0)
        {
            _animator.SetBool("isAlive", false);
            GetComponent<AIMovement>().moveSpeed = 0;
            StartCoroutine(Destroy());
        } else
        {
            _currentHealth -= _damage;
        }
    }
}    
