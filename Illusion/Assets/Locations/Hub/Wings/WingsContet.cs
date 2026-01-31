using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class WingsContet : MonoBehaviour
{
    [SerializeField] protected TMP_Text header;
    [SerializeField] protected TMP_Text text;

    [SerializeReference, SubclassSelector] WingscontentAbstract wingcontent;

    private void Awake()
    {
        wingcontent.Init(header, text, GetComponent<MonoBehaviour>());

        wingcontent.Launch();
    }
}

[Serializable]
public abstract class WingscontentAbstract
{
    protected TMP_Text _header;
    protected TMP_Text _text;

    protected MonoBehaviour _obj;

    public virtual void Init(TMP_Text header, TMP_Text text, MonoBehaviour obj)
    {
        _header = header;
        _text = text;
        _obj = obj;
    }

    public abstract void Launch();
}

[Serializable]
public class WingsDataContent : WingscontentAbstract
{
    private Coroutine coroutine;

    public override void Launch()
    {
        _header.text = "Дата";

        coroutine = _obj.StartCoroutine(cycle());
    }

    IEnumerator cycle()
    {
        WaitForSeconds wfs = new(1);

        while (true)
        {
            _text.text = DateTime.Now.ToString();

            yield return wfs;
        }
    }
}

[Serializable]
public class WingsEmojiContent : WingscontentAbstract
{
    private Coroutine coroutine;

    public override void Launch()
    {
        _header.text = "Привет!";

        coroutine = _obj.StartCoroutine(cycle());
    }

    IEnumerator cycle()
    {
        WaitForSeconds wfs = new WaitForSeconds(1.5f);

        while (true)
        {
            _text.text = "0_0";

            yield return wfs;
            
            _text.text = "*_*";

            yield return wfs;
        }
    }
}
