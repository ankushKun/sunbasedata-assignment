using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupHandler : MonoBehaviour
{
    public TMP_Text nameTxt;
    public TMP_Text points;
    public TMP_Text location;

    public void setDetails(string name, string points, string location)
    {
        this.nameTxt.text = name;
        this.points.text = points;
        this.location.text = location;
    }
}
