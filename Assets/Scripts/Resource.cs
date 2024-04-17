using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : InteractableObject
{
    public override void onClick()
    {
        Destroy(gameObject);
    }
}
