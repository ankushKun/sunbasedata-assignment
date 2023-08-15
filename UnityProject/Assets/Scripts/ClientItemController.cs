using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClientItemController : MonoBehaviour
{
    public string label;
    public bool isManager;
    public int id;

    public TMP_Text labelText;
    public GameObject PopupItem;
    public Button btn;
    Response resCopy;
    public GameObject popup;

    public void setDetails(string label, bool isManager, int id, int index, Response r)
    {
        PopupItem = GameObject.Find("Popup");
        Debug.Log(PopupItem);
        this.label = label;
        this.isManager = isManager;
        this.id = id;
        this.resCopy = r;
        labelText.text = label;
        this.transform.localScale = new Vector3(1, 1, 1);
        this.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        this.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        this.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1);
        this.GetComponent<RectTransform>().offsetMax = new Vector2(0, -100 * index);
        this.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        this.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
        btn.onClick.AddListener(showPopup);
    }

    public void showPopup()
    {
        PopupItem.SetActive(true);
        PopupHandler p = PopupItem.GetComponent<PopupHandler>();
        Debug.Log(p);
        Debug.Log(resCopy.data.Count);
        try
        {
            Data d = resCopy.data[id.ToString()];
            p.setDetails(d.name, "Points: " + d.points.ToString(), "Address: " + d.address);
        }
        catch
        {
            p.setDetails("Client", "Details", "Unavailable");
        }
    }
}
