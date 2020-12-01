using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{

    [Range(1, 100000)]
    public float healthPointsCurrently;
    [Range(1, 100000)]
    public float healthPointsCapacity;

    public void IncreaseCurrentHP(float hp)
    {
        healthPointsCurrently += hp;
        SetHealthBarColor();
        if (healthPointsCurrently < healthPointsCapacity)
        {
            healthPointsCurrently = healthPointsCapacity;
        }
    }

    public void DecreaseCurrentHP(float hp)
    {
        healthPointsCurrently -= hp;
        SetHealthBarColor();
        string percentageDmgTaken = "" + healthPointsCurrently / healthPointsCapacity;  

        if (healthPointsCurrently <= 0)
        {
            transform.localScale = new Vector3(0, 1, 1);
        }
        else
        transform.localScale = new Vector3(float.Parse(percentageDmgTaken), 1, 1);
    }

    public void SetHealthPointsCapacity(float HPCapacity)
    {
        healthPointsCapacity = HPCapacity;
        healthPointsCurrently = HPCapacity;
    }

    void SetHealthBarColor()
    {
        if (healthPointsCapacity / 2 >= healthPointsCurrently)
        {
            transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
        if (healthPointsCapacity / 3 >= healthPointsCurrently)
        {
            transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(1.0F, 0.7F, 0.0F);
        }
        if (healthPointsCapacity / 4 >= healthPointsCurrently)
        {
            transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
}
