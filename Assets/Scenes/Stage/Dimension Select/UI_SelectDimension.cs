using System;
using System.Collections.Generic;
using DrawCaster.DataPersistence;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SelectDimension : MonoBehaviour
{

    [Header("Dimension Image Icon")]
    [SerializeField] private List<Sprite> dimension_icons = new();

    [Header("Reference")]
    [SerializeField] private Transform prevUI;
    [SerializeField] private Transform currentUI;
    [SerializeField] private Transform nextUI;
    [SerializeField] private GameObject canvas;
    private SelectDimensionManager selectDimensionManager => SelectDimensionManager.Instance;


    private void InitLoadData()
    {
        UpdateUIData(selectDimensionManager.Current_dimension);
    }
    private void ShowDimensions()
    {
        canvas.SetActive(true);
    }
    private void UpdateUIData(DimensionData dimensionData)
    {
        int index = int.Parse(dimensionData.dimension_id);
        string name;
        if (index == 1)
        {
            name = "Tutorial";
        }
        else
        {
            name = (index-1).ToString();
        }
        currentUI.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Dimension " + name;
        float best_play_time1 = dimensionData.best_play_time;
        TimeSpan best_play_time_span1 = TimeSpan.FromSeconds(best_play_time1);
        currentUI.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Best time: " + string.Format("{0:D2}:{1:D2}", best_play_time_span1.Minutes, best_play_time_span1.Seconds);
        currentUI.GetChild(1).GetChild(1).GetComponent<Image>().color = Color.white;
        currentUI.GetChild(1).GetChild(1).GetComponent<Image>().sprite = dimension_icons[index - 1];

        if (dimensionData.dimension_id != "001")
        {
            prevUI.GetChild(0).GetChild(1).GetComponent<Image>().color = Color.white;
            prevUI.GetChild(0).GetChild(1).GetComponent<Image>().sprite = dimension_icons[index - 2];
        }
        else
        {
            prevUI.GetChild(0).GetChild(1).GetComponent<Image>().color = Color.clear;
            prevUI.GetChild(0).GetChild(1).GetComponent<Image>().sprite = null;
        }

        if (dimensionData.dimension_id != "004")
        {
            nextUI.GetChild(0).GetChild(1).GetComponent<Image>().color = Color.white;
            nextUI.GetChild(0).GetChild(1).GetComponent<Image>().sprite = dimension_icons[index];
        }
        else
        {
            nextUI.GetChild(0).GetChild(1).GetComponent<Image>().color = Color.clear;
            nextUI.GetChild(0).GetChild(1).GetComponent<Image>().sprite = null;
        }

    }
    public void OnClickNext()
    {
        int index = int.Parse(selectDimensionManager.Current_dimension.dimension_id);
        if (index == 4 || index == 0)
        {
            return;
        }
        index++;
        selectDimensionManager.Current_dimension = selectDimensionManager.GetDimensionData("00" + index);
        UpdateUIData(selectDimensionManager.Current_dimension);
    }
    public void OnClickPrev()
    {
        int index = int.Parse(selectDimensionManager.Current_dimension.dimension_id);
        if (index == 1 || index == 0)
        {
            return;
        }
        index--;
        selectDimensionManager.Current_dimension = selectDimensionManager.GetDimensionData("00" + index);
        UpdateUIData(selectDimensionManager.Current_dimension);

    }
    private void OnEnable()
    {
        SelectDimensionManager.OnIntroSuccess += ShowDimensions;
        SelectDimensionManager.OnIntroSuccess += InitLoadData;
    }
    private void OnDisable()
    {
        SelectDimensionManager.OnIntroSuccess -= ShowDimensions;
        SelectDimensionManager.OnIntroSuccess -= InitLoadData;
    }
}
