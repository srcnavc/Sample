using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mayor : MonoBehaviour
{
    [SerializeField] bool isIdleWoodEnabled = false;
    [SerializeField] bool isBlacksmithEnabled = false;
    
    public static string Stand = "isIdleWoodEnabled";
    public static string WeaponKey = "isBlacksmithEnabled";

    int tempInt;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerPrefs.SetInt(Stand, 0);
        PlayerPrefs.SetInt(WeaponKey, 1);
        //SetPlayerPrefs();

    }

    private void SetPlayerPrefs()
    {
        if (PlayerPrefs.HasKey(Stand))
        {
            tempInt = PlayerPrefs.GetInt(Stand);
            if (tempInt == 0)
                isIdleWoodEnabled = false;
            else
                isIdleWoodEnabled = true;
        }
        else
        {
            PlayerPrefs.SetInt(Stand, 0);
        }

        if (PlayerPrefs.HasKey(WeaponKey))
        {
            tempInt = PlayerPrefs.GetInt(WeaponKey);
            if (tempInt == 0)
                isBlacksmithEnabled = false;
            else
                isBlacksmithEnabled = true;
        }
        else
        {
            PlayerPrefs.SetInt(WeaponKey, 0);
        }
    }

}
