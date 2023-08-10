using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorPanel : MonoBehaviour
{
    private Text _text;
    private CanvasGroup _group;


    // Start is called before the first frame update
    private void Start()
    {
        _group = GetComponent<CanvasGroup>();
        _text = GetComponentInChildren<Text>();

        Hide();
    }

    public void ShowError<T>(T message)
    {
        _text.text = message.ToString();
        _group.alpha = 1f;
    }

    public void Hide()
    {
        _group.alpha = 0f;
    }
}
