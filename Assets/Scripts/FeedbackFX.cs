using UnityEngine;
using System.Collections;

public class FeedbackFX : MonoBehaviour
{
    [Header("Audio")]
    private AudioSource audioSource;
    private AudioClip sliceSound;
    private float lastSoundTime = 0f;
    
    [Header("Particles")]
    private ParticleSystem sliceParticles;
    private ParticleSystem confettiParticles;
    private ParticleSystem.EmitParams emitParams;
    
    void Awake()
    {
        SetupAudio();
        SetupParticles();
    }
    
    void SetupAudio()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = 0.7f;
        
        // Create synthetic crunch sound
        sliceSound = CreateCrunchSound();
    }
    
    AudioClip CreateCrunchSound()
    {
        int sampleRate = 44100;
        float duration = 0.1f;
        int sampleLength = Mathf.RoundToInt(sampleRate * duration);
        
        AudioClip clip = AudioClip.Create("Crunch", sampleLength, 1, sampleRate, false);
        float[] samples = new float[sampleLength];
        
        // Generate white noise with envelope
        for (int i = 0; i < sampleLength; i++)
        {
            float t = (float)i / sampleLength;
            float envelope = Mathf.Exp(-t * 10f); // Quick decay
            samples[i] = (Random.value * 2f - 1f) * envelope * 0.3f;
            
            // Add some low frequency content for crunch
            samples[i] += Mathf.Sin(t * 100f * Mathf.PI) * envelope * 0.2f;
        }
        
        clip.SetData(samples, 0);
        return clip;
    }
    
    void SetupParticles()
    {
        // Slice particles (small debris)
        GameObject sliceParticleGO = new GameObject("SliceParticles");
        sliceParticleGO.transform.SetParent(transform);
        sliceParticles = sliceParticleGO.AddComponent<ParticleSystem>();
        
        var main = sliceParticles.main;
        main.startLifetime = 0.5f;
        main.startSpeed = 3f;
        main.startSize = 0.1f;
        main.maxParticles = 1000;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        
        var emission = sliceParticles.emission;
        emission.enabled = false; // Manual emission only
        
        var shape = sliceParticles.shape;
        shape.enabled = false;
        
        var velocityOverLifetime = sliceParticles.velocityOverLifetime;
        velocityOverLifetime.enabled = true;
        velocityOverLifetime.space = ParticleSystemSimulationSpace.World;
        velocityOverLifetime.y = new ParticleSystem.MinMaxCurve(-2f); // Gravity
        
        // Confetti particles (layer clear celebration)
        GameObject confettiGO = new GameObject("ConfettiParticles");
        confettiGO.transform.SetParent(transform);
        confettiParticles = confettiGO.AddComponent<ParticleSystem>();
        
        var confettiMain = confettiParticles.main;
        confettiMain.startLifetime = 2f;
        confettiMain.startSpeed = 5f;
        confettiMain.startSize = 0.3f;
        confettiMain.maxParticles = 500;
        
        var confettiEmission = confettiParticles.emission;
        confettiEmission.enabled = false;
        
        var confettiShape = confettiParticles.shape;
        confettiShape.shapeType = ParticleSystemShapeType.Cone;
        confettiShape.angle = 45f;
        confettiShape.radius = 0.1f;
        
        // Create simple particle texture
        CreateParticleTexture();
    }
    
    void CreateParticleTexture()
    {
        Texture2D texture = new Texture2D(32, 32);
        Vector2 center = Vector2.one * 16;
        
        for (int y = 0; y < 32; y++)
        {
            for (int x = 0; x < 32; x++)
            {
                float dist = Vector2.Distance(new Vector2(x, y), center) / 16f;
                float alpha = dist < 1f ? 1f - dist : 0f;
                texture.SetPixel(x, y, new Color(1, 1, 1, alpha));
            }
        }
        
        texture.Apply();
        
        var sliceRenderer = sliceParticles.GetComponent<ParticleSystemRenderer>();
        sliceRenderer.material = new Material(Shader.Find("Sprites/Default"));
        sliceRenderer.material.mainTexture = texture;
        
        var confettiRenderer = confettiParticles.GetComponent<ParticleSystemRenderer>();
        confettiRenderer.material = new Material(Shader.Find("Sprites/Default"));
        confettiRenderer.material.mainTexture = texture;
    }
    
    public void PlayHaptic()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
        Handheld.Vibrate();
        #endif
    }
    
    public void PlaySliceSound(float pitch = 1f)
    {
        // Prevent sound spam
        if (Time.time - lastSoundTime < 0.05f) return;
        
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(sliceSound, 0.3f);
        lastSoundTime = Time.time;
    }
    
    public void SpawnSliceParticles(Vector3 position, Color color)
    {
        emitParams.position = position;
        emitParams.startColor = color;
        emitParams.startSize = Random.Range(0.05f, 0.15f);
        emitParams.startLifetime = Random.Range(0.3f, 0.6f);
        emitParams.velocity = Random.insideUnitCircle * 2f;
        
        sliceParticles.Emit(emitParams, 3);
    }
    
    public void SpawnSliceBurst(Vector3 position)
    {
        // Larger burst effect
        for (int i = 0; i < 10; i++)
        {
            emitParams.position = position;
            emitParams.velocity = Random.insideUnitCircle.normalized * Random.Range(2f, 4f);
            sliceParticles.Emit(emitParams, 1);
        }
    }
    
    public void PlayLayerClearEffect(Vector3 position, Color color)
    {
        // Haptic pulse
        PlayHaptic();
        
        // Success sound (higher pitch)
        PlaySliceSound(1.5f);
        
        // Confetti burst
        confettiParticles.transform.position = position;
        
        var main = confettiParticles.main;
        main.startColor = color;
        
        confettiParticles.Emit(50);
        
        // Camera shake
        StartCoroutine(CameraShake(0.2f, 0.1f));
    }
    
    public void PlayVictoryEffect()
    {
        // Big celebration
        PlaySliceSound(2f);
        
        // Multiple confetti bursts
        StartCoroutine(VictorySequence());
    }
    
    IEnumerator CameraShake(float duration, float magnitude)
    {
        Camera cam = Camera.main;
        Vector3 originalPos = cam.transform.position;
        
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            
            cam.transform.position = originalPos + new Vector3(x, y, 0);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        cam.transform.position = originalPos;
    }
    
    IEnumerator VictorySequence()
    {
        for (int i = 0; i < 5; i++)
        {
            confettiParticles.transform.position = new Vector3(
                Random.Range(-3f, 3f),
                Random.Range(-2f, 2f),
                0
            );
            
            var main = confettiParticles.main;
            main.startColor = new Color(Random.value, Random.value, Random.value, 1f);
            
            confettiParticles.Emit(30);
            PlayHaptic();
            
            yield return new WaitForSeconds(0.2f);
        }
    }
}