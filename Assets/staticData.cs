using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class staticData
{
    private static int field1, field2, field3, field4, field5, field6, field7, field8, field9;
    private static int workFields, familyFields, funFields;
    private static int lifeProgress;
    private static int moneyStatus, happyStatus, popularityStatus;
    private static bool choiceHasBeenMade;

    public static int LifeProgress
    {
        get { return lifeProgress; }
        set { lifeProgress = value; }
    }

    public static int WorkFields
    {
        get { return workFields; }
        set { workFields = value; }
    }

    public static int FamilyFields
    {
        get { return familyFields; }
        set { familyFields = value; }
    }

    public static int FunFields
    {
        get { return funFields; }
        set { funFields = value; }
    }

    public static int Field1
    {
        get { return field1; }
        set { field1 = value; }
    }

    public static int Field2
    {
        get { return field2; }
        set { field2 = value; }
    }

    public static int Field3
    {
        get { return field3; }
        set { field3 = value; }
    }

    public static int Field4
    {
        get { return field4; }
        set { field4 = value; }
    }

    public static int Field5
    {
        get { return field5; }
        set { field5 = value; }
    }

    public static int Field6
    {
        get { return field6; }
        set { field6 = value; }
    }

    public static int Field7
    {
        get { return field7; }
        set { field7 = value; }
    }

    public static int Field8
    {
        get { return field8; }
        set { field8 = value; }
    }

    public static int Field9
    {
        get { return field9; }
        set { field9 = value; }
    }

    public static bool ChoiceHasBeenMade
    {
        get { return choiceHasBeenMade; }
        set { choiceHasBeenMade = value; }
    }

    public static int MoneyStatus
    {
        get { return moneyStatus; }
        set { moneyStatus = value; }
    }

    public static int HappyStatus
    {
        get { return happyStatus; }
        set { happyStatus = value; }
    }

    public static int PopularityStatus
    {
        get { return popularityStatus; }
        set { popularityStatus = value; }
    }
}
