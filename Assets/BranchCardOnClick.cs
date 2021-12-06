using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class BranchCardOnClick : MonoBehaviour
{
    public Flowchart main;
    private AudioSource a;
    public void BranchingChoice(int i)
    {
        main.SetIntegerVariable("GenericBranch", i);
        main.SetBooleanVariable("NextStep", true);
        a = GetComponent<AudioSource>();
        a.Play();
    }
}
