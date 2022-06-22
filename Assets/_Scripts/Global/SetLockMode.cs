using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLockMode : MonoBehaviour
{
    public void setLockModeOff()
    {
        PlayerPrefs.SetInt("TrainingDone", 1);
        print("Unlocked");
    }
}
