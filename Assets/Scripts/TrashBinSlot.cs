using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashBinSlot : MonoBehaviour, IDropHandler
{
    public GameObject item
    {
        get
        {
            if (transform.childCount>0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }


    public string jenisTrashBin;
    public bool isFilled;
    public string sampah;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop" + eventData.pointerDrag.transform.name + " di "+ jenisTrashBin);
        if(eventData.pointerDrag != null && isFilled == false)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            isFilled = true;
            sampah = eventData.pointerDrag.transform.name;
            if(jenisTrashBin == "Organic")
            {
                if(eventData.pointerDrag.name == "Sampah")
                    UIManager.instance.sampahPas = true;
            }
        }


    }

    private void Update()
    {
        /*if(isFilled)
            Debug.Log(sampah);*/
    }

}