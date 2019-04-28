using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class question : MonoBehaviour
{

    public GameObject directions;
    public GameObject birthText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        birthText.GetComponent<TextMeshProUGUI>().enabled = false;
        directions.GetComponent<Renderer>().enabled = true;
    }

    private void OnMouseUp()
    {
        birthText.GetComponent<TextMeshProUGUI>().enabled = true;
        directions.GetComponent<Renderer>().enabled = false;
    }
}
