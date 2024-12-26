using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Zenject;

public class TimeReward : MonoBehaviour
{
    [SerializeField] private Button _rewardButton;
    [SerializeField] private Image _notificationImage;
    private int _rewardIntervalMinutes = 1;

    [SerializeField] private int _rewardSize;
    private int _rewardMultiplier;

    [Inject] private GlobalEvent _globalEvent;

    private void Start() 
    {
        _rewardMultiplier = PlayerPrefs.GetInt("CurrentLevel", 1);
        StartCoroutine(UpdateCoroutine());
    } 

    public bool CanClaimReward()
    {
        string savedTime = PlayerPrefs.GetString("LastTime", "");
        if(string.IsNullOrEmpty(savedTime)) return true;

        DateTime lastRewardTime = DateTime.Parse(savedTime);
        DateTime currentTime = DateTime.UtcNow;

        TimeSpan timeSinceLastReward = currentTime - lastRewardTime;
        return timeSinceLastReward.TotalHours >= _rewardIntervalMinutes;
    }

    private IEnumerator UpdateCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if(CanClaimReward())
            {
                _notificationImage.enabled = true;
                _rewardButton.interactable = true;
            }  
        }
    }

    public void ClaimReward()
    {
        PlayerPrefs.SetString("LastTime", DateTime.UtcNow.ToString());
        PlayerPrefs.Save();

        _notificationImage.enabled = false;
        _rewardButton.interactable = false;

        _globalEvent.onUpdateBalance?.Invoke(_rewardSize * _rewardMultiplier, true);
        _globalEvent.onUpdateDisplayInfo?.Invoke();
    }
}
