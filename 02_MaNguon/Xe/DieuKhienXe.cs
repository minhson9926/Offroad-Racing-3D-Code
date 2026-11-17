using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DieuKhienXe : MonoBehaviour
{
    public float tocDoToiDa = 26f, tocDoQuay = 95f;
    public ParticleSystem hieuUngBuiTrai, hieuUngBuiPhai, luaNitro;
    public AudioSource amThanhDongCo;
    public Transform[] cacBanhXe;

    private Rigidbody rb;
    private float tocDoHienTai = 0f, nhapGa, nhapLai;
    private bool dangNitro;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        transform.position += Vector3.up * 0.4f;

        // Sửa lỗi ô vuông hồng URP và biến thành tia lửa vàng phụt sau đuôi xe
        Material matTiaLua = TaoVatLieuTiaLuaVang();
        CauHinhTiaLuaSauXe(hieuUngBuiTrai, matTiaLua, new Vector3(-0.65f, 0.35f, -2.05f), 0.14f, 18f);
        CauHinhTiaLuaSauXe(hieuUngBuiPhai, matTiaLua, new Vector3(0.65f, 0.35f, -2.05f), 0.14f, 18f);
        CauHinhTiaLuaSauXe(luaNitro, matTiaLua, new Vector3(0f, 0.45f, -2.1f), 0.22f, 35f);
    }

    Material TaoVatLieuTiaLuaVang()
    {
        Shader sh = Shader.Find("Universal Render Pipeline/Particles/Unlit")
                 ?? Shader.Find("Particles/Standard Unlit")
                 ?? Shader.Find("Sprites/Default");
        Material mat = new Material(sh);

        // Vẽ đốm sáng tròn tỏa hào quang sắc nét (không bị vuông cạnh)
        int size = 64;
        Texture2D tex = new Texture2D(size, size, TextureFormat.RGBA32, false);
        Vector2 center = new Vector2(size * 0.5f, size * 0.5f);
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float dist = Vector2.Distance(new Vector2(x, y), center) / (size * 0.5f);
                float alpha = Mathf.Clamp01(1f - dist);
                alpha = alpha * alpha;
                tex.SetPixel(x, y, new Color(1f, 0.92f, 0.45f, alpha));
            }
        }
        tex.Apply();

        mat.mainTexture = tex;
        if (mat.HasProperty("_BaseMap")) mat.SetTexture("_BaseMap", tex);
        if (mat.HasProperty("_BaseColor")) mat.SetColor("_BaseColor", new Color(1f, 0.85f, 0.15f, 1f));
        if (mat.HasProperty("_Color")) mat.SetColor("_Color", new Color(1f, 0.85f, 0.15f, 1f));
        return mat;
    }

    void CauHinhTiaLuaSauXe(ParticleSystem ps, Material mat, Vector3 viTriSauXe, float kichThuoc, float matDo)
    {
        if (ps == null) return;
        ps.transform.localPosition = viTriSauXe;
        // Hướng tia lửa bắn thẳng về phía sau đuôi xe
        ps.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);

        var main = ps.main;
        main.startColor = new ParticleSystem.MinMaxGradient(new Color(1f, 0.92f, 0.2f, 1f), new Color(1f, 0.55f, 0.05f, 1f));
        main.startSize = new ParticleSystem.MinMaxCurve(kichThuoc * 0.6f, kichThuoc);
        main.startSpeed = new ParticleSystem.MinMaxCurve(4f, 9f);
        main.startLifetime = new ParticleSystem.MinMaxCurve(0.15f, 0.32f);
        main.simulationSpace = ParticleSystemSimulationSpace.World;

        var emission = ps.emission;
        emission.rateOverTime = matDo;

        var shape = ps.shape;
        shape.enabled = true;
        shape.shapeType = ParticleSystemShapeType.Cone;
        shape.angle = 18f;
        shape.radius = 0.08f;

        var renderer = ps.GetComponent<ParticleSystemRenderer>();
        if (renderer != null)
        {
            renderer.material = mat;
            renderer.renderMode = ParticleSystemRenderMode.Stretch;
            renderer.lengthScale = 2.2f;
        }
    }

    void Update()
    {
        nhapGa = 0f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) nhapGa += 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) nhapGa -= 1f;

        nhapLai = 0f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) nhapLai += 1f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) nhapLai -= 1f;

        dangNitro = Input.GetKey(KeyCode.LeftShift) && nhapGa > 0f;

        if (cacBanhXe != null)
            for (int i = 0; i < cacBanhXe.Length; i++)
                if (cacBanhXe[i] != null) cacBanhXe[i].Rotate(Vector3.right, tocDoHienTai * 180f * Time.deltaTime, Space.Self);

        float absSpeed = Mathf.Abs(tocDoHienTai);
        if (amThanhDongCo != null)
        {
            if (absSpeed > 0.5f || Mathf.Abs(nhapGa) > 0.1f)
            {
                if (!amThanhDongCo.isPlaying) amThanhDongCo.Play();
                amThanhDongCo.pitch = Mathf.Lerp(0.8f, 2.0f, absSpeed / tocDoToiDa);
            }
            else amThanhDongCo.pitch = 0.7f;
        }

        BatTatPS(hieuUngBuiTrai, absSpeed > 2.5f);
        BatTatPS(hieuUngBuiPhai, absSpeed > 2.5f);
        BatTatPS(luaNitro, dangNitro);
    }

    void FixedUpdate()
    {
        rb.WakeUp();
        float maxSpd = dangNitro ? 34f : 26f;
        float mucTieu = nhapGa * maxSpd;
        float doNhay = (Mathf.Abs(nhapGa) > 0.01f) ? 18f : 12f;
        tocDoHienTai = Mathf.MoveTowards(tocDoHienTai, mucTieu, doNhay * Time.fixedDeltaTime);

        float vanTocRoi = Mathf.Min(rb.linearVelocity.y, 0f) - 15f * Time.fixedDeltaTime;
        Vector3 diChuyen = transform.forward * tocDoHienTai;
        rb.linearVelocity = new Vector3(diChuyen.x, vanTocRoi, diChuyen.z);

        if (Mathf.Abs(nhapLai) > 0.01f && Mathf.Abs(tocDoHienTai) > 0.3f)
        {
            float chieu = (tocDoHienTai >= 0f) ? 1f : -1f;
            rb.MoveRotation(Quaternion.Euler(0f, rb.rotation.eulerAngles.y + nhapLai * tocDoQuay * chieu * Time.fixedDeltaTime, 0f));
        }
    }

    void BatTatPS(ParticleSystem ps, bool bat) { if (ps != null) { if (bat && !ps.isPlaying) ps.Play(); else if (!bat && ps.isPlaying) ps.Stop(); } }
    public float LayTocDoKmh() { return Mathf.Abs(tocDoHienTai) * 3.6f; }
}