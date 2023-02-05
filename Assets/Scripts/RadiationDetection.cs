using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class RadiationDetection : MonoBehaviour
{

    [Header("Sliders")]
    [SerializeField] private Slider radSliderOne;
    [SerializeField] private Slider radiationSliderTwo;
    [SerializeField] private Slider radiationSliderThree;
    [SerializeField] private Slider radiationSliderFour;

    [Header("Text boxes")]
    [SerializeField] private GameObject textOne;
    [SerializeField] private GameObject textTwo;
    [SerializeField] private GameObject textThree;
    [SerializeField] private GameObject textFour;
    [SerializeField] private GameObject pointCounter;
    [SerializeField] private GameObject wrongCounter;

    [Header("Simon says buttons")]
    [SerializeField] private GameObject buttonOne;
    [SerializeField] private GameObject buttonTwo;
    [SerializeField] private GameObject buttonThree;
    [SerializeField] private GameObject buttonFour;

    [Header("Materials")]
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material baseMaterial;

    public bool APressed { get; set; } = false;
    public bool BPressed { get; set; } = false;
    public bool CPressed { get; set; } = false;
    public bool DPressed { get; set; } = false;
    public bool Active { get; set; } = false;

    private Tuple<string, float> highestRad = null;
    private GameObject crate = null;

    // Update is called once per frame
    void Update()
    {
        // Something goes here I just don't know what yet
        GetCrateInformation();
        ButtonInteraction();

        if (!Active)
        {
            highestRad = null;
            APressed = false;
            BPressed = false;
            CPressed = false;
            DPressed = false;
            crate = null;
        }
    }

    /*
    private void SimonSays()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 4f))
        {
            if(hit.transform.tag == "Crate")
            {
                List<int> combination = hit.transform.GetComponentInParent<BoxGen>().finalCombination;
                RunSimonSays(combination);
            }
        }
    }
    
    private async void RunSimonSays(List<int> combo)
    {
        await combo.ForEachWithDelay(i => Task.Run(() =>
        {

            if (combo[i] == 1)
            {
                buttonOne.GetComponent<Renderer>().material = redMaterial;
                System.Threading.Thread.Sleep(500);
                buttonOne.GetComponent<Renderer>().material = baseMaterial;
            }
            else if (combo[i] == 2)
            {
                buttonTwo.GetComponent<Renderer>().material = redMaterial;
                System.Threading.Thread.Sleep(500);
                buttonTwo.GetComponent<Renderer>().material = baseMaterial;
            }
            else if (combo[i] == 3)
            {
                buttonThree.GetComponent<Renderer>().material = redMaterial;
                System.Threading.Thread.Sleep(500);
                buttonThree.GetComponent<Renderer>().material = baseMaterial;
            }
            else if (combo[i] == 4)
            {
                buttonFour.GetComponent<Renderer>().material = redMaterial;
                System.Threading.Thread.Sleep(500);
                buttonFour.GetComponent<Renderer>().material = baseMaterial;
            }

        }), 1000);
    }

    */
    

    private void GetCrateInformation()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 10f) && Active == false)
        {
            if(hit.transform.tag == "Crate")
            {
                Active = true; // Change this elsewhere

                highestRad = hit.transform.GetComponentInParent<BoxGen>().HighestRad;
                crate = hit.transform.parent.gameObject;

                Tuple<string, float> ARad = hit.transform.GetComponentInParent<BoxGen>().ARad;
                Tuple<string, float> BRad = hit.transform.GetComponentInParent<BoxGen>().BRad;
                Tuple<string, float> CRad = hit.transform.GetComponentInParent<BoxGen>().CRad;
                Tuple<string, float> DRad = hit.transform.GetComponentInParent<BoxGen>().DRad;

                radSliderOne.value = ARad.Item2;
                radiationSliderTwo.value = BRad.Item2;
                radiationSliderThree.value = CRad.Item2;
                radiationSliderFour.value = DRad.Item2;

                textOne.GetComponent<Text>().text = $"A: {Mathf.Round(ARad.Item2)}%";
                textTwo.GetComponent<Text>().text = $"B: {Mathf.Round(BRad.Item2)}%";
                textThree.GetComponent<Text>().text = $"C: {Mathf.Round(CRad.Item2)}%";
                textFour.GetComponent<Text>().text = $"D: {Mathf.Round(DRad.Item2)}%";
            }
            else
            {
                radSliderOne.value = 0;
                radiationSliderTwo.value = 0;
                radiationSliderThree.value = 0;
                radiationSliderFour.value = 0;

                textOne.GetComponent<Text>().text = "A: 0%";
                textTwo.GetComponent<Text>().text = "B: 0%";
                textThree.GetComponent<Text>().text = "C: 0%";
                textFour.GetComponent<Text>().text = "D: 0%";
            }
        }
    }

    private void ButtonInteraction()
    {
        if (Active)
        {
            if(highestRad.Item1 == "A" && APressed)
            {
                pointCounter.GetComponent<PointCounter>().Points++;
                crate.GetComponent<CrateAI>().SetNewDestination();
                Active = false;
                return;
            }
            
            if(highestRad.Item1 == "B" && BPressed)
            {
                pointCounter.GetComponent<PointCounter>().Points++;
                crate.GetComponent<CrateAI>().SetNewDestination();
                Active = false;
                return;
            }
            
            if(highestRad.Item1 == "C" && CPressed)
            {
                pointCounter.GetComponent<PointCounter>().Points++;
                crate.GetComponent<CrateAI>().SetNewDestination();
                Active = false;
                return;

            }
            
            if(highestRad.Item1 == "D" && DPressed)
            {
                pointCounter.GetComponent<PointCounter>().Points++;
                crate.GetComponent<CrateAI>().SetNewDestination();
                Active = false;
                return;
            }
        }
    }
}
