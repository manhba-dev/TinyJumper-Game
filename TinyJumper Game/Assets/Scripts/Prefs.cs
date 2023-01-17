using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefs 
{
    // tao 1 bien staic bien tinh
    public static int bestScore
    {
        // diem so cu mak nho hon diem so dua vao value
        set{
            if(PlayerPrefs.GetInt(PrefConsts.BEST_SCORE,0) < value)
            {
                PlayerPrefs.SetInt(PrefConsts.BEST_SCORE,value);
            }
        }

        get => PlayerPrefs.GetInt(PrefConsts.BEST_SCORE,0);
    }
}
