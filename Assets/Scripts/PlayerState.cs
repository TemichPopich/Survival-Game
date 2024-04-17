using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance;

    public GameObject playerBody;

    public float currentHealth;
    public float maxHealth = 100;

    public float currentStamina;
    public float maxStamina = 100;

    float distanceTravelled = 0;
    Vector3 lastPosition;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth - 50;
        currentStamina = maxStamina;
        lastPosition = playerBody.transform.position;
    }

    void Update()
    {
        distanceTravelled += Vector3.Distance(playerBody.transform.position, lastPosition);
        lastPosition = playerBody.transform.position;

        if (distanceTravelled > 3 && PlayerMovement.Instance.isRunning)
        {
            distanceTravelled = 0;
            currentStamina -= 1;
        }

        if (!PlayerMovement.Instance.isRunning && currentStamina < maxStamina)
        {
            currentStamina += 10 * Time.deltaTime;
        }
    }
}
