using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextRawLanguage : MonoBehaviour
{
    [SerializeField] private string spanish;
    [SerializeField] private string english;
    [SerializeField] private string portuguese;
    TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        ReadLanguage(KeyStorage.Constants.CurrentLanguage);
    }

    private void OnEnable()
    {
        LanguageService.OnLanguageChanged += ReadLanguage;
    }
    private void OnDisable()
    {
        LanguageService.OnLanguageChanged -= ReadLanguage;
    }

    private void ReadLanguage(TypeLanguage language)
    {
        switch (language)
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
