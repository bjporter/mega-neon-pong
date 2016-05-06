using UnityEngine;
using System.Collections;

public class RippleEffectEdit : MonoBehaviour
{
    public AnimationCurve waveform = new AnimationCurve(
        new Keyframe(0.00f, 0.50f, 0, 0),
        new Keyframe(0.05f, 1.00f, 0, 0),
        new Keyframe(0.15f, 0.10f, 0, 0),
        new Keyframe(0.25f, 0.80f, 0, 0),
        new Keyframe(0.35f, 0.30f, 0, 0),
        new Keyframe(0.45f, 0.60f, 0, 0),
        new Keyframe(0.55f, 0.40f, 0, 0),
        new Keyframe(0.65f, 0.55f, 0, 0),
        new Keyframe(0.75f, 0.46f, 0, 0),
        new Keyframe(0.85f, 0.52f, 0, 0),
        new Keyframe(0.99f, 0.50f, 0, 0)
    );

    [Range(0.01f, 1.0f)]
    public float refractionStrength = 0.5f;

    public Color reflectionColor = Color.gray;

    [Range(0.01f, 1.0f)]
    public float reflectionStrength = 0.7f;

    [Range(1.0f, 3.0f)]
    public float waveSpeed = 1f;

    [Range(0.0f, 2.0f)]
    public float dropInterval = 3.5f;

    [SerializeField, HideInInspector]
    Shader shader;

    //Test stuff
    private bool flag = true;
    Vector2 staticPosition = new Vector2(0,0);

    [SerializeField]
    private GameObject positionObject;

    [SerializeField]
    private Camera camera;

    class Droplet
    {
        Vector2 position;
        float time;

        public Droplet()
        {
            time = 1000;
        }

        public void Reset(Vector3 new_pos)
        {
            //position = ;
            //Debug.DrawLine(position, new Vector3(position.x + 10, position.y + 10, position.z + 10));
            Debug.Log(new_pos.ToString());
            position = new Vector2(new_pos.x, new_pos.y);
            //position = new Vector2(Random.value, Random.value);
            time = 0;
        }

        public void Update()
        {
            time += Time.deltaTime;
        }

        public Vector4 MakeShaderParameter(float aspect)
        {
            return new Vector4(position.x * aspect, position.y, time, 0);
        }
    }

    Droplet[] droplets;
    Texture2D gradTexture;
    Material material;
    float timer;
    int dropCount;

    void UpdateShaderParameters()
    {
        var c = GetComponent<Camera>();

        material.SetVector("_Drop1", droplets[0].MakeShaderParameter(c.aspect));
        //material.SetVector("_Drop2", droplets[1].MakeShaderParameter(c.aspect));
        //material.SetVector("_Drop3", droplets[2].MakeShaderParameter(c.aspect));

        waveSpeed = 0.25f;
        //dropInterval = 5f;

        material.SetColor("_Reflection", reflectionColor);
        material.SetVector("_Params1", new Vector4(c.aspect, 1, 1 / waveSpeed, 0));
        material.SetVector("_Params2", new Vector4(1, 1 / c.aspect, refractionStrength, reflectionStrength));
    }

    void Awake()
    {
        droplets = new Droplet[3];
        droplets[0] = new Droplet();
        //droplets[1] = new Droplet();
        //droplets[2] = new Droplet();

        gradTexture = new Texture2D(2048, 1, TextureFormat.Alpha8, false);
        gradTexture.wrapMode = TextureWrapMode.Clamp;
        gradTexture.filterMode = FilterMode.Bilinear;
        for (var i = 0; i < gradTexture.width; i++)
        {
            var x = 1.0f / gradTexture.width * i;
            var a = waveform.Evaluate(x);
            gradTexture.SetPixel(i, 0, new Color(a, a, a, a));
        }
        gradTexture.Apply();

        material = new Material(shader);
        material.hideFlags = HideFlags.DontSave;
        material.SetTexture("_GradTex", gradTexture);

        UpdateShaderParameters();
    }

    void Update()
    {
        if (dropInterval > 0)
        {
            timer += Time.deltaTime;
            while (timer > dropInterval)
            {
                Emit();
                timer -= dropInterval;
            }
        }

        //foreach (var d in droplets) d.Update();
        droplets[0].Update();

        refractionStrength -= .0033f;
        reflectionStrength -= .0033f;

        if (refractionStrength <= 0) refractionStrength = 0;
        if (reflectionStrength <= 0) reflectionStrength = 0;
        UpdateShaderParameters();
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }

    private bool emitStarted = false;

    public void Emit()
    {
        if(emitStarted) {
            //droplets[0].Reset(new Vector3(1000f,1000f,1000f));
        }

        //droplets[dropCount++ % droplets.Length].Reset();
        if(flag) {
            emitStarted = true;
            Vector3 p = GetComponent<Camera>().WorldToViewportPoint(positionObject.transform.position);
            droplets[0].Reset(p);
            flag = false;
        }
    }
}
