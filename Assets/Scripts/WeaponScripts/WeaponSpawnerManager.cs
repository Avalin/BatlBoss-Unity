using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawnerManager : MonoBehaviour {

    public List<GameObject> weaponSpawners;
    private List<GameObject> weaponSpawnersBackup;

    void Start ()
    {
        weaponSpawners = new List<GameObject>();

        //Instantiates 5 weaponspawners at start
        for(int i = 0; i < 5; i++)
        {
            CreateRandomWeaponSpawner();
        }
    }

    void Update()
    {
        weaponSpawnersBackup = weaponSpawners;
        foreach (GameObject ws in weaponSpawnersBackup)
        {
            //If weapon has been taken from spawner
            if (ws.transform.childCount == 0)
            {
                RemoveWeaponSpawner(ws);
                //Instantiates a new weapon, once one is taken
                CreateRandomWeaponSpawner();
            }
        }
        weaponSpawners = weaponSpawnersBackup;
    }

    void RemoveWeaponSpawner(GameObject weaponSpawner)
    {
        weaponSpawners.Remove(weaponSpawner);
        Destroy(weaponSpawner);
    }

    void CreateRandomWeaponSpawner()
    {
        string weaponSpawnerPath = "Prefabs/WeaponSpawner/WeaponSpawner" + Random.Range(0, 4);
        Vector3 spawnAreaRange = new Vector3(Random.Range(-15.0f, 15.0f), 1.48f, Random.Range(-15.0f, 15.0f));
        GameObject ws = Instantiate(Resources.Load(weaponSpawnerPath), 
            spawnAreaRange, Quaternion.identity * transform.localRotation) as GameObject;
        weaponSpawners.Add(ws);
    }
}
