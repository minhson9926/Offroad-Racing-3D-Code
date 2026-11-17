using UnityEngine;
public class TaoAmThanhTuDong : MonoBehaviour {
    public bool laNhacNen = false;
    void Start() {
        if (!TryGetComponent<AudioSource>(out AudioSource src)) src = gameObject.AddComponent<AudioSource>();
        if (src.clip != null) return;
        int sr = 44100, samples = sr * 2;
        AudioClip clip = AudioClip.Create("Audio", samples, 1, sr, false);
        float[] data = new float[samples];
        for (int i = 0; i < samples; i++) {
            float t = (float)i / sr;
            data[i] = laNhacNen
                ? Mathf.Sin(2 * Mathf.PI * ((i % (sr / 2) < sr / 4) ? 220f : 277.18f) * t) * 0.08f
                : (Mathf.Sin(2 * Mathf.PI * 55f * t) + 0.5f * Mathf.Sin(2 * Mathf.PI * 110f * t)) * 0.25f;
        }
        clip.SetData(data, 0); src.clip = clip; src.loop = true;
        if (laNhacNen) src.Play();
    }
}