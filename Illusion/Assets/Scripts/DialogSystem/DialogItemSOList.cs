using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogItemSOList", menuName = "Scriptable Objects/DialogItemSOList")]
public class DialogItemSOList : ScriptableObject
{
    public List<DialogItemSO> DialogsItemSOList;
}
