using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextTimer:MonoBehaviour
{
    [SerializeField] Timer timer;
    TMP_Text textCompo = null;

    private void Awake()
    {
        textCompo = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        timer.OnUpdateTime += DisplayTime;
    }
    private void OnDisable()
    {
        timer.OnUpdateTime -= DisplayTime;
    }

    private void DisplayTime(string obj)
    {
        textCompo.text = obj;
    }

}