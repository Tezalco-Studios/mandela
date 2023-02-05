using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCInteraction : Interactable
{
    [SerializeField] private GameObject radDetector;
    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        radDetector.GetComponent<RadiationDetection>().CPressed = true;
    }

    public override void OnLoseFocus()
    {
        
    }
}
