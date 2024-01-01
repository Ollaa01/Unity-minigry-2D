using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ResetPlayerPrefs : MonoBehaviour
{
    public void ResetAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs reset.");
    }
}
