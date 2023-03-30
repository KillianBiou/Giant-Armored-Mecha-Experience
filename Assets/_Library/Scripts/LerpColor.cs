using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LerpColor : MonoBehaviour
{

    [SerializeField]
    private Color c1;

    [SerializeField]
    private Color c2;

    [SerializeField]
    private Slider sl;

    [SerializeField]
    private Image fill;

    public void UpdateColor()
    {
        fill.color = Color.Lerp(c2, c1, sl.value);
    }

}
