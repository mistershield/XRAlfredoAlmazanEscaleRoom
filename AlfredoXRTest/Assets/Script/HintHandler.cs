using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HintHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI uiText;
    [SerializeField] private List<string> hints = new List<string>();

    private int currentIndex = 0;
    private int currentIndexMaxValue;
    void Start()
    {
        currentIndexMaxValue = hints.Count - 1;
        UpdateHintText();
    }
    private void UpdateHintText()
    {
        uiText.text = hints[currentIndex];
    }
    public void ChangeHintText(int direction)
    {
        if((currentIndex > 0 && direction < 0) || (currentIndex < currentIndexMaxValue && direction > 0))
        {
            currentIndex += direction;
            UpdateHintText();
        }
    }
}
