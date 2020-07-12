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

    float showTime = .75f;
    float hideTime = .5f;
    int shownIndex = 0;

    public void SetValues(string title, string description, Sprite graphic)
    {
        BigText.text = title;
        ExplanationText.text = description;
        Graphic.sprite = graphic;
    }

    public IEnumerator Show(int index)
    {
        shownIndex = index;
        Hashtable showTable = new Hashtable();
        showTable.Add("amount", Vector3.down * 125f * shownIndex);
        showTable.Add("time", showTime);
        showTable.Add("easetype", EaseType.easeInBack);
        iTween.MoveAdd(Movable.gameObject, showTable);
        yield return new WaitForSeconds(showTime);
    }

    public IEnumerator Hide()
    {
        Hashtable hideTable = new Hashtable();
        hideTable.Add("amount", Vector3.up * 125f * shownIndex);
        hideTable.Add("time", hideTime);
        hideTable.Add("easetype", EaseType.easeOutBack);
        iTween.MoveAdd(Movable.gameObject, hideTable);
        yield return new WaitForSeconds(hideTime);
    }
}
