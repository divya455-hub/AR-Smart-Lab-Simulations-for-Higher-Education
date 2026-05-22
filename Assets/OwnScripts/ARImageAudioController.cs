using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARImageAudioController : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            HandleTrackedImage(trackedImage);
        }
        foreach (var trackedImage in eventArgs.updated)
        {
            HandleTrackedImage(trackedImage);
        }
    }

    void HandleTrackedImage(ARTrackedImage trackedImage)
    {
        SerializableGuid serialGuid = ToSerializableGuid(trackedImage.referenceImage.guid);

        PlayAudioForImage(trackedImage.name);
    }

    void PlayAudioForImage(string imageName)
    {
       
        Debug.Log("Play audio for: " + imageName);
        
    }

    
    public static SerializableGuid ToSerializableGuid(Guid guid)
    {
        byte[] bytes = guid.ToByteArray();
        ulong low = BitConverter.ToUInt64(bytes, 0);   
        ulong high = BitConverter.ToUInt64(bytes, 8);  
        return new SerializableGuid(high, low);
    }
}