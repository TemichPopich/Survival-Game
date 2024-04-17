using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour
{
    public string name;

    public string firstRequirement;
    public string secondRequirement;

    public int firstReqAmount;
    public int secondReqAmount;

    public Blueprint(string name, string firstRequirement, int firstReqAmount, string secondRequirement, int secondReqAmount)
    {
        this.name = name;
        this.firstRequirement = firstRequirement;
        this.secondRequirement = secondRequirement;
        this.firstReqAmount = firstReqAmount;
        this.secondReqAmount = secondReqAmount;
    }
}
