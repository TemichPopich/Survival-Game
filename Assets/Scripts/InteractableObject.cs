using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    private Collider _player;
    [SerializeField] protected string _name;
    [SerializeField] protected int _countOfResources;

    [SerializeField] protected int _maxHealth;

    public bool playerRange = false;

    #region MONO

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other;
            playerRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = null;
            playerRange = false;
        }
    }

    private void OnDestroy()
    {
        EventSystem.OnCollect(_countOfResources, _name);
    }

    #endregion 

    public abstract void onClick();

    public string GetName()
    {
        return _name;
    }
}
