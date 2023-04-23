using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource successAudioSource;
    public AudioSource failAudioSource;

    private string lastDetectedQRCode = null;
    private int score = 0;

    private void Start()
    {
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
        if (lastDetectedQRCode == null)
        {
            lastDetectedQRCode = qrCodeName;
            return;
        }

        if (IsMatchingQRCode(lastDetectedQRCode, qrCodeName))
        {
            score++;
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
    }

    private bool IsMatchingQRCode(string qrCode1, string qrCode2)
    {
        return qrCode1.Split('_')[0] == qrCode2.Split('_')[0];
    }
}
