using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBInteraction : Interactable
{
    [SerializeField] private GameObject radDetector;
    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        radDetector.GetComponent<RadiationDetection>().BPressed = true;
    }

    public override void OnLoseFocus()
    {
        
    }
}
