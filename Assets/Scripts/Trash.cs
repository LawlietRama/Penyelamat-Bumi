using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Trash", menuName = "Trash")]

public class Trash : ScriptableObject
{
    public int trashPos;
    public string trashName;
    public Sprite trashIcon;
    public string trashType;
}
