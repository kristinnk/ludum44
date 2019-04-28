using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class confirmHandler : MonoBehaviour
{
    public GameObject[] selectors;
    public int WorkFields;
    public int FamilyFields;
    public int FunFields;
    public int EmptyFields;

    private void Start()
    {
        WorkFields = 0;
        FamilyFields = 0;
        FunFields = 0;
        EmptyFields = 9;
    }

    private void Update()
    {
        int work = 0;
        int fam = 0;
        int fun = 0;
        int empty = 0;
        
        foreach (GameObject obj in selectors)
        {
            selectorScript selector = obj.GetComponent<selectorScript>();
            switch (selector.currentSpriteIndex)
            {
                case 0:
                    empty++;
                    break;
                case 1:
                    fun++;
                    break;
                case 2:
                    fam++;
                    break;
                case 3:
                    work++;
                    break;
                default: break;
            }
            WorkFields = work;
            FamilyFields = fam;
            FunFields = fun;
            EmptyFields = empty;

            staticData.WorkFields = work;
            staticData.FamilyFields = fam;
            staticData.FunFields = fun;
        }
    }
}
