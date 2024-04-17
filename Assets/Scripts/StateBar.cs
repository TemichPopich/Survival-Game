using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateBar : MonoBehaviour
{
    public Slider slider;
    public GameObject playerState;
    private PlayerState states;

    private float maxHealth, currentHealth;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        states = playerState.GetComponent<PlayerState>();
        onChanged();
    }

    // Update is called once per frame
    void Update()
    {
        onChanged();
    }

    void onChanged()
    {
        currentHealth = Mathf.Round(Mathf.Max(0, states.currentHealth));
        maxHealth = states.maxHealth;

        float fillValue = currentHealth / maxHealth;

        slider.value = fillValue;
    }
}
