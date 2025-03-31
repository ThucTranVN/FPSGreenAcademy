using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ScreenGame : BaseScreen
{
    [SerializeField] private Image[] shieldBars;
    [SerializeField] GameObject zoomUI;
    [SerializeField] TMP_Text ammoText;
    [SerializeField] TMP_Text currentMissionName;
    [SerializeField] GameObject missionProgressPref;

    private List<TMP_Text> missionProgressViews = new();

    public override void Init()
    {
        base.Init();

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.UPDATE_AMMO, OnUpdateAmmoEvent);
            ListenerManager.Instance.Register(ListenType.UPDATE_HEALTH, OnUpdateHealthEvent);
            ListenerManager.Instance.Register(ListenType.UPDATE_ZOOM, OnUpdateZoomEvent);
            ListenerManager.Instance.Register(ListenType.UPDATE_COUNT_ENEMY, OnUpdateCountEnemy);
            ListenerManager.Instance.Register(ListenType.UPDATE_MISSION, OnUpdateMission);
        }

        InitMission();
    }

    public override void Hide()
    {
        base.Hide();
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Unregister(ListenType.UPDATE_AMMO, OnUpdateAmmoEvent);
            ListenerManager.Instance.Unregister(ListenType.UPDATE_HEALTH, OnUpdateHealthEvent);
            ListenerManager.Instance.Unregister(ListenType.UPDATE_ZOOM, OnUpdateZoomEvent);
            ListenerManager.Instance.Unregister(ListenType.UPDATE_COUNT_ENEMY, OnUpdateCountEnemy);
            ListenerManager.Instance.Unregister(ListenType.UPDATE_MISSION, OnUpdateMission);
        }
    }

    public override void Show(object data)
    {
        base.Show(data);

        foreach (var item in shieldBars)
        {
            item.gameObject.SetActive(true);
        }

        InitMission();
    }

    private void OnUpdateAmmoEvent(object value)
    {
        if(value != null)
        {
            if(value is int currentAmmo)
            {
               ammoText.text = currentAmmo.ToString("D2");
            }
        }
    }

    private void OnUpdateHealthEvent(object value)
    {
        if(value != null)
        {
            if(value is int currentHealth)
            {
                for (int i = 0; i < shieldBars.Length; i++)
                {
                    if (i < currentHealth)
                    {
                        shieldBars[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        shieldBars[i].gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void OnUpdateZoomEvent(object value)
    {
        if(value != null)
        {
            if(value is bool zoomState)
            {
                zoomUI.SetActive(zoomState);
            }
        }
    }

    private void InitMission()
    {
        if (MissionManager.HasInstance)
        {
            for (int i = 0; i < MissionManager.Instance.MissionData.Count; i++)
            {
                GameObject missonGo = Instantiate(missionProgressPref, missionProgressPref.transform.parent);
                missonGo.SetActive(true);
                TMP_Text txtProgress = missonGo.GetComponent<TMP_Text>();
                txtProgress.text = $"Kill: 0 / {MissionManager.Instance.MissionData[i].TotalEnemy}";
                missionProgressViews.Add(txtProgress);
            }

            currentMissionName.text = MissionManager.Instance.CurrentMission.MissionName;
        }
    }

    private void OnUpdateCountEnemy(object value)
    {
        if(value != null)
        {
            if(value is int countEnemyDead)
            {
                if(missionProgressViews?.Count > 0)
                {
                    missionProgressViews[MissionManager.Instance.MissionIndex].text = $"Kill: {countEnemyDead} / {MissionManager.Instance.CurrentMission.TotalEnemy}";
                }
            }
        }
    }

    private void OnUpdateMission(object value)
    {
        if(value != null)
        {
            if(value is MissionSO currentMission)
            {
                currentMissionName.text = currentMission.MissionName;
            }
        }
    }
}
