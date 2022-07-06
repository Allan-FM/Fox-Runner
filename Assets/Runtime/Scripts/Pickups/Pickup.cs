using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Pickup : MonoBehaviour
{
    [SerializeField] private AudioClip pickupAudio;

    [SerializeField] private GameObject model;

    public void OnPickedUp()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioUtility.PlayAudioCue(audioSource, pickupAudio);

        //TODO: Mover toque de áudio para um AudioService
        model.SetActive(false);
        Destroy(gameObject, pickupAudio.length);
    }
}
