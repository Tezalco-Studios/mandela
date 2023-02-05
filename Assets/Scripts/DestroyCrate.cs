using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCrate : MonoBehaviour
{
    private GameObject endDestination;
    private GameObject crateSpawner;
    private GameObject radDetector;

    // Start is called before the first frame update
    void Start()
    {
        endDestination = GameObject.Find("EndDestination");
        crateSpawner = GameObject.Find("CrateSpawner");
        radDetector = GameObject.Find("RadDetector");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(endDestination.transform.position, this.gameObject.transform.position) <= 2f)
        {
            crateSpawner.GetComponent<CrateSpawner>().SpawnCrate();
            radDetector.GetComponent<RadiationDetection>().Active = false;
            Destroy(this.gameObject);
        }
    }
}
