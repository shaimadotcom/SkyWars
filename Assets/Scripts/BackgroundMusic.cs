using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundAudioSource;  // مرجع للصوت الخلفي

    void Start()
    {
        if (backgroundAudioSource == null)
        {
            Debug.LogError("AudioSource is not assigned in the Inspector!");
            return;  // توقف عن تنفيذ الكود إذا لم يتم تعيين الـ AudioSource
        }

        if (!backgroundAudioSource.isPlaying)
        {
            Debug.Log("Starting background music...");
            backgroundAudioSource.Play();  // يبدأ تشغيل الصوت
        }
        else
        {
            Debug.Log("Background music is already playing.");
        }
    }
}
