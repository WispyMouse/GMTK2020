using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static iTween;

public class NotificationPanel : MonoBehaviour
{
    public RectTransform Movable;
    public Text BigText;
    public Text ExplanationText;
    public Image Graphic;

    float showTime = 1.25f;
    float hideTime = 1.25f;

    public void SetValues(string title, string description, Sprite graphic)
    {
        BigText.text = title;
        ExplanationText.text = description;
        Graphic.sprite = graphic;
    }

    public IEnumerator Show()
    {
        Hashtable showTable = new Hashtable();
        showTable.Add("amount", Vector3.down * 125f);
        showTable.Add("time", showTime);
        showTable.Add("easetype", EaseType.easeInCubic);
        iTween.MoveAdd(Movable.gameObject, showTable);
        yield return new WaitForSeconds(showTime);
    }

    public IEnumerator Hide()
    {
        Hashtable hideTable = new Hashtable();
        hideTable.Add("amount", Vector3.up * 125f);
        hideTable.Add("time", hideTime);
        hideTable.Add("easetype", EaseType.easeOutCubic);
        iTween.MoveAdd(Movable.gameObject, hideTable);
        yield return new WaitForSeconds(hideTime);
    }
}
