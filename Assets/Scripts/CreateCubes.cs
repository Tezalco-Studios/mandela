using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCubes : MonoBehaviour
{

    [SerializeField] private GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawner());
    }

    IEnumerator spawner()
    {
        for(;;)
        {
            Instantiate(prefab, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(5);
        }
    }
}
