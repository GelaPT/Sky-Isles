using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    private bool isFishing;

    private float timeToCatchFish;
    
    private void StartFishing()
    {
        isFishing = true;
        takeInput = false;

        timeToCatchFish = Random.Range(1f, 4f);
        Invoke(nameof(CatchFish), timeToCatchFish);
        timeToCatchFish += Time.time;
    }

    private void CatchFish()
    {
        Debug.Log("Fish!");
    }
    
    private void StopFishing()
    {
        isFishing = false;
        takeInput = true;

        if (Time.time - timeToCatchFish is < 0.5f and > 0f)
        {
            GiveItemServerRpc(this, "raw_fish");
        }
    }
}
