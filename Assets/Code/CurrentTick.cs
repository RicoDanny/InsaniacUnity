using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentTick : MonoBehaviour
{
    public GameObject TickCounterObject;
    
    void Update()
    {
        transform.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = TickCounterObject.GetComponent<TickCounter>().Tickcounter.ToString();
    }
}
