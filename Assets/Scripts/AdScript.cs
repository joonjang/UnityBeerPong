using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdScript : MonoBehaviour
{
    string gameId = "3648094";
    public string placementId = "BPBannerAd";
    bool testMode = false;

    public void Start()
    {
        Advertisement.Initialize(gameId, testMode);
        StartCoroutine(ShowBannerWhenReady());

    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(placementId))
        {
            yield return new WaitForSeconds(5);
        }
        Advertisement.Banner.Show(placementId);
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
    }
}
