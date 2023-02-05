using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAInteraction : Interactable
{

    [SerializeField] private GameObject radDetector;
    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        Debug.Log("Interacted");
        radDetector.GetComponent<RadiationDetection>().APressed = true;
    }

    public override void OnLoseFocus()
    {
        
    }
}
