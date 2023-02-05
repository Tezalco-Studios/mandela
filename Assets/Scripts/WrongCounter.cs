using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WrongCounter : MonoBehaviour
{
    public int WrongPoints { get; set; } = 0;

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.GetComponent<Text>().text = $"{WrongPoints} Wrong";
    }
}
