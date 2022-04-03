using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class ScreenDisplay : MonoBehaviour
{
    public TMP_Text text1;
    public TMP_Text text2;


    public void SetText(string time)
    {
        string text = "<size=0.43>SUNDERLAND\n<size=0.4>" + time;
        text1.text = text;
        text2.text = text;
    }

}
