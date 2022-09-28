using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<GameObject> uiElements;
    int uiIndex, oldUiIndex;

    void ShowUIScreen(int uiIndex)
    {
        uiElements[oldUiIndex].SetActive(false);
        uiElements[uiIndex].SetActive(true);
        oldUiIndex = uiIndex;
    }
}
