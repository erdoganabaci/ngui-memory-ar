using System;
using UnityEngine;
using Vuforia;

public class CustomTrackableEventHandler : DefaultObserverEventHandler
{
    public GameManager gameManager;

    protected override void Start()
    {
        base.Start();

        GameObject gameManagerObject = GameObject.Find("GameManager");
        if (gameManagerObject != null)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();
        }
    }

    protected override void OnTrackingFound()
    {
          Debug.Log("OnTrackingFound");
        base.OnTrackingFound();

        if (gameManager != null)
        {
                      Debug.Log(" before OnQRCodeDetected");

            gameManager.OnQRCodeDetected(mObserverBehaviour.TargetName);
        }
    }
}
