using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateSpawner : MonoBehaviour
{
    [SerializeField] private GameObject crate;

    private void Awake()
    {
        SpawnCrate();
    }

    public void SpawnCrate()
    {
        Instantiate(crate, this.transform.position, Quaternion.identity);
    }
}
