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
    // Register for OnTargetStatusChanged event
    // mObserverBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
 
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

    //  private void OnTargetStatusChanged(ObserverBehaviour observerBehaviour, TargetStatus targetStatus)
    // {
    //     if (targetStatus.Status != Status.TRACKED && targetStatus.Status != Status.EXTENDED_TRACKED)
    //     {
    //         // The QR code is no longer tracked, hide the augmentations
    //         foreach (Transform child in observerBehaviour.transform)
    //         {
    //             child.gameObject.SetActive(false);
    //         }
    //     }
    //     else
    //     {
    //         // The QR code is tracked again, show the augmentations
    //         foreach (Transform child in observerBehaviour.transform)
    //         {
    //             child.gameObject.SetActive(true);
    //         }
    //     }
    // }
}
