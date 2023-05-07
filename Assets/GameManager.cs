using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public AudioSource successAudioSource;
    public AudioSource failAudioSource;
    public UnityEngine.Video.VideoPlayer videoPlayer;
    public VideoClip bear_qr1_video;
    public VideoClip bear_qr2_video;
    public TextMeshProUGUI scoreText;


    private string lastDetectedQRCode = null;
    private int score = 0;

        // private Dictionary<string, string> qrCodeToVideoURL = new Dictionary<string, string>
    // {
    //     { "bear_qr1", "https://drive.google.com/file/d/1fCDPJNvxnWHtwry9srx1MXiCpHRptbwT/preview" },
    //     { "bear_qr2", "https://drive.google.com/file/d/1fCDPJNvxnWHtwry9srx1MXiCpHRptbwT/preview" },
    //     // Add more mappings here
    // };

     [SerializeField]
    public Dictionary<string, VideoClip> qrCodeToVideoClip = new Dictionary<string, VideoClip>
    {
        { "bear_qr1", null },
        { "bear_qr2", null },
        // Add more mappings here
    };

    private void Awake()
{
    GameObject scoreTextObject = GameObject.Find("Text (TMP)");
    if (scoreTextObject != null)
    {
        scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
    }
}


    private void Start()
    {
        // scoreText.text = "Score: " + score.ToString();
        // scoreText.text = "10";
     if (scoreText != null)
       {
        //    scoreText.text = "10";
        scoreText.text = "Score: " + score.ToString();

       }
       else
       {
           Debug.LogError("ScoreText not found or assigned.");
       }
    
         // Find the "Video Plane" object
        Transform videoPlaneTransform = transform.Find("Video Plane");

        if (videoPlaneTransform == null)
        {
            Debug.LogError("Video Plane not found!");
            return;
        }

        // Get the renderer component of the "Video Plane" object
        Renderer videoPlaneRenderer = videoPlaneTransform.GetComponent<Renderer>();

        if (videoPlaneRenderer == null)
        {
            Debug.LogError("Renderer component not found on Video Plane!");
            return;
        }

        // Assign the video plane's renderer to the video player's targetMaterialRenderer
        videoPlayer.targetMaterialRenderer = videoPlaneRenderer;

        GameObject successAudioSourceObject = GameObject.Find("Success Audio Source");
        if (successAudioSourceObject != null)
        {
            successAudioSource = successAudioSourceObject.GetComponent<AudioSource>();
        }

        GameObject failAudioSourceObject = GameObject.Find("Fail Audio Source");
        if (failAudioSourceObject != null)
        {
            failAudioSource = failAudioSourceObject.GetComponent<AudioSource>();
        }
    }

    public void OnQRCodeDetected(string qrCodeName)
    {
        if (lastDetectedQRCode == null || lastDetectedQRCode == qrCodeName)
        {
            lastDetectedQRCode = qrCodeName;
            return;
        }
        Debug.Log("OnQRCodeDetected lastDetectedQRCode: " + lastDetectedQRCode);
        Debug.Log("OnQRCodeDetected qrCodeName: " + qrCodeName);
        Debug.Log("OnQRCodeDetected is match: " + IsMatchingQRCode(lastDetectedQRCode, qrCodeName));

        
        if (IsMatchingQRCode(lastDetectedQRCode, qrCodeName))
        {
            score++;
            scoreText.text = "Score: " + score.ToString();

            Debug.Log("Score: " + score);

            if (successAudioSource != null)
            {
                successAudioSource.Play();
                Debug.Log("Success sound played");
            }
        }
        else
        {
            if (failAudioSource != null)
            {
                failAudioSource.Play();
                Debug.Log("Fail sound played");
            }
        }

        lastDetectedQRCode = null;
        // PlayVideo(qrCodeName);

    }

    private bool IsMatchingQRCode(string qrCode1, string qrCode2)
    {
        return qrCode1.Split('_')[0] == qrCode2.Split('_')[0];
    }


private void PlayVideo(string qrCodeName)
{
    if (qrCodeToVideoClip.ContainsKey(qrCodeName))
    {
        VideoClip videoClip = qrCodeToVideoClip[qrCodeName];
            Debug.Log("videoClip name: "+videoClip);
            Debug.Log("qrCodeToVideoClip name: "+qrCodeToVideoClip);
            Debug.Log("qrCodeName name: "+qrCodeName);

        if (videoClip != null)
        {
            videoPlayer.clip = videoClip;
            videoPlayer.Play();
        }
        else
        {
            // The video clip is null, so load it from the Resources folder
            videoClip = Resources.Load<VideoClip>("Videos/" + qrCodeName + "_video");
            if (videoClip != null)
            {
                qrCodeToVideoClip[qrCodeName] = videoClip;
                videoPlayer.clip = videoClip;
                videoPlayer.Play();
            }
            else
            {
                Debug.LogError("VideoClip not found for QR code: " + qrCodeName);
            }
        }
    }
    else
    {
        Debug.LogError("QR code not found in the dictionary: " + qrCodeName);
    }
}


// private void PlayVideo(string qrCodeName)
// {
//     if (qrCodeToVideoClip.ContainsKey(qrCodeName))
//     {
//         VideoClip videoClip = qrCodeToVideoClip[qrCodeName];

//         if (videoClip != null)
//         {
//             videoPlayer.clip = videoClip;
//             videoPlayer.Play();
//         }
//         else
//         {
//             Debug.LogError("VideoClip not found for QR code: " + qrCodeName);
//         }
//     }
//     else
//     {
//         Debug.LogError("QR code not found in the dictionary: " + qrCodeName);
//     }
// }

//     public void PlayVideoForQRCode(string qrCode)
// {
//      Debug.Log("video played");

//     string videoURL = GetVideoURLForQRCode(qrCode);
//     if (!string.IsNullOrEmpty(videoURL))
//     {
//         videoPlayer.url = videoURL;
//         videoPlayer.Play();
//     }
//     else
//     {
//         Debug.LogError("No video found for QR code: " + qrCode);
//     }
// }



    // private string GetVideoURLForQRCode(string qrCode)
    // {
    //     if (qrCodeToVideoURL.TryGetValue(qrCode, out string videoURL))
    //     {
    //         return videoURL;
    //     }
    //     else
    //     {
    //         return null;
    //     }
    // }
}
