using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextRawLanguage : MonoBehaviour
{
    [SerializeField] private string spanish;
    [SerializeField] private string english;
    [SerializeField] private string portuguese;
    TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        switch (KeyStorage.Constants.CurrentLanguage)
        {
            case TypeLanguage.None:
                break;
            case TypeLanguage.Spanish:
                _text.text = spanish;
                break;
            case TypeLanguage.English:
                _text.text = english;
                break;
            case TypeLanguage.Portuguese:
                _text.text = portuguese;
                break;
            default:
                break;
        }
    }
}
