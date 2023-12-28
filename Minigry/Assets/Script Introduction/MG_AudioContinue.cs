using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_AudioContinue : MonoBehaviour
{
    public static MG_AudioContinue Instance;
    public bool useSharedMusic = true;

    public bool getSharedMusic()
    {
        return useSharedMusic;
    }
}
