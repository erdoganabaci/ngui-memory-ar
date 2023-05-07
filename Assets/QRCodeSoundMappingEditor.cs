#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.Video;
[CustomEditor(typeof(GameManager))]
public class QRCodeSoundMappingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GameManager gameManager = (GameManager)target;

        EditorGUILayout.LabelField("QR Code Sound Mappings", EditorStyles.boldLabel);
        
        EditorGUI.indentLevel++;

        for (int i = 0; i < gameManager.qrCodeSoundMappings.Count; i++)
        {
            EditorGUILayout.BeginVertical("Box");

            EditorGUILayout.BeginHorizontal();
            gameManager.qrCodeSoundMappings[i].qrCodeType = EditorGUILayout.TextField("QR Code Type:", gameManager.qrCodeSoundMappings[i].qrCodeType);
            if (GUILayout.Button("Remove"))
            {
                gameManager.qrCodeSoundMappings.RemoveAt(i);
                i--;
                continue;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Success Sounds", EditorStyles.boldLabel);

            EditorGUI.indentLevel++;
            for (int j = 0; j < gameManager.qrCodeSoundMappings[i].successSounds.Count; j++)
            {
                EditorGUILayout.BeginHorizontal();
                gameManager.qrCodeSoundMappings[i].successSounds[j] = (AudioClip)EditorGUILayout.ObjectField("Clip " + (j + 1), gameManager.qrCodeSoundMappings[i].successSounds[j], typeof(AudioClip), false);
                if (GUILayout.Button("Remove"))
                {
                    gameManager.qrCodeSoundMappings[i].successSounds.RemoveAt(j);
                    j--;
                }
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Add Success Sound"))
            {
                gameManager.qrCodeSoundMappings[i].successSounds.Add(null);
            }

            EditorGUI.indentLevel--;

            EditorGUILayout.LabelField("Success Videos", EditorStyles.boldLabel);

            EditorGUI.indentLevel++;
            for (int j = 0; j < gameManager.qrCodeSoundMappings[i].successVideos.Count; j++)
            {
                EditorGUILayout.BeginHorizontal();
                gameManager.qrCodeSoundMappings[i].successVideos[j] = (VideoClip)EditorGUILayout.ObjectField("Clip " + (j + 1), gameManager.qrCodeSoundMappings[i].successVideos[j], typeof(VideoClip), false);
                if (GUILayout.Button("Remove"))
                {
                    gameManager.qrCodeSoundMappings[i].successVideos.RemoveAt(j);
                    j--;
                }
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Add Success Video"))
            {
                gameManager.qrCodeSoundMappings[i].successVideos.Add(null);
            }

            EditorGUI.indentLevel--;

            EditorGUILayout.EndVertical();
        }

        if (GUILayout.Button("Add QR Code Sound Mapping"))
        {
            gameManager.qrCodeSoundMappings.Add(new QRCodeSoundMapping());
        }

        EditorGUI.indentLevel--;

        EditorGUILayout.Space();

        DrawPropertiesExcluding(serializedObject, "qrCodeSoundMappings");

        serializedObject.ApplyModifiedProperties();
    }
}

#endif
