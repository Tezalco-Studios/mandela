using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCounter : MonoBehaviour
{
    public int Points { get; set; } = 0;

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<Text>().text = $"{Points} Correct";
    }
}
