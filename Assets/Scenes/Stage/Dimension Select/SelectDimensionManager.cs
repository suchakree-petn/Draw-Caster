using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using DrawCaster.DataPersistence;
using QFSW.QC.Actions;
using UnityEngine;

public class SelectDimensionManager : Singleton<SelectDimensionManager>, IDataPersistence
{
    [Header("Loaded Dimension data")]
    [SerializeField] private List<DimensionData> dimensionDatas;
    public List<DimensionData> DimensionDatas => dimensionDatas;
    [SerializeField] private DimensionData current_dimension;
    public DimensionData Current_dimension
    {
        get
        {
            return current_dimension;
        }
        set
        {
            current_dimension = value;
        }
    }

    [Header("Intro Config")]
    [SerializeField] private float intro_zoom_duration;
    [SerializeField] private float intro_zoom_ortho;
    [SerializeField] private AnimationCurve intro_zoom_curve;

    public static Action OnIntroSuccess;
    public static Action OnLoadSuccess;

    [Header("Reference")]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform zoomDestination;

    protected override void InitAfterAwake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        DataPersistenceManager.Instance.LoadGame();
        OnLoadSuccess?.Invoke();
        current_dimension = GetDimensionData("001");
        IntroZoom();
    }

    private void IntroZoom()
    {
        cam.transform.DOMove(zoomDestination.position, intro_zoom_duration).SetEase(intro_zoom_curve);
        cam.DOOrthoSize(intro_zoom_ortho, intro_zoom_duration).SetEase(intro_zoom_curve).OnComplete(() =>
        {
            OnIntroSuccess?.Invoke();
        });
    }
    public DimensionData GetDimensionData(string dimension_id)
    {
        foreach (DimensionData data in dimensionDatas)
        {
            if (dimension_id == data.dimension_id)
            {
                return data;
            }
        }
        return new();
    }
    public void LoadData(GameData data)
    {
        dimensionDatas = data.dimensionData;
    }

    public void SaveData(ref GameData data)
    {
    }
}
