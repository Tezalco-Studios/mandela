using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDInteraction : Interactable
{
    [SerializeField] private GameObject radDetector;
    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        radDetector.GetComponent<RadiationDetection>().DPressed = true;
    }

    public override void OnLoseFocus()
    {
        
    }
}
