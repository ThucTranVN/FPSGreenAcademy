using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenGame : BaseScreen
{
    [SerializeField] private Image[] shieldBars;
    [SerializeField] GameObject zoomUI;
    [SerializeField] TMP_Text ammoText;

    public override void Init()
    {
        base.Init();

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.UPDATE_AMMO, OnUpdateAmmoEvent);
            ListenerManager.Instance.Register(ListenType.UPDATE_HEALTH, OnUpdateHealthEvent);
            ListenerManager.Instance.Register(ListenType.UPDATE_ZOOM, OnUpdateZoomEvent);
        }
    }

    public override void Hide()
    {
        base.Hide();
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Unregister(ListenType.UPDATE_AMMO, OnUpdateAmmoEvent);
            ListenerManager.Instance.Unregister(ListenType.UPDATE_HEALTH, OnUpdateHealthEvent);
            ListenerManager.Instance.Unregister(ListenType.UPDATE_ZOOM, OnUpdateZoomEvent);
        }
    }

    public override void Show(object data)
    {
        base.Show(data);
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
}
