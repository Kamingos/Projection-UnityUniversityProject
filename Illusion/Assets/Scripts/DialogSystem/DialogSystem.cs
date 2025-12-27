using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] GameObject Panel;

    [SerializeField] RawImage Avatar;

    [SerializeField] TMP_Text Name;

    [SerializeField] TMP_Text Description;

    [SerializeField] DialogItemSOList DialogSOList;


    public void Awake()
    {
        Panel.SetActive(false);
    }

    public void Play(string name)
    {
        Panel.SetActive(true);

        StartCoroutine(PlayDialog(GetItemByName(DialogSOList.DialogsItemSOList, name)));
    }

    IEnumerator PlayDialog(DialogItemSO dialogItemSO)
    {
        if (dialogItemSO == null) yield break;

        foreach (DialogItem dialogItem in dialogItemSO.DialogList)
        {
            ShowDialog(dialogItem);

            yield return new WaitForSeconds(dialogItem.TimeDelay);
        }

        Panel.SetActive(false);
    }

    private void ShowDialog(DialogItem item)
    {
        Name.text = item.CharacterName; ;

        Description.text = item.Description;

        Avatar.texture = item.icon;
    }

    private DialogItemSO GetItemByName(List<DialogItemSO> dialogList, string name)
    {
        foreach (var item in dialogList)
        {
            if (item.DialogName == name)
            {
                return item;
            }
        }
        return null;
    }
}
