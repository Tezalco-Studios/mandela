using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BoxGen : MonoBehaviour
{
    private static System.Random rand = new System.Random();

    public Tuple<string, float> ARad { get; private set; }
    public Tuple<string, float> BRad { get; private set; }
    public Tuple<string, float> CRad { get; private set; }
    public Tuple<string, float> DRad { get; private set; }
    public Tuple<string, float> HighestRad { get; private set; }

    public List<int> finalCombination = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        List<int> SimonSays = new List<int>() { 1, 2, 3, 4 };

        ARad = new Tuple<string, float>("A", GenerateRadiation());
        BRad = new Tuple<string, float>("B", GenerateRadiation());
        CRad = new Tuple<string, float>("C", GenerateRadiation());
        DRad = new Tuple<string, float>("D", GenerateRadiation());

        HighestRad = ARad.Item2 >= BRad.Item2 && ARad.Item2 >= CRad.Item2 && ARad.Item2 >= DRad.Item2 ? new Tuple<string, float>("A", ARad.Item2)
            : BRad.Item2 >= ARad.Item2 && BRad.Item2 >= CRad.Item2 && BRad.Item2 >= DRad.Item2 ? new Tuple<string, float>("B", BRad.Item2)
            : CRad.Item2 >= ARad.Item2 && CRad.Item2 >= BRad.Item2 && CRad.Item2 >= DRad.Item2 ? new Tuple<string, float>("C", CRad.Item2) 
            : new Tuple<string, float>("D", DRad.Item2);

        //SimonSays = SimonSays.Select(x => new { value = x, order = rand.Next() }).OrderBy(x => x.order).Select(x => x.value).ToList();
        finalCombination = SimonSays.OrderBy(x => rand.Next()).ToList();

        Debug.Log("///// Simon Says values /////");
        for (int i = 0; i < finalCombination.Count; i++) Debug.Log(finalCombination[i]);
    }

    /// <summary>
    /// Generate a random amount of radation
    /// </summary>
    /// <returns>float</returns>
    private float GenerateRadiation()
        => UnityEngine.Random.Range(0f, 100f);
}