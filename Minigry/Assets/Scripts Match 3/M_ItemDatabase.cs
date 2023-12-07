using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_ItemDatabase : MonoBehaviour
{
    public static M_Item[] Items { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] private static void Initialize() { Items = Resources.LoadAll<M_Item>("Match 3/Items/");
    }


}
