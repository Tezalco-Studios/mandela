using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{

    [Header("Look parameters")]
    [SerializeField] private float lookSpeedX = 2.0f;
    [SerializeField] private float lookSpeedY = 2f;
    [SerializeField] private float upperLookLimit = 80f;
    [SerializeField] private float lowerLookLimit = 80f;

    private Camera playerCamera;
    private float rotationX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera= GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PlayerController>().CanMove)
        {
            HandleMouseLook();
        }
            
    }

    private void HandleMouseLook()
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }
}