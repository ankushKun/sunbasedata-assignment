using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    public void HidePopup()
    {
        transform.DOLocalMoveY(-1000, 0.5f);
        GameObject.Find("Scroll View").transform.DOLocalMoveX(-940, 0.5f);
        GameObject.Find("FilteringDropdown").transform.DOLocalMoveX(940, 0.5f);
    }
}
