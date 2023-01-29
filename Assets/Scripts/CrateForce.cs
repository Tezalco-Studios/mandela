using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateForce : MonoBehaviour
{
    [SerializeField] private GameObject lerpPos;

    private void Awake()
    {
        lerpPos = GameObject.Find("LerpPoint");
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Conveyor")
            transform.position = Vector3.Lerp(transform.position, lerpPos.transform.position, Mathf.Sin(0.2f * Time.deltaTime));
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.name == "LerpPoint")
            Destroy(this.gameObject);
    }
}
