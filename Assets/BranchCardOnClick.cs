using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class BranchCardOnClick : MonoBehaviour
{
    public Flowchart main;
    public void BranchingChoice(int i)
    {
        main.SetIntegerVariable("GenericBranch", i);
        main.SetBooleanVariable("NextStep", true);
    }
}
