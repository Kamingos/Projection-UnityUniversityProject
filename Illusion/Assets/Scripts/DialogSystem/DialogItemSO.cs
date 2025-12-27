using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogItem", menuName = "Scriptable Objects/DialogItem")]
public class DialogItemSO : ScriptableObject
{
    public string DialogName;

    public List<DialogItem> DialogList;
}

[Serializable]
public struct DialogItem
{
    public string CharacterName;
    public string Description;

    public int TimeDelay;

    public Texture icon;
}