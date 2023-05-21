using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;
using Vuforia;


// [System.Serializable]
// public class QRCodeSoundMapping
// {
//     public string qrCodeType;
//     public List<AudioClip> successSounds;
// }

[System.Serializable]
public class QRCodeSoundMapping
{
    public string qrCodeType;
    public List<AudioClip> successSounds;
    public List<VideoClip> successVideos;

    public QRCodeSoundMapping()
    {
        successSounds = new List<AudioClip>();
        successVideos = new List<VideoClip>();
    }
}

public class GameManager : MonoBehaviour
{
    private Dictionary<string, ObserverBehaviour> scannedQRCodes = new Dictionary<string, ObserverBehaviour>(); 

    public AudioSource successAudioSource;
    public AudioSource failAudioSource;
    public AudioSource randomAudioSource;
    public UnityEngine.Video.VideoPlayer videoPlayer;
    public VideoClip bear_qr1_video;
    public VideoClip bear_qr2_video;
    public TextMeshProUGUI scoreText;
    public RawImage videoDisplay;
    [SerializeField]
    public List<QRCodeSoundMapping> qrCodeSoundMappings = new List<QRCodeSoundMapping>();


    // public List<QRCodeSoundMapping> qrCodeSoundMappings;
    // public List<QRCodeSoundMapping> qrCodeSoundMappings = new List<QRCodeSoundMapping>();
    
    private string lastDetectedQRCode = null;
    private int score = 0;

  
    private void Awake()
    {
        GameObject scoreTextObject = GameObject.Find("Text (TMP)");
        if (scoreTextObject != null)
            {
            scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
            }
    }
    
    private void AssignVideoTexture(VideoPlayer vp)
    {
        videoDisplay.texture = vp.texture;
    }

    private void Start()
    {
         videoDisplay.enabled = false; // Hide video player
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
        Debug.Log("successAudioSourceObject",successAudioSourceObject);
        if (successAudioSourceObject != null)
        {
            successAudioSource = successAudioSourceObject.GetComponent<AudioSource>();
            Debug.Log("successAudioSourceObject");
        }

        GameObject failAudioSourceObject = GameObject.Find("Fail Audio Source");
        Debug.Log("failAudioSourceObject",failAudioSourceObject);
        if (failAudioSourceObject != null)
        {
            failAudioSource = failAudioSourceObject.GetComponent<AudioSource>();
            Debug.Log("failAudioSourceObject");
        }
        GameObject randomAudioSourceObject = GameObject.Find("Random Audio Source");
        if (randomAudioSourceObject != null)
        {
            randomAudioSource = randomAudioSourceObject.GetComponent<AudioSource>();
            Debug.Log("randomAudioSourceObject");
        }
    }

    public void OnQRCodeDetected(string qrCodeName,ObserverBehaviour observerBehaviour)
    {
            // Check if this QR code has already been scanned
            if (scannedQRCodes.ContainsKey(qrCodeName))
            {
                observerBehaviour.enabled = false;
                // Disable the augmentation
                foreach(Transform child in observerBehaviour.transform)
                {
                    child.gameObject.SetActive(false);
                }
                // If it has, just ignore it and return
                return;
            }
            // If this QR code is new, remember it and the corresponding ObserverBehaviour object
            scannedQRCodes[qrCodeName] = observerBehaviour;

            string qrCodeType = qrCodeName.Split('_')[0];
            // if the video is playing, stop it
            videoPlayer.Stop();
            Debug.Log("OnQRCodeDetected qrCodeType: " + qrCodeType);

           
            // Find the matching QRCodeSoundMapping
            QRCodeSoundMapping soundMapping = qrCodeSoundMappings.Find(mapping => mapping.qrCodeType == qrCodeType);

            if (soundMapping != null)
            {
                if (!randomAudioSource.isPlaying) // Add this if statement
                {
                    // Play a random success sound
                    int randomIndex = UnityEngine.Random.Range(0, soundMapping.successSounds.Count);
                    AudioClip randomSound = soundMapping.successSounds[randomIndex];
                    randomAudioSource.clip = randomSound;
                    randomAudioSource.Play();
                    // successAudioSource.clip = randomSound;
                    // successAudioSource.Play();
                }
            }
            else
            {
                Debug.LogWarning("OnQRCodeDetected No QRCodeSoundMapping found for QR code type: " + qrCodeType);
            }

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

            if (soundMapping != null && soundMapping.successVideos.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, soundMapping.successVideos.Count);
                VideoClip randomVideo = soundMapping.successVideos[randomIndex];
                videoPlayer.clip = randomVideo;
                videoPlayer.Prepare();

                // Set up the callback for when the video is prepared
                videoPlayer.prepareCompleted += AssignVideoTexture;
                videoPlayer.Play();

                videoDisplay.enabled = true;  // Show video player
            }


            // // Add if else statement to play different videos
            // // Play the matching video
            // videoPlayer.clip = bear_qr1_video;
            // videoPlayer.Prepare();

            // // Set up the callback for when the video is prepared
            // videoPlayer.prepareCompleted += AssignVideoTexture;
            // videoPlayer.Play();

            //  videoDisplay.enabled = true;  // Show video player
        }
        else
        {
             videoDisplay.enabled = false;  // Hide video player
            if (failAudioSource != null)
            {
                failAudioSource.Play();
                Debug.Log("Fail sound played");
            }
            // Get the ObserverBehaviours for the QR codes that do not match
            ObserverBehaviour lastDetectedQRCodeObserverBehaviour = scannedQRCodes[lastDetectedQRCode];
            ObserverBehaviour qrCodeNameObserverBehaviour = scannedQRCodes[qrCodeName];
        
            // Enable the ObserverBehaviour and its children for the QR codes that do not match
            lastDetectedQRCodeObserverBehaviour.enabled = true;
            foreach (Transform child in lastDetectedQRCodeObserverBehaviour.transform)
            {
                child.gameObject.SetActive(true);
            }
        
            qrCodeNameObserverBehaviour.enabled = true;
            foreach (Transform child in qrCodeNameObserverBehaviour.transform)
            {
                child.gameObject.SetActive(true);
            }
             // Remove the QR codes that do not match
            scannedQRCodes.Remove(lastDetectedQRCode);
            scannedQRCodes.Remove(qrCodeName);
        }

        lastDetectedQRCode = null;

    }



    private bool IsMatchingQRCode(string qrCode1, string qrCode2)
    {
        return qrCode1.Split('_')[0] == qrCode2.Split('_')[0];
    }

    //   public void OnCloseButtonClick()
    // {
    //     // Stop the video
    //     videoPlayer.Stop();

    //     // Hide the video display
    //     videoDisplay.enabled = false;
    //     Debug.Log("Close button has been clicked");
    // }

    public void OnCloseButtonClick()
        {
            // if(scannedQRCodes.ContainsKey(qrCodeName))
            // {
            //     // Disable the ObserverBehaviour
            //     scannedQRCodes[qrCodeName].enabled = false;

            //     // Disable all child GameObjects
            //     foreach(Transform child in scannedQRCodes[qrCodeName].transform)
            //     {
            //         child.gameObject.SetActive(false);
            //     }

              
            // }
              // Stop the video player if it's playing
                if (videoPlayer.isPlaying)
                {
                    videoPlayer.Stop();
                    videoDisplay.enabled = false;  // Hide video player
                }
        }

    public void OnRefreshButtonClick()
    {
        Debug.Log("Refresh clicked: ");

        // For each previously scanned QR code
        foreach (KeyValuePair<string, ObserverBehaviour> entry in scannedQRCodes)
        {
            // Disable the observer behaviour and all its children
            entry.Value.enabled = true;
            foreach (Transform child in entry.Value.transform)
            {
                child.gameObject.SetActive(true);
                 Debug.Log("Enabled child game object: " + child.name);
            }
        }
        // Clear the dictionary of scanned QR codes
        scannedQRCodes.Clear();
        lastDetectedQRCode = null;
        score = 0;
        scoreText.text = "Score: " + score.ToString();
    }


}
