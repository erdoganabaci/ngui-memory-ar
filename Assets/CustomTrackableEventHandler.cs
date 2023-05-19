using System;
using UnityEngine;
using Vuforia;


public class CustomTrackableEventHandler : DefaultObserverEventHandler
{
    public GameManager gameManager;

  protected override void Start()
{
    base.Start();

    // Find the GameManager object in the scene
    GameObject gameManagerObject = GameObject.Find("GameManager");
    if (gameManagerObject != null)
    {
        // Get the GameManager component from the object
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
                      Debug.Log(" before OnQRCodeDetected qr name"+ mObserverBehaviour.TargetName);

            gameManager.OnQRCodeDetected(mObserverBehaviour.TargetName,mObserverBehaviour);
        }
    }
}
