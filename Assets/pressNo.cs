using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pressNo : MonoBehaviour
{
    public Sprite buttonNormal;
    public Sprite buttonPressed;
    public Sprite buttonDisabled;
    public eventHandler handler;
    public GameObject resultText;
    public bool disabledButton = false;

    public GameObject greenButton;
    public GameObject blueButton;
    public GameObject nextButton;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetState()
    {
        sr.sprite = buttonNormal;
        disabledButton = false;
    }

    private void OnMouseDown()
    {
        if (!disabledButton)
        {
            GetComponent<AudioSource>().Play();

            staticData.ChoiceHasBeenMade = true;
            sr.sprite = buttonPressed;
            handler.currentEvent.pressRed();

            // disable other buttons
            SpriteRenderer srBlue = blueButton.GetComponent<SpriteRenderer>();
            srBlue.sprite = blueButton.GetComponent<pressSmile>().buttonDisabled;
            blueButton.GetComponent<pressSmile>().disabledButton = true;

            SpriteRenderer srGreen = greenButton.GetComponent<SpriteRenderer>();
            srGreen.sprite = greenButton.GetComponent<pressCheck>().buttonDisabled;
            greenButton.GetComponent<pressCheck>().disabledButton = true;

            // show result text
            TextMeshProUGUI tg = resultText.GetComponent<TextMeshProUGUI>();
            tg.text = handler.GetComponent<eventHandler>().currentEvent.redChoice.result_text;
            resultText.SetActive(true);

            // enable next button
            SpriteRenderer srNext = nextButton.GetComponent<SpriteRenderer>();
            srNext.sprite = nextButton.GetComponent<nextButton>().buttonNormal;
            nextButton.GetComponent<nextButton>().disabledButton = false;
        }
    }
}
