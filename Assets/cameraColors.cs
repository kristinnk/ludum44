using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraColors : MonoBehaviour
{

    public Camera cam;

    public Color workColor;
    public Color familyColor;
    public Color funColor;
    public Color defaultColor;
    public float duration = 3.0f;
    private int currentColorIndex;
    private float startTime;
    private int lastState;
    private Color lastColor;

    // Start is called before the first frame update
    void Start()
    {
        currentColorIndex = staticData.Field1;
        lastState = currentColorIndex;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float t = (Time.time - startTime) / duration;
        int currentProgress = (staticData.LifeProgress + 1) / 2;
        int fieldType = getFieldType();
        if (fieldType != lastState)
        {
            startTime = Time.time;
            lastColor = cam.backgroundColor;
        }
        lastState = fieldType;

        switch (fieldType)
        {
            case 1:
                cam.backgroundColor = Color.Lerp(lastColor, funColor, t);
                break;
            case 2:
                cam.backgroundColor = Color.Lerp(lastColor, familyColor, t);
                break;
            case 3:
                cam.backgroundColor = Color.Lerp(lastColor, workColor, t);
                break;
            default:
                // cam.backgroundColor = Color.Lerp(defaultColor, workColor, t);
                cam.backgroundColor = defaultColor;
                break;
        }
    }

    private int getFieldType()
    {
        int currentField = (staticData.LifeProgress + 1) / 2;
        int retValue = 0;
        switch (currentField)
        {
            case 1:
                retValue = staticData.Field1;
                break;
            case 2:
                retValue = staticData.Field2;
                break;
            case 3:
                retValue = staticData.Field3;
                break;
            case 4:
                retValue = staticData.Field4;
                break;
            case 5:
                retValue = staticData.Field5;
                break;
            case 6:
                retValue = staticData.Field6;
                break;
            case 7:
                retValue = staticData.Field7;
                break;
            case 8:
                retValue = staticData.Field8;
                break;
            case 9:
                retValue = staticData.Field9;
                break;
            default:
                retValue = 0;
                break;
        }
        return retValue;
    }
}
