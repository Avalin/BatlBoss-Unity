using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public Transform weapon;

    void Start()
    {
        weapon = this.gameObject.transform.GetChild(0);
        MakeWeaponFloat(weapon);
    }

    void Update()
    {
        if(weapon != null)
        {
            MakeWeaponRotate(weapon);
        }
    }

    void MakeWeaponFloat(Transform weapon)
    {
        Rigidbody rb = weapon.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    void MakeWeaponRotate(Transform weapon)
    {
        if (weapon.tag.Equals("Torch"))
        {
            weapon.Rotate(0, 0, Time.deltaTime * 50);
        }
        else
        {
            weapon.Rotate(0, Time.deltaTime * 50, 0);
        }
    }

    public void RemoveWeaponAsChild()
    {
        weapon = null;
    }

}
