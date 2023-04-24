using System;
using UnityEngine;
using Vuforia;
using UnityEngine.Video;


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

    // If the GameManager component is found, add the video clips to the dictionary
    if (gameManager != null)
    {
                      Debug.Log("gameManager is not null");

        // Load the video clips from the Assets/Videos folder
        VideoClip bear_qr1_video = Resources.Load<VideoClip>("Videos/bear_qr1_video");
        VideoClip bear_qr2_video = Resources.Load<VideoClip>("Videos/bear_qr2_video");

         // Add the video clips to the qrCodeToVideoClip dictionary with unique keys
        if (!gameManager.qrCodeToVideoClip.ContainsKey("bear_qr1"))
        {
            gameManager.qrCodeToVideoClip.Add("bear_qr1", bear_qr1_video);
        }

        if (!gameManager.qrCodeToVideoClip.ContainsKey("bear_qr2"))
        {
            gameManager.qrCodeToVideoClip.Add("bear_qr2", bear_qr2_video);
        }
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

            gameManager.OnQRCodeDetected(mObserverBehaviour.TargetName);
            // gameManager.PlayVideoForQRCode(mObserverBehaviour.TargetName);

        }
    }
}
