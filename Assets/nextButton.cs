using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextButton : MonoBehaviour
{
    public Sprite buttonNormal;
    public Sprite buttonPressed;
    public Sprite buttonDisabled;
    public bool disabledButton = true;

    public GameObject greenButton;
    public GameObject blueButton;
    public GameObject redButton;
    public GameObject handler;
    public GameObject resultText;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (staticData.ChoiceHasBeenMade == false)
        {
            sr.sprite = buttonDisabled;
        }
        else
        {
            sr.sprite = buttonNormal;
        }
    }

    public void resetState()
    {
        sr.sprite = buttonDisabled;
        disabledButton = true;
    }

    private void OnMouseDown()
    {   
        if (staticData.ChoiceHasBeenMade) {
            GetComponent<AudioSource>().Play();
            sr.sprite = buttonPressed;
            resultText.SetActive(false);
            greenButton.GetComponent<pressCheck>().resetState();
            blueButton.GetComponent<pressSmile>().resetState();
            redButton.GetComponent<pressNo>().resetState();
            handler.GetComponent<eventHandler>().progressLife();
        }
    }

    private void OnMouseUp()
    {
        sr.sprite = buttonDisabled;
    }
}
