using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSetter : MonoBehaviour
{
    public Color[] colors;
   public Color SetColor(int blockValue)
    {
        // 1 2 4 8 16 32  recieving
        //0 1 2 3 4 5 6   converting 

        // Convert the number to an index
        int index = Mathf.RoundToInt(Mathf.Log(blockValue, 2));

        // Assign color based on the index
        int colorIndex = Mathf.Clamp(index, 0, colors.Length - 1);

        return colors[colorIndex];
    }
}
