using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using System.Collections.Generic;

public class TuDongDungGameDuaXe
{
    [MenuItem("BaiTapLon/1. Cap Nhat Toan Bo Code Chuan")]
    public static void CapNhatCode()
    {
        string[] folders = {
            "Assets/01_Canh", "Assets/02_MaNguon/AmThanh", "Assets/02_MaNguon/DiaHinh",
            "Assets/02_MaNguon/GiaoDien", "Assets/02_MaNguon/NhanVat", "Assets/02_MaNguon/QuanLy",
            "Assets/02_MaNguon/Xe", "Assets/03_Xe", "Assets/04_NhanVat", "Assets/05_DiaHinh",
            "Assets/06_AmThanh", "Assets/07_VatLieu", "Assets/08_Prefab", "Assets/09_HinhAnh"
        };
        foreach (string f in folders) if (!Directory.Exists(f)) Directory.CreateDirectory(f);

        File.WriteAllText("Assets/02_MaNguon/QuanLy/DuLieuGame.cs", @"using UnityEngine;
public static class DuLieuGame {
    public static void LuuXe(string tenXe, int indexXe) { PlayerPrefs.SetString(""XeDaChon"", tenXe); PlayerPrefs.SetInt(""IndexXe"", indexXe); PlayerPrefs.Save(); }
    public static void LuuNhanVat(string tenNV, int indexNV) { PlayerPrefs.SetString(""NhanVatDaChon"", tenNV); PlayerPrefs.SetInt(""IndexNhanVat"", indexNV); PlayerPrefs.Save(); }
    public static void LuuDiaHinh(string tenDH, int indexDH) { PlayerPrefs.SetString(""DiaHinhDaChon"", tenDH); PlayerPrefs.SetInt(""IndexDiaHinh"", indexDH); PlayerPrefs.Save(); }
    public static void LuuKetQua(int hangPlayer, float thoiGian) {
        PlayerPrefs.SetInt(""HangPlayer"", hangPlayer);
        PlayerPrefs.SetString(""ThuHang"", ""Top "" + hangPlayer);
        PlayerPrefs.SetFloat(""ThoiGianDua"", thoiGian);
        PlayerPrefs.Save();
    }
}", System.Text.Encoding.UTF8);

        File.WriteAllText("Assets/02_MaNguon/QuanLy/QuanLyChonLua.cs", @"using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuanLyChonLua : MonoBehaviour {
    public GameObject[] cacMauXeTrungBay;
    public Text txtTenXeShowroom;
    public Text txtMoTaXeShowroom;
    public Image thanhTocDo;
    public Image thanhGiaToc;
    public Image thanhBamDuong;
    private int xeDangXem = 0;

    void Start() {
        if (cacMauXeTrungBay != null && cacMauXeTrungBay.Length > 0) XemXe(0);
    }

    public void XemXe(int index) {
        xeDangXem = index;
        for (int i = 0; i < cacMauXeTrungBay.Length; i++)
            if (cacMauXeTrungBay[i] != null) cacMauXeTrungBay[i].SetActive(i == index);

        string[] ten = { ""JEEP WRANGLER OFFROAD"", ""SAND BUGGY TURBO V8"", ""MONSTER TRUCK TITAN 4X4"" };
        string[] moTa = {
            ""Khung gầm thép chịu lực, mui trần chống lật, cân bằng hoàn hảo trên mọi địa hình."",
            ""Trọng lượng siêu nhẹ, động cơ tăng áp sau, bứt tốc cực nhanh trên đường trường."",
            ""Hệ thống treo khổng lồ, 4 bánh cỡ đại vượt dốc và địa hình lầy lội dễ dàng.""
        };
        float[] spd = { 0.72f, 0.95f, 0.78f };
        float[] acc = { 0.78f, 0.92f, 0.70f };
        float[] grip = { 0.92f, 0.80f, 0.96f };

        if (txtTenXeShowroom != null) txtTenXeShowroom.text = ten[index];
        if (txtMoTaXeShowroom != null) txtMoTaXeShowroom.text = moTa[index];
        if (thanhTocDo != null) thanhTocDo.fillAmount = spd[index];
        if (thanhGiaToc != null) thanhGiaToc.fillAmount = acc[index];
        if (thanhBamDuong != null) thanhBamDuong.fillAmount = grip[index];
    }

    public void XacNhanChonXe() {
        string[] tenLuu = { ""Xe Jeep"", ""Xe Buggy"", ""Xe Monster Truck"" };
        DuLieuGame.LuuXe(tenLuu[xeDangXem], xeDangXem);
        SceneManager.LoadScene(""03_ChonNhanVat"");
    }

    public void NutBatDau() { SceneManager.LoadScene(""02_ChonXe""); }
    public void NutThoat() { Application.Quit(); Debug.Log(""Da thoat game!""); }
    public void VeMenuChinh() { SceneManager.LoadScene(""01_MenuChinh""); }
    public void ChonXeJeep() { DuLieuGame.LuuXe(""Xe Jeep"", 0); SceneManager.LoadScene(""03_ChonNhanVat""); }
    public void ChonXeBuggy() { DuLieuGame.LuuXe(""Xe Buggy"", 1); SceneManager.LoadScene(""03_ChonNhanVat""); }
    public void ChonXeMonsterTruck() { DuLieuGame.LuuXe(""Xe Monster Truck"", 2); SceneManager.LoadScene(""03_ChonNhanVat""); }
    public void ChonTayDua(int index) { DuLieuGame.LuuNhanVat(""Tay đua "" + (index + 1), index); SceneManager.LoadScene(""04_ChonDiaHinh""); }
    public void ChonDiaHinh(int index) {
        string[] ds = { ""Sa mạc"", ""Rừng"", ""Đồi núi"", ""Đường bùn"" };
        DuLieuGame.LuuDiaHinh(ds[index], index);
        SceneManager.LoadScene(""05_DuaXe"");
    }
}", System.Text.Encoding.UTF8);

        File.WriteAllText("Assets/02_MaNguon/Xe/XoayTrungBay.cs", @"using UnityEngine;
public class XoayTrungBay : MonoBehaviour {
    public float tocDoXoay = 28f;
    void Update() { transform.Rotate(Vector3.up, tocDoXoay * Time.deltaTime, Space.World); }
}", System.Text.Encoding.UTF8);

        File.WriteAllText("Assets/02_MaNguon/QuanLy/QuanLyCuocDua.cs", @"using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class QuanLyCuocDua : MonoBehaviour {
    public GameObject[] prefabCacLoaiXe;
    public Transform diemXuatPhatPlayer;
    public GameObject[] cacDiaHinh;
    public Transform vachDich;
    public List<Transform> danhSachXeDua = new List<Transform>();

    [HideInInspector] public DieuKhienXe xeNguoiChoi;
    [HideInInspector] public float thoiGianDua = 0f;
    [HideInInspector] public int hangHienTai = 1;

    private bool daKetThuc = false;
    private List<KetQuaTungXe> danhSachVeDich = new List<KetQuaTungXe>();
    private HashSet<Transform> xeDaGhiNhan = new HashSet<Transform>();

    struct KetQuaTungXe { public string tenTayDua, tenXe; public float thoiGian; public bool laNguoiChoi; }

    void Awake() {
        int indexDH = PlayerPrefs.GetInt(""IndexDiaHinh"", 0);
        for (int i = 0; i < cacDiaHinh.Length; i++)
            if (cacDiaHinh[i] != null) cacDiaHinh[i].SetActive(i == indexDH);

        int indexXe = PlayerPrefs.GetInt(""IndexXe"", 0);
        if (prefabCacLoaiXe != null && prefabCacLoaiXe.Length > indexXe && prefabCacLoaiXe[indexXe] != null) {
            GameObject xeObj = Instantiate(prefabCacLoaiXe[indexXe], diemXuatPhatPlayer.position, diemXuatPhatPlayer.rotation);
            xeObj.tag = ""Player"";
            xeNguoiChoi = xeObj.GetComponent<DieuKhienXe>();
            danhSachXeDua.Insert(0, xeObj.transform);
        }
    }

    void Update() {
        if (daKetThuc) return;
        thoiGianDua += Time.deltaTime;
        if (xeNguoiChoi == null || vachDich == null) return;
        float kcPlayer = Vector3.Distance(xeNguoiChoi.transform.position, vachDich.position);
        int hang = 1;
        foreach (Transform xe in danhSachXeDua) {
            if (xe == null || xe == xeNguoiChoi.transform) continue;
            if (xeDaGhiNhan.Contains(xe) || Vector3.Distance(xe.position, vachDich.position) < kcPlayer) hang++;
        }
        hangHienTai = hang;
    }

    public void GhiNhanXeVeDich(Transform xeTransform) {
        if (xeTransform == null || xeDaGhiNhan.Contains(xeTransform)) return;
        xeDaGhiNhan.Add(xeTransform);

        bool laPlayer = (xeNguoiChoi != null && xeTransform == xeNguoiChoi.transform);
        string tenTayDua = laPlayer ? PlayerPrefs.GetString(""NhanVatDaChon"", ""Tay đua 1"") + "" (BẠN)"" : (xeTransform.name.Contains(""1"") ? ""Alex Mercer (AI 1)"" : ""Marcus Vance (AI 2)"");
        string tenXe = laPlayer ? PlayerPrefs.GetString(""XeDaChon"", ""Xe Jeep"") : (xeTransform.name.Contains(""1"") ? ""Xe Thể Thao Xanh"" : ""Xe Thể Thao Vàng"");

        danhSachVeDich.Add(new KetQuaTungXe { tenTayDua = tenTayDua, tenXe = tenXe, thoiGian = thoiGianDua, laNguoiChoi = laPlayer });
        if (laPlayer) KetThucCuocDua();
    }

    public void KetThucCuocDua() {
        if (daKetThuc) return;
        daKetThuc = true;

        foreach (Transform xe in danhSachXeDua) {
            if (xe != null && !xeDaGhiNhan.Contains(xe)) {
                float kcConLai = (vachDich != null) ? Vector3.Distance(xe.position, vachDich.position) : 30f;
                float thoiGianUocTinh = thoiGianDua + (kcConLai / 20f);
                string tenAI = xe.name.Contains(""1"") ? ""Alex Mercer (AI 1)"" : ""Marcus Vance (AI 2)"";
                string xeAI = xe.name.Contains(""1"") ? ""Xe Thể Thao Xanh"" : ""Xe Thể Thao Vàng"";
                danhSachVeDich.Add(new KetQuaTungXe { tenTayDua = tenAI, tenXe = xeAI, thoiGian = thoiGianUocTinh, laNguoiChoi = false });
            }
        }

        danhSachVeDich.Sort((a, b) => a.thoiGian.CompareTo(b.thoiGian));
        for (int i = 0; i < danhSachVeDich.Count; i++) {
            var kq = danhSachVeDich[i];
            PlayerPrefs.SetString($""Top{i + 1}_Ten"", kq.tenTayDua);
            PlayerPrefs.SetString($""Top{i + 1}_Xe"", kq.tenXe);
            PlayerPrefs.SetFloat($""Top{i + 1}_ThoiGian"", kq.thoiGian);
            PlayerPrefs.SetInt($""Top{i + 1}_LaPlayer"", kq.laNguoiChoi ? 1 : 0);
            if (kq.laNguoiChoi) DuLieuGame.LuuKetQua(i + 1, kq.thoiGian);
        }
        PlayerPrefs.Save();
        Invoke(nameof(ChuyenSangKetQua), 2.0f);
    }

    void ChuyenSangKetQua() { SceneManager.LoadScene(""06_KetQua""); }
}", System.Text.Encoding.UTF8);

        File.WriteAllText("Assets/02_MaNguon/QuanLy/VachDich.cs", @"using UnityEngine;
public class VachDich : MonoBehaviour {
    public QuanLyCuocDua quanLyCuocDua;
    public GameObject bangThongBaoChienThang;
    private void OnTriggerEnter(Collider other) {
        Transform xeRoot = other.attachedRigidbody != null ? other.attachedRigidbody.transform : other.transform;
        if (xeRoot.CompareTag(""Player"") || xeRoot.GetComponent<DieuKhienXe>() != null) {
            if (bangThongBaoChienThang != null) bangThongBaoChienThang.SetActive(true);
            if (quanLyCuocDua != null) quanLyCuocDua.GhiNhanXeVeDich(xeRoot);
        } else if (xeRoot.GetComponent<XeAI>() != null) {
            if (quanLyCuocDua != null) quanLyCuocDua.GhiNhanXeVeDich(xeRoot);
        }
    }
}", System.Text.Encoding.UTF8);

        File.WriteAllText("Assets/02_MaNguon/Xe/DieuKhienXe.cs", @"using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class DieuKhienXe : MonoBehaviour {
    public float tocDoToiDa = 26f, tocDoQuay = 95f;
    public ParticleSystem hieuUngBuiTrai, hieuUngBuiPhai, luaNitro;
    public AudioSource amThanhDongCo;
    public Transform[] cacBanhXe;

    private Rigidbody rb;
    private float tocDoHienTai = 0f, nhapGa, nhapLai;
    private bool dangNitro;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false; rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        transform.position += Vector3.up * 0.4f;
    }

    void Update() {
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
        if (amThanhDongCo != null) {
            if (absSpeed > 0.5f || Mathf.Abs(nhapGa) > 0.1f) {
                if (!amThanhDongCo.isPlaying) amThanhDongCo.Play();
                amThanhDongCo.pitch = Mathf.Lerp(0.8f, 2.0f, absSpeed / tocDoToiDa);
            } else amThanhDongCo.pitch = 0.7f;
        }
        BatTatPS(hieuUngBuiTrai, absSpeed > 2.5f);
        BatTatPS(hieuUngBuiPhai, absSpeed > 2.5f);
        BatTatPS(luaNitro, dangNitro);
    }

    void FixedUpdate() {
        rb.WakeUp();
        float maxSpd = dangNitro ? 34f : 26f;
        float mucTieu = nhapGa * maxSpd;
        float doNhay = (Mathf.Abs(nhapGa) > 0.01f) ? 18f : 12f;
        tocDoHienTai = Mathf.MoveTowards(tocDoHienTai, mucTieu, doNhay * Time.fixedDeltaTime);

        float vanTocRoi = Mathf.Min(rb.linearVelocity.y, 0f) - 15f * Time.fixedDeltaTime;
        Vector3 diChuyen = transform.forward * tocDoHienTai;
        rb.linearVelocity = new Vector3(diChuyen.x, vanTocRoi, diChuyen.z);

        if (Mathf.Abs(nhapLai) > 0.01f && Mathf.Abs(tocDoHienTai) > 0.3f) {
            float chieu = (tocDoHienTai >= 0f) ? 1f : -1f;
            rb.MoveRotation(Quaternion.Euler(0f, rb.rotation.eulerAngles.y + nhapLai * tocDoQuay * chieu * Time.fixedDeltaTime, 0f));
        }
    }

    void BatTatPS(ParticleSystem ps, bool bat) { if (ps != null) { if (bat && !ps.isPlaying) ps.Play(); else if (!bat && ps.isPlaying) ps.Stop(); } }
    public float LayTocDoKmh() { return Mathf.Abs(tocDoHienTai) * 3.6f; }
}", System.Text.Encoding.UTF8);

        File.WriteAllText("Assets/02_MaNguon/Xe/XeAI.cs", @"using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class XeAI : MonoBehaviour {
    public Transform[] danhSachDiemMoc;
    public float tocDoAI = 21f, tocDoQuay = 5f;
    private int diemHienTai = 0;
    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false; rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        transform.position += Vector3.up * 0.4f;
    }

    void FixedUpdate() {
        rb.WakeUp();
        if (danhSachDiemMoc == null || danhSachDiemMoc.Length == 0) return;
        Transform mucTieu = danhSachDiemMoc[diemHienTai];
        Vector3 huong = mucTieu.position - transform.position; huong.y = 0f;
        if (huong.sqrMagnitude > 0.1f)
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(huong), tocDoQuay * Time.fixedDeltaTime));

        float vanTocRoi = Mathf.Min(rb.linearVelocity.y, 0f) - 15f * Time.fixedDeltaTime;
        Vector3 diChuyen = transform.forward * tocDoAI;
        rb.linearVelocity = new Vector3(diChuyen.x, vanTocRoi, diChuyen.z);

        if (Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z), new Vector3(mucTieu.position.x, 0f, mucTieu.position.z)) < 15f)
            diemHienTai = (diemHienTai + 1) % danhSachDiemMoc.Length;
    }
}", System.Text.Encoding.UTF8);

        File.WriteAllText("Assets/02_MaNguon/Xe/CameraTheoXe.cs", @"using UnityEngine;
public class CameraTheoXe : MonoBehaviour {
    public QuanLyCuocDua quanLyCuocDua;
    public Vector3 khoangCach = new Vector3(0f, 4.5f, -8.5f);
    public float doMuot = 8f;
    private Transform mucTieuXe;
    void LateUpdate() {
        if (mucTieuXe == null && quanLyCuocDua != null && quanLyCuocDua.xeNguoiChoi != null) mucTieuXe = quanLyCuocDua.xeNguoiChoi.transform;
        if (mucTieuXe == null) return;
        transform.position = Vector3.Lerp(transform.position, mucTieuXe.TransformPoint(khoangCach), doMuot * Time.deltaTime);
        transform.LookAt(mucTieuXe.position + Vector3.up * 1.2f);
    }
}", System.Text.Encoding.UTF8);

        File.WriteAllText("Assets/02_MaNguon/GiaoDien/HienThiHUD.cs", @"using UnityEngine;
using UnityEngine.UI;
public class HienThiHUD : MonoBehaviour {
    public QuanLyCuocDua quanLyCuocDua;
    public Text txtTenXe, txtTocDo, txtThoiGian, txtXepHang;
    void Start() { if (txtTenXe != null) txtTenXe.text = ""Xe: "" + PlayerPrefs.GetString(""XeDaChon"", ""Xe Jeep""); }
    void Update() {
        if (quanLyCuocDua == null || quanLyCuocDua.xeNguoiChoi == null) return;
        if (txtTocDo != null) txtTocDo.text = string.Format(""Tốc độ: {0:0} km/h"", quanLyCuocDua.xeNguoiChoi.LayTocDoKmh());
        float t = quanLyCuocDua.thoiGianDua;
        if (txtThoiGian != null) txtThoiGian.text = string.Format(""Thời gian: {0:00}:{1:00}"", Mathf.FloorToInt(t / 60f), Mathf.FloorToInt(t % 60f));
        if (txtXepHang != null) txtXepHang.text = ""Hạng: Top "" + quanLyCuocDua.hangHienTai;
    }
}", System.Text.Encoding.UTF8);

        File.WriteAllText("Assets/02_MaNguon/GiaoDien/HienThiKetQua.cs", @"using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HienThiKetQua : MonoBehaviour {
    public Text txtTieuDeChinh;
    public Text txtTomTatChuyenDi;
    public Text[] txtHangSo;
    public Text[] txtTenTayDua;
    public Text[] txtPhuongTien;
    public Text[] txtThanhTich;
    public Image[] nenDongBang;

    void Start() {
        int hangPlayer = Mathf.Clamp(PlayerPrefs.GetInt(""HangPlayer"", 1), 1, 3);
        string tenNV = PlayerPrefs.GetString(""NhanVatDaChon"", ""Tay đua 1"");
        string tenXe = PlayerPrefs.GetString(""XeDaChon"", ""Xe Jeep"");
        string diaHinh = PlayerPrefs.GetString(""DiaHinhDaChon"", ""Sa mạc"");

        if (txtTieuDeChinh != null)
            txtTieuDeChinh.text = hangPlayer == 1 ? ""CHÚC MỪNG VÔ ĐỊCH - TOP 1"" : $""HOÀN THÀNH CUỘC ĐUA - TOP {hangPlayer}"";
        if (txtTomTatChuyenDi != null)
            txtTomTatChuyenDi.text = $""Chặng đua: {diaHinh.ToUpper()}   |   Tay đua: {tenNV}   |   Xe: {tenXe}"";

        for (int i = 0; i < 3; i++) {
            int top = i + 1;
            string ten = PlayerPrefs.GetString($""Top{top}_Ten"", $""Tay đua {top}"");
            string xe = PlayerPrefs.GetString($""Top{top}_Xe"", ""Xe Đua"");
            float tg = PlayerPrefs.GetFloat($""Top{top}_ThoiGian"", 45f + i * 2f);
            bool laPlayer = PlayerPrefs.GetInt($""Top{top}_LaPlayer"", 0) == 1;

            if (txtHangSo != null && txtHangSo.Length > i) txtHangSo[i].text = ""#"" + top;
            if (txtTenTayDua != null && txtTenTayDua.Length > i) txtTenTayDua[i].text = ten;
            if (txtPhuongTien != null && txtPhuongTien.Length > i) txtPhuongTien[i].text = xe;
            if (txtThanhTich != null && txtThanhTich.Length > i) txtThanhTich[i].text = string.Format(""{0:00}:{1:00.00}"", Mathf.FloorToInt(tg / 60f), tg % 60f);
            if (nenDongBang != null && nenDongBang.Length > i && nenDongBang[i] != null)
                nenDongBang[i].color = laPlayer ? new Color(0.95f, 0.72f, 0.12f, 0.45f) : new Color(0.14f, 0.18f, 0.26f, 0.90f);
        }
    }

    public void ChoiLai() { SceneManager.LoadScene(""05_DuaXe""); }
    public void VeMenuChinh() { SceneManager.LoadScene(""01_MenuChinh""); }
}", System.Text.Encoding.UTF8);

        File.WriteAllText("Assets/02_MaNguon/AmThanh/TaoAmThanhTuDong.cs", @"using UnityEngine;
public class TaoAmThanhTuDong : MonoBehaviour {
    public bool laNhacNen = false;
    void Start() {
        if (!TryGetComponent<AudioSource>(out AudioSource src)) src = gameObject.AddComponent<AudioSource>();
        if (src.clip != null) return;
        int sr = 44100, samples = sr * 2;
        AudioClip clip = AudioClip.Create(""Audio"", samples, 1, sr, false);
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
}", System.Text.Encoding.UTF8);

        AssetDatabase.Refresh();
        Debug.Log("DA CAP NHAT TOAN BO CODE CHUAN! Hay bam tiep muc 2.");
    }

    [MenuItem("BaiTapLon/2. Dung Giao Dien Chuyen Nghiep & 6 Scene")]
    public static void DungToanBoDuAn()
    {
        Material matJeep = TaoVatLieu("Assets/07_VatLieu/Mat_Jeep.mat", new Color(0.16f, 0.42f, 0.15f), 0.3f, 0.55f);
        Material matBuggy = TaoVatLieu("Assets/07_VatLieu/Mat_Buggy.mat", new Color(0.98f, 0.40f, 0.04f), 0.45f, 0.75f);
        Material matMonster = TaoVatLieu("Assets/07_VatLieu/Mat_Monster.mat", new Color(0.78f, 0.08f, 0.12f), 0.4f, 0.7f);
        Material matAI1 = TaoVatLieu("Assets/07_VatLieu/Mat_AI1.mat", new Color(0.1f, 0.38f, 0.9f), 0.35f, 0.65f);
        Material matAI2 = TaoVatLieu("Assets/07_VatLieu/Mat_AI2.mat", new Color(0.92f, 0.75f, 0.1f), 0.35f, 0.65f);
        Material matLop = TaoVatLieu("Assets/07_VatLieu/Mat_LopXe.mat", new Color(0.09f, 0.09f, 0.1f), 0f, 0.25f);
        Material matMam = TaoVatLieu("Assets/07_VatLieu/Mat_MamXe.mat", new Color(0.82f, 0.85f, 0.9f), 0.9f, 0.85f);
        Material matKinh = TaoVatLieu("Assets/07_VatLieu/Mat_KinhXe.mat", new Color(0.12f, 0.22f, 0.32f), 0.2f, 0.95f);
        Material matDen = TaoVatLieuPhatSang("Assets/07_VatLieu/Mat_DenXe.mat", new Color(1f, 0.96f, 0.8f));
        Material matKhung = TaoVatLieu("Assets/07_VatLieu/Mat_KhungThep.mat", new Color(0.16f, 0.17f, 0.19f), 0.65f, 0.55f);

        GameObject prefabJeep = TaoXeJeepChiTiet(matJeep, matLop, matMam, matKinh, matDen, matKhung);
        GameObject prefabBuggy = TaoXeBuggyChiTiet(matBuggy, matLop, matMam, matDen, matKhung);
        GameObject prefabMonster = TaoXeMonsterChiTiet(matMonster, matLop, matMam, matKinh, matDen, matKhung);
        GameObject prefabAI1 = TaoXeAI_ChiTiet("Xe_AI_1", matAI1, matLop, matMam, matKinh, matDen, matKhung);
        GameObject prefabAI2 = TaoXeAI_ChiTiet("Xe_AI_2", matAI2, matLop, matMam, matKinh, matDen, matKhung);

        TaoSceneMenuChuyenNghiep(prefabBuggy, matKhung);
        TaoSceneChonXeShowroom3D(new GameObject[] { prefabJeep, prefabBuggy, prefabMonster }, matKhung);
        TaoSceneChonNhanVatPro();
        TaoSceneChonDiaHinhPro();
        TaoSceneDuaXeThucTe(new GameObject[] { prefabJeep, prefabBuggy, prefabMonster }, new GameObject[] { prefabAI1, prefabAI2 });
        TaoSceneKetQuaBangXepHang();

        EditorBuildSettings.scenes = new EditorBuildSettingsScene[]
        {
            new EditorBuildSettingsScene("Assets/01_Canh/01_MenuChinh.unity", true),
            new EditorBuildSettingsScene("Assets/01_Canh/02_ChonXe.unity", true),
            new EditorBuildSettingsScene("Assets/01_Canh/03_ChonNhanVat.unity", true),
            new EditorBuildSettingsScene("Assets/01_Canh/04_ChonDiaHinh.unity", true),
            new EditorBuildSettingsScene("Assets/01_Canh/05_DuaXe.unity", true),
            new EditorBuildSettingsScene("Assets/01_Canh/06_KetQua.unity", true)
        };

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorSceneManager.OpenScene("Assets/01_Canh/01_MenuChinh.unity");
        Debug.Log("HOAN THANH DUNG TOAN BO GIAO DIEN MOI & 6 SCENE!");
    }

    static System.Type LayKieu(string tenClass) { return System.Type.GetType(tenClass + ", Assembly-CSharp"); }
    static void GanSuKienVoid(Button btn, Component target, string methodName)
    {
        var action = System.Delegate.CreateDelegate(typeof(UnityEngine.Events.UnityAction), target, methodName) as UnityEngine.Events.UnityAction;
        UnityEventTools.AddVoidPersistentListener(btn.onClick, action);
    }
    static void GanSuKienInt(Button btn, Component target, string methodName, int val)
    {
        var action = System.Delegate.CreateDelegate(typeof(UnityEngine.Events.UnityAction<int>), target, methodName) as UnityEngine.Events.UnityAction<int>;
        UnityEventTools.AddIntPersistentListener(btn.onClick, action, val);
    }
    static Sprite TaoSpriteBoGoc(int w, int h, int radius, Color topCol, Color botCol, Color borderCol, int borderW = 3)
    {
        Texture2D tex = new Texture2D(w, h, TextureFormat.RGBA32, false); tex.filterMode = FilterMode.Bilinear;
        for (int y = 0; y < h; y++)
        {
            Color fill = Color.Lerp(botCol, topCol, (float)y / h);
            for (int x = 0; x < w; x++)
            {
                int dx = Mathf.Max(0, Mathf.Max(radius - x, x - (w - 1 - radius)));
                int dy = Mathf.Max(0, Mathf.Max(radius - y, y - (h - 1 - radius)));
                float dist = Mathf.Sqrt(dx * dx + dy * dy);
                if (dist > radius) tex.SetPixel(x, y, Color.clear);
                else if (dist > radius - borderW || x < borderW || x >= w - borderW || y < borderW || y >= h - borderW) tex.SetPixel(x, y, borderCol);
                else tex.SetPixel(x, y, fill);
            }
        }
        tex.Apply(); return Sprite.Create(tex, new Rect(0, 0, w, h), new Vector2(0.5f, 0.5f));
    }
    static Sprite TaoAnhMinhHoaDiaHinh(int loai)
    {
        int w = 360, h = 200; Texture2D tex = new Texture2D(w, h, TextureFormat.RGBA32, false);
        Color skyTop, skyBot, hillCol, groundCol, accentCol;
        if (loai == 0) { skyTop = new Color(0.18f, 0.55f, 0.92f); skyBot = new Color(0.98f, 0.85f, 0.55f); hillCol = new Color(0.85f, 0.62f, 0.25f); groundCol = new Color(0.92f, 0.72f, 0.34f); accentCol = new Color(0.18f, 0.58f, 0.22f); }
        else if (loai == 1) { skyTop = new Color(0.15f, 0.48f, 0.78f); skyBot = new Color(0.72f, 0.88f, 0.78f); hillCol = new Color(0.12f, 0.38f, 0.16f); groundCol = new Color(0.20f, 0.52f, 0.22f); accentCol = new Color(0.08f, 0.28f, 0.10f); }
        else if (loai == 2) { skyTop = new Color(0.12f, 0.25f, 0.48f); skyBot = new Color(0.65f, 0.75f, 0.85f); hillCol = new Color(0.36f, 0.38f, 0.42f); groundCol = new Color(0.48f, 0.50f, 0.52f); accentCol = new Color(0.92f, 0.94f, 0.98f); }
        else { skyTop = new Color(0.28f, 0.30f, 0.32f); skyBot = new Color(0.55f, 0.48f, 0.40f); hillCol = new Color(0.26f, 0.18f, 0.12f); groundCol = new Color(0.38f, 0.24f, 0.14f); accentCol = new Color(0.22f, 0.15f, 0.08f); }
        for (int y = 0; y < h; y++) for (int x = 0; x < w; x++)
            {
                float nx = (float)x / w, ny = (float)y / h, hillH = 0.38f + Mathf.Sin(nx * 6.5f + loai) * 0.12f + Mathf.Cos(nx * 14f) * 0.05f;
                if (ny > hillH) tex.SetPixel(x, y, Color.Lerp(skyBot, skyTop, (ny - hillH) / (1f - hillH)));
                else if (ny > 0.22f) tex.SetPixel(x, y, (loai == 2 && ny > hillH - 0.04f) ? accentCol : hillCol);
                else tex.SetPixel(x, y, groundCol);
            }
        tex.Apply(); return Sprite.Create(tex, new Rect(0, 0, w, h), new Vector2(0.5f, 0.5f));
    }
    static Font LayFontSacNet() { Font f = Font.CreateDynamicFontFromOSFont("Segoe UI", 28) ?? Font.CreateDynamicFontFromOSFont("Arial", 28); return f != null ? f : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf"); }
    static Canvas TaoCanvasFullHD(out GameObject qlObj, bool coCameraSan = false)
    {
        if (!coCameraSan) { GameObject cam = new GameObject("Main Camera"); cam.tag = "MainCamera"; Camera c = cam.AddComponent<Camera>(); c.clearFlags = CameraClearFlags.SolidColor; c.backgroundColor = new Color(0.07f, 0.09f, 0.13f); cam.AddComponent<AudioListener>(); }
        GameObject es = new GameObject("EventSystem"); es.AddComponent<EventSystem>(); es.AddComponent<StandaloneInputModule>();
        GameObject canvasObj = new GameObject("Canvas_UI"); Canvas canvas = canvasObj.AddComponent<Canvas>(); canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>(); scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize; scaler.referenceResolution = new Vector2(1920, 1080); scaler.matchWidthOrHeight = 0.5f;
        canvasObj.AddComponent<GraphicRaycaster>(); qlObj = new GameObject("QuanLyUI"); return canvas;
    }
    static Image TaoKhungUI(Transform parent, string ten, Vector2 pos, Vector2 size, Sprite sp, Color tint)
    {
        GameObject go = new GameObject(ten); go.transform.SetParent(parent, false); Image img = go.AddComponent<Image>(); img.sprite = sp; img.color = tint;
        RectTransform rt = img.GetComponent<RectTransform>(); rt.anchoredPosition = pos; rt.sizeDelta = size; return img;
    }
    static Text TaoTextPro(Transform parent, string noiDung, Vector2 pos, Vector2 size, int fontSize, Color col, TextAnchor align = TextAnchor.MiddleCenter, bool bold = true)
    {
        GameObject obj = new GameObject("Text_UI"); obj.transform.SetParent(parent, false); Text t = obj.AddComponent<Text>();
        t.text = noiDung; t.font = LayFontSacNet(); t.fontSize = fontSize; t.fontStyle = bold ? FontStyle.Bold : FontStyle.Normal; t.alignment = align; t.color = col;
        Shadow sh = obj.AddComponent<Shadow>(); sh.effectColor = new Color(0, 0, 0, 0.75f); sh.effectDistance = new Vector2(2f, -2f);
        RectTransform rt = t.GetComponent<RectTransform>(); rt.anchoredPosition = pos; rt.sizeDelta = size; return t;
    }
    static Button TaoButtonPro(Transform parent, string nhan, Vector2 pos, Vector2 size, Color topCol, Color botCol, Color borderCol, int fontSize = 30)
    {
        Sprite sp = TaoSpriteBoGoc((int)size.x, (int)size.y, 18, topCol, botCol, borderCol, 3);
        GameObject obj = new GameObject("Btn_" + nhan); obj.transform.SetParent(parent, false); Image img = obj.AddComponent<Image>(); img.sprite = sp; Button btn = obj.AddComponent<Button>();
        ColorBlock cb = btn.colors; cb.normalColor = Color.white; cb.highlightedColor = new Color(1.15f, 1.15f, 1.15f, 1f); cb.pressedColor = new Color(0.8f, 0.8f, 0.8f, 1f); btn.colors = cb;
        RectTransform rt = obj.GetComponent<RectTransform>(); rt.anchoredPosition = pos; rt.sizeDelta = size; TaoTextPro(obj.transform, nhan, Vector2.zero, size, fontSize, Color.white); return btn;
    }
    static void TaoSceneMenuChuyenNghiep(GameObject prefabXeTrungBay, Material matSan)
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
        Camera.main.clearFlags = CameraClearFlags.SolidColor; Camera.main.backgroundColor = new Color(0.06f, 0.08f, 0.12f);
        Camera.main.transform.position = new Vector3(3.8f, 2.3f, 5.2f); Camera.main.transform.LookAt(new Vector3(-1.4f, 0.9f, 0f));
        GameObject podium = GameObject.CreatePrimitive(PrimitiveType.Cylinder); podium.transform.position = new Vector3(-1.6f, -0.1f, 0f); podium.transform.localScale = new Vector3(5.2f, 0.15f, 5.2f); podium.GetComponent<Renderer>().sharedMaterial = matSan;
        GameObject xeShow = (GameObject)PrefabUtility.InstantiatePrefab(prefabXeTrungBay); xeShow.transform.position = new Vector3(-1.6f, 0.05f, 0f);
        Object.DestroyImmediate(xeShow.GetComponent<Rigidbody>()); Component dkOld = xeShow.GetComponent(LayKieu("DieuKhienXe")); if (dkOld != null) Object.DestroyImmediate(dkOld); xeShow.AddComponent(LayKieu("XoayTrungBay"));
        Canvas cv = TaoCanvasFullHD(out GameObject qlObj, true); Component ql = qlObj.AddComponent(LayKieu("QuanLyChonLua"));
        Component bgm = qlObj.AddComponent(LayKieu("TaoAmThanhTuDong")); SerializedObject soBgm = new SerializedObject(bgm); soBgm.FindProperty("laNhacNen").boolValue = true; soBgm.ApplyModifiedProperties();
        Sprite spPanel = TaoSpriteBoGoc(680, 780, 26, new Color(0.11f, 0.14f, 0.20f, 0.92f), new Color(0.06f, 0.08f, 0.12f, 0.96f), new Color(0.95f, 0.72f, 0.15f, 0.85f), 3);
        Image panel = TaoKhungUI(cv.transform, "PanelMenu", new Vector2(460, 0), new Vector2(680, 780), spPanel, Color.white);
        TaoTextPro(panel.transform, "OFFROAD RACING 3D", new Vector2(0, 270), new Vector2(620, 70), 46, new Color(1f, 0.80f, 0.18f));
        TaoTextPro(panel.transform, "GIẢI ĐUA XE ĐỊA HÌNH CHUYÊN NGHIỆP", new Vector2(0, 212), new Vector2(620, 40), 22, new Color(0.75f, 0.82f, 0.92f), TextAnchor.MiddleCenter, false);
        Button btnStart = TaoButtonPro(panel.transform, "▶  BẮT ĐẦU CUỘC ĐUA", new Vector2(0, 60), new Vector2(520, 90), new Color(0.16f, 0.76f, 0.34f), new Color(0.09f, 0.52f, 0.22f), new Color(0.45f, 0.95f, 0.58f), 32);
        Button btnQuit = TaoButtonPro(panel.transform, "✕  THOÁT TRÒ CHƠI", new Vector2(0, -65), new Vector2(520, 80), new Color(0.82f, 0.22f, 0.22f), new Color(0.55f, 0.12f, 0.12f), new Color(0.98f, 0.45f, 0.45f), 28);
        TaoTextPro(panel.transform, "Điều khiển: W A S D lái xe  •  Left Shift kích hoạt Nitro", new Vector2(0, -280), new Vector2(600, 50), 20, new Color(0.65f, 0.72f, 0.82f), TextAnchor.MiddleCenter, false);
        GanSuKienVoid(btnStart, ql, "NutBatDau"); GanSuKienVoid(btnQuit, ql, "NutThoat");
        EditorSceneManager.SaveScene(scene, "Assets/01_Canh/01_MenuChinh.unity");
    }
    static void TaoSceneChonXeShowroom3D(GameObject[] prefabs, Material matSan)
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
        Camera.main.clearFlags = CameraClearFlags.SolidColor; Camera.main.backgroundColor = new Color(0.07f, 0.09f, 0.14f);
        Camera.main.transform.position = new Vector3(0f, 2.5f, 6.2f); Camera.main.transform.LookAt(new Vector3(0f, 0.95f, 0f));
        GameObject podium = GameObject.CreatePrimitive(PrimitiveType.Cylinder); podium.transform.position = new Vector3(0f, -0.1f, 0f); podium.transform.localScale = new Vector3(5.6f, 0.15f, 5.6f); podium.GetComponent<Renderer>().sharedMaterial = matSan;
        GameObject[] mauXe = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            GameObject x = (GameObject)PrefabUtility.InstantiatePrefab(prefabs[i]); x.transform.position = new Vector3(0f, 0.05f, 0f);
            Object.DestroyImmediate(x.GetComponent<Rigidbody>()); Component dkOld = x.GetComponent(LayKieu("DieuKhienXe")); if (dkOld != null) Object.DestroyImmediate(dkOld);
            x.AddComponent(LayKieu("XoayTrungBay")); x.SetActive(i == 0); mauXe[i] = x;
        }
        Canvas cv = TaoCanvasFullHD(out GameObject qlObj, true); Component ql = qlObj.AddComponent(LayKieu("QuanLyChonLua"));
        Sprite spHeader = TaoSpriteBoGoc(1840, 95, 16, new Color(0.12f, 0.15f, 0.22f, 0.92f), new Color(0.08f, 0.10f, 0.15f, 0.95f), new Color(0.95f, 0.72f, 0.15f, 0.6f), 2);
        Image header = TaoKhungUI(cv.transform, "Header", new Vector2(0, 470), new Vector2(1840, 95), spHeader, Color.white);
        TaoTextPro(header.transform, "GARAGE CHỌN CHIẾN XA ĐỊA HÌNH", new Vector2(0, 0), new Vector2(900, 60), 38, new Color(1f, 0.82f, 0.2f));
        Sprite spInfo = TaoSpriteBoGoc(520, 560, 20, new Color(0.10f, 0.13f, 0.19f, 0.92f), new Color(0.06f, 0.08f, 0.12f, 0.95f), new Color(0.3f, 0.65f, 0.95f, 0.7f), 2);
        Image infoPanel = TaoKhungUI(cv.transform, "PanelThongSo", new Vector2(-640, 40), new Vector2(520, 560), spInfo, Color.white);
        SerializedObject soQl = new SerializedObject(ql);
        SerializedProperty pMauXe = soQl.FindProperty("cacMauXeTrungBay"); pMauXe.arraySize = 3;
        for (int i = 0; i < 3; i++) pMauXe.GetArrayElementAtIndex(i).objectReferenceValue = mauXe[i];
        soQl.FindProperty("txtTenXeShowroom").objectReferenceValue = TaoTextPro(infoPanel.transform, "JEEP WRANGLER OFFROAD", new Vector2(0, 215), new Vector2(460, 55), 28, new Color(1f, 0.82f, 0.2f), TextAnchor.MiddleLeft);
        soQl.FindProperty("txtMoTaXeShowroom").objectReferenceValue = TaoTextPro(infoPanel.transform, "Khung gầm thép chịu lực...", new Vector2(0, 135), new Vector2(460, 90), 20, new Color(0.82f, 0.86f, 0.92f), TextAnchor.UpperLeft, false);
        soQl.FindProperty("thanhTocDo").objectReferenceValue = TaoThanhChiSo(infoPanel.transform, "TỐC ĐỘ TỐI ĐA", new Vector2(0, 20), new Color(0.95f, 0.45f, 0.12f));
        soQl.FindProperty("thanhGiaToc").objectReferenceValue = TaoThanhChiSo(infoPanel.transform, "KHẢ NĂNG BỨT TỐC", new Vector2(0, -75), new Color(0.20f, 0.75f, 0.95f));
        soQl.FindProperty("thanhBamDuong").objectReferenceValue = TaoThanhChiSo(infoPanel.transform, "ĐỘ BÁM ĐỊA HÌNH", new Vector2(0, -170), new Color(0.22f, 0.82f, 0.38f));
        soQl.ApplyModifiedProperties();
        Button bJeep = TaoButtonPro(cv.transform, "1. XE JEEP", new Vector2(-560, -340), new Vector2(320, 75), new Color(0.18f, 0.45f, 0.18f), new Color(0.11f, 0.30f, 0.11f), new Color(0.45f, 0.85f, 0.45f), 26);
        Button bBuggy = TaoButtonPro(cv.transform, "2. XE BUGGY", new Vector2(-200, -340), new Vector2(320, 75), new Color(0.85f, 0.38f, 0.08f), new Color(0.60f, 0.24f, 0.04f), new Color(1f, 0.65f, 0.25f), 26);
        Button bMonster = TaoButtonPro(cv.transform, "3. MONSTER TRUCK", new Vector2(160, -340), new Vector2(340, 75), new Color(0.75f, 0.14f, 0.18f), new Color(0.50f, 0.08f, 0.10f), new Color(0.98f, 0.42f, 0.45f), 26);
        Button bConfirm = TaoButtonPro(cv.transform, "TIẾP TỤC CHỌN TAY ĐUA  ➔", new Vector2(640, -340), new Vector2(460, 85), new Color(0.16f, 0.76f, 0.34f), new Color(0.09f, 0.52f, 0.22f), new Color(0.5f, 1f, 0.65f), 28);
        GanSuKienInt(bJeep, ql, "XemXe", 0); GanSuKienInt(bBuggy, ql, "XemXe", 1); GanSuKienInt(bMonster, ql, "XemXe", 2); GanSuKienVoid(bConfirm, ql, "XacNhanChonXe");
        EditorSceneManager.SaveScene(scene, "Assets/01_Canh/02_ChonXe.unity");
    }
    static Image TaoThanhChiSo(Transform parent, string nhan, Vector2 pos, Color fillCol)
    {
        TaoTextPro(parent, nhan, pos + new Vector2(0, 26), new Vector2(460, 32), 20, Color.white, TextAnchor.MiddleLeft);
        Sprite spBg = TaoSpriteBoGoc(460, 24, 10, new Color(0.18f, 0.22f, 0.28f), new Color(0.12f, 0.15f, 0.20f), new Color(0.35f, 0.4f, 0.5f), 2);
        Image bg = TaoKhungUI(parent, "BarBG", pos - new Vector2(0, 8), new Vector2(460, 24), spBg, Color.white);
        Texture2D texFill = new Texture2D(32, 32); for (int y = 0; y < 32; y++) for (int x = 0; x < 32; x++) texFill.SetPixel(x, y, fillCol); texFill.Apply();
        GameObject fillObj = new GameObject("Fill"); fillObj.transform.SetParent(bg.transform, false);
        Image fill = fillObj.AddComponent<Image>(); fill.sprite = Sprite.Create(texFill, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f));
        fill.type = Image.Type.Filled; fill.fillMethod = Image.FillMethod.Horizontal; fill.fillAmount = 0.8f;
        fill.GetComponent<RectTransform>().sizeDelta = new Vector2(452, 16); return fill;
    }
    static void TaoSceneChonNhanVatPro()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        Canvas cv = TaoCanvasFullHD(out GameObject qlObj); Component ql = qlObj.AddComponent(LayKieu("QuanLyChonLua"));
        TaoTextPro(cv.transform, "CHỌN TAY ĐUA ĐẠI DIỆN", new Vector2(0, 420), new Vector2(900, 80), 46, new Color(1f, 0.82f, 0.2f));
        string[] names = { "TAY ĐUA 1", "TAY ĐUA 2", "TAY ĐUA 3" };
        string[] specs = { "Kỹ năng: Vào cua chuẩn xác", "Kỹ năng: Tăng tốc Nitro +15%", "Kỹ năng: Giữ thăng bằng cực tốt" };
        Color[] cardCols = { new Color(0.14f, 0.35f, 0.68f), new Color(0.68f, 0.24f, 0.16f), new Color(0.16f, 0.52f, 0.32f) };
        for (int i = 0; i < 3; i++)
        {
            float xPos = (i - 1) * 520f;
            Sprite spCard = TaoSpriteBoGoc(450, 560, 24, new Color(0.13f, 0.17f, 0.24f), new Color(0.08f, 0.10f, 0.15f), cardCols[i], 4);
            Image card = TaoKhungUI(cv.transform, "CardNV_" + i, new Vector2(xPos, -20), new Vector2(450, 560), spCard, Color.white);
            Sprite spAvatar = TaoSpriteBoGoc(220, 220, 110, cardCols[i] * 1.3f, cardCols[i] * 0.7f, Color.white, 4);
            Image av = TaoKhungUI(card.transform, "Avatar", new Vector2(0, 120), new Vector2(220, 220), spAvatar, Color.white);
            TaoTextPro(av.transform, "0" + (i + 1), Vector2.zero, new Vector2(200, 120), 68, Color.white);
            TaoTextPro(card.transform, names[i], new Vector2(0, -45), new Vector2(400, 55), 34, Color.white);
            TaoTextPro(card.transform, specs[i], new Vector2(0, -105), new Vector2(400, 45), 22, new Color(0.78f, 0.85f, 0.95f), TextAnchor.MiddleCenter, false);
            Button b = TaoButtonPro(card.transform, "CHỌN TAY ĐUA NÀY", new Vector2(0, -200), new Vector2(360, 72), cardCols[i] * 1.2f, cardCols[i] * 0.8f, Color.white, 24);
            GanSuKienInt(b, ql, "ChonTayDua", i);
        }
        EditorSceneManager.SaveScene(scene, "Assets/01_Canh/03_ChonNhanVat.unity");
    }
    static void TaoSceneChonDiaHinhPro()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        Canvas cv = TaoCanvasFullHD(out GameObject qlObj); Component ql = qlObj.AddComponent(LayKieu("QuanLyChonLua"));
        TaoTextPro(cv.transform, "CHỌN CHẶNG ĐUA ĐỊA HÌNH", new Vector2(0, 430), new Vector2(950, 80), 46, new Color(1f, 0.82f, 0.2f));
        string[] dhs = { "SA MẠC NẮNG CHÁY", "RỪNG THÔNG RẬM RẠP", "ĐỒI NÚI ĐÁ CAO", "ĐƯỜNG BÙN LẦY LỘI" };
        string[] descs = { "Đồi cát uốn lượn, xương rồng\nĐộ khó: Trung bình", "Rừng cây dày đặc, khúc cua gắt\nĐộ khó: Khá", "Dốc núi cao, vách đá hiểm trở\nĐộ khó: Cao", "Đường lầy trơn trượt, cây khô\nĐộ khó: Thử thách" };
        Color[] borderCols = { new Color(0.95f, 0.72f, 0.22f), new Color(0.25f, 0.82f, 0.38f), new Color(0.45f, 0.75f, 0.98f), new Color(0.85f, 0.48f, 0.24f) };
        for (int i = 0; i < 4; i++)
        {
            float xPos = (i - 1.5f) * 445f;
            Sprite spCard = TaoSpriteBoGoc(415, 620, 22, new Color(0.13f, 0.16f, 0.23f), new Color(0.08f, 0.10f, 0.14f), borderCols[i], 3);
            Image card = TaoKhungUI(cv.transform, "CardDH_" + i, new Vector2(xPos, -25), new Vector2(415, 620), spCard, Color.white);
            TaoKhungUI(card.transform, "Preview", new Vector2(0, 155), new Vector2(365, 210), TaoAnhMinhHoaDiaHinh(i), Color.white);
            TaoTextPro(card.transform, dhs[i], new Vector2(0, -5), new Vector2(380, 55), 27, borderCols[i]);
            TaoTextPro(card.transform, descs[i], new Vector2(0, -85), new Vector2(370, 90), 21, new Color(0.82f, 0.86f, 0.92f), TextAnchor.MiddleCenter, false);
            Button b = TaoButtonPro(card.transform, "VÀO CUỘC ĐUA  ▶", new Vector2(0, -215), new Vector2(350, 75), borderCols[i] * 0.85f, borderCols[i] * 0.5f, Color.white, 25);
            GanSuKienInt(b, ql, "ChonDiaHinh", i);
        }
        EditorSceneManager.SaveScene(scene, "Assets/01_Canh/04_ChonDiaHinh.unity");
    }
    static void TaoSceneKetQuaBangXepHang()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        Canvas cv = TaoCanvasFullHD(out GameObject qlObj); Component kq = qlObj.AddComponent(LayKieu("HienThiKetQua"));
        Sprite spMain = TaoSpriteBoGoc(1360, 780, 28, new Color(0.11f, 0.14f, 0.21f, 0.96f), new Color(0.06f, 0.08f, 0.12f, 0.98f), new Color(0.95f, 0.75f, 0.18f), 4);
        Image board = TaoKhungUI(cv.transform, "BangXepHangChinh", new Vector2(0, 15), new Vector2(1360, 780), spMain, Color.white);
        SerializedObject soKq = new SerializedObject(kq);
        soKq.FindProperty("txtTieuDeChinh").objectReferenceValue = TaoTextPro(board.transform, "BẢNG XẾP HẠNG CHUNG CUỘC", new Vector2(0, 315), new Vector2(1240, 75), 44, new Color(1f, 0.84f, 0.2f));
        soKq.FindProperty("txtTomTatChuyenDi").objectReferenceValue = TaoTextPro(board.transform, "Chặng đua: SA MẠC   |   Tay đua: Tay đua 1   |   Xe: Xe Jeep", new Vector2(0, 248), new Vector2(1240, 45), 24, new Color(0.75f, 0.85f, 0.96f), TextAnchor.MiddleCenter, false);
        Sprite spHeaderRow = TaoSpriteBoGoc(1220, 62, 12, new Color(0.20f, 0.26f, 0.36f), new Color(0.15f, 0.20f, 0.28f), new Color(0.4f, 0.5f, 0.65f), 2);
        Image hRow = TaoKhungUI(board.transform, "HeaderRow", new Vector2(0, 175), new Vector2(1220, 62), spHeaderRow, Color.white);
        TaoTextPro(hRow.transform, "HẠNG", new Vector2(-500, 0), new Vector2(160, 50), 24, new Color(1f, 0.85f, 0.3f));
        TaoTextPro(hRow.transform, "TAY ĐUA", new Vector2(-190, 0), new Vector2(380, 50), 24, new Color(1f, 0.85f, 0.3f), TextAnchor.MiddleLeft);
        TaoTextPro(hRow.transform, "PHƯƠNG TIỆN", new Vector2(170, 0), new Vector2(300, 50), 24, new Color(1f, 0.85f, 0.3f), TextAnchor.MiddleLeft);
        TaoTextPro(hRow.transform, "THỜI GIAN", new Vector2(475, 0), new Vector2(220, 50), 24, new Color(1f, 0.85f, 0.3f), TextAnchor.MiddleRight);
        SerializedProperty pHang = soKq.FindProperty("txtHangSo"); pHang.arraySize = 3;
        SerializedProperty pTen = soKq.FindProperty("txtTenTayDua"); pTen.arraySize = 3;
        SerializedProperty pXe = soKq.FindProperty("txtPhuongTien"); pXe.arraySize = 3;
        SerializedProperty pTg = soKq.FindProperty("txtThanhTich"); pTg.arraySize = 3;
        SerializedProperty pNen = soKq.FindProperty("nenDongBang"); pNen.arraySize = 3;
        Color[] medalCols = { new Color(1f, 0.84f, 0.15f), new Color(0.82f, 0.86f, 0.92f), new Color(0.86f, 0.54f, 0.28f) };
        for (int i = 0; i < 3; i++)
        {
            float yRow = 82f - i * 105f;
            Sprite spRow = TaoSpriteBoGoc(1220, 88, 16, new Color(0.18f, 0.22f, 0.30f), new Color(0.12f, 0.15f, 0.22f), medalCols[i], 2);
            Image row = TaoKhungUI(board.transform, "RowTop_" + (i + 1), new Vector2(0, yRow), new Vector2(1220, 88), spRow, Color.white);
            pNen.GetArrayElementAtIndex(i).objectReferenceValue = row;
            pHang.GetArrayElementAtIndex(i).objectReferenceValue = TaoTextPro(row.transform, "#" + (i + 1), new Vector2(-500, 0), new Vector2(150, 65), 36, medalCols[i]);
            pTen.GetArrayElementAtIndex(i).objectReferenceValue = TaoTextPro(row.transform, "Tay đua " + (i + 1), new Vector2(-190, 0), new Vector2(380, 65), 28, Color.white, TextAnchor.MiddleLeft);
            pXe.GetArrayElementAtIndex(i).objectReferenceValue = TaoTextPro(row.transform, "Xe Jeep", new Vector2(170, 0), new Vector2(300, 65), 26, new Color(0.8f, 0.88f, 0.96f), TextAnchor.MiddleLeft, false);
            pTg.GetArrayElementAtIndex(i).objectReferenceValue = TaoTextPro(row.transform, "00:45.00", new Vector2(475, 0), new Vector2(220, 65), 30, new Color(0.45f, 0.95f, 0.65f), TextAnchor.MiddleRight);
        }
        soKq.ApplyModifiedProperties();
        Button btnReplay = TaoButtonPro(board.transform, "↻  ĐUA LẠI CHẶNG NÀY", new Vector2(-240, -285), new Vector2(420, 80), new Color(0.16f, 0.72f, 0.34f), new Color(0.09f, 0.48f, 0.22f), new Color(0.45f, 0.95f, 0.58f), 28);
        Button btnMenu = TaoButtonPro(board.transform, "⌂  VỀ MENU CHÍNH", new Vector2(240, -285), new Vector2(420, 80), new Color(0.20f, 0.48f, 0.85f), new Color(0.12f, 0.30f, 0.58f), new Color(0.48f, 0.75f, 1f), 28);
        GanSuKienVoid(btnReplay, kq, "ChoiLai"); GanSuKienVoid(btnMenu, kq, "VeMenuChinh");
        EditorSceneManager.SaveScene(scene, "Assets/01_Canh/06_KetQua.unity");
    }

    static Material TaoVatLieu(string path, Color col, float metallic, float smoothness)
    {
        Shader sh = Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard"); Material m = new Material(sh); m.color = col;
        if (m.HasProperty("_Metallic")) m.SetFloat("_Metallic", metallic); if (m.HasProperty("_Smoothness")) m.SetFloat("_Smoothness", smoothness); if (m.HasProperty("_Glossiness")) m.SetFloat("_Glossiness", smoothness);
        AssetDatabase.CreateAsset(m, path); return m;
    }
    static Material TaoVatLieuPhatSang(string path, Color col) { Material m = TaoVatLieu(path, col, 0.1f, 0.9f); m.EnableKeyword("_EMISSION"); if (m.HasProperty("_EmissionColor")) m.SetColor("_EmissionColor", col * 2.5f); return m; }
    static GameObject TaoBoPhan(Transform parent, string ten, PrimitiveType loai, Vector3 pos, Vector3 rot, Vector3 scale, Material mat)
    {
        GameObject go = GameObject.CreatePrimitive(loai); go.name = ten; Object.DestroyImmediate(go.GetComponent<Collider>());
        go.transform.SetParent(parent, false); go.transform.localPosition = pos; go.transform.localEulerAngles = rot; go.transform.localScale = scale;
        if (mat != null) go.GetComponent<Renderer>().sharedMaterial = mat; return go;
    }
    static Transform TaoBanhXeChiTiet(Transform parent, string ten, Vector3 pos, float duongKinh, float beRong, Material matLop, Material matMam)
    {
        GameObject wheelRoot = new GameObject(ten); wheelRoot.transform.SetParent(parent, false); wheelRoot.transform.localPosition = pos;
        TaoBoPhan(wheelRoot.transform, "LopCaoSu", PrimitiveType.Cylinder, Vector3.zero, new Vector3(0, 0, 90), new Vector3(duongKinh, beRong, duongKinh), matLop);
        TaoBoPhan(wheelRoot.transform, "MamHopKim", PrimitiveType.Cylinder, Vector3.zero, new Vector3(0, 0, 90), new Vector3(duongKinh * 0.58f, beRong * 1.04f, duongKinh * 0.58f), matMam);
        TaoBoPhan(wheelRoot.transform, "TrucOc", PrimitiveType.Cylinder, Vector3.zero, new Vector3(0, 0, 90), new Vector3(duongKinh * 0.2f, beRong * 1.08f, duongKinh * 0.2f), matLop);
        return wheelRoot.transform;
    }
    static void GanHeThongVatLyXe(GameObject root, Vector3 colSize, Vector3 colCenter, Transform[] wheels)
    {
        BoxCollider col = root.AddComponent<BoxCollider>(); col.size = colSize; col.center = colCenter;
        Rigidbody rb = root.AddComponent<Rigidbody>(); rb.mass = 1200f;
        AudioSource audio = root.AddComponent<AudioSource>(); audio.loop = true; audio.playOnAwake = false; root.AddComponent(LayKieu("TaoAmThanhTuDong"));
        ParticleSystem buiL = TaoParticle(root.transform, "BuiBanhTrai", new Vector3(-0.8f, 0.15f, -colSize.z * 0.45f), new Color(0.72f, 0.62f, 0.48f, 0.7f));
        ParticleSystem buiR = TaoParticle(root.transform, "BuiBanhPhai", new Vector3(0.8f, 0.15f, -colSize.z * 0.45f), new Color(0.72f, 0.62f, 0.48f, 0.7f));
        ParticleSystem nitro = TaoParticle(root.transform, "LuaNitro", new Vector3(0f, 0.55f, -colSize.z * 0.52f), new Color(0.1f, 0.75f, 1f, 0.9f));
        Component dk = root.AddComponent(LayKieu("DieuKhienXe"));
        SerializedObject so = new SerializedObject(dk);
        so.FindProperty("amThanhDongCo").objectReferenceValue = audio; so.FindProperty("hieuUngBuiTrai").objectReferenceValue = buiL;
        so.FindProperty("hieuUngBuiPhai").objectReferenceValue = buiR; so.FindProperty("luaNitro").objectReferenceValue = nitro;
        SerializedProperty pWheels = so.FindProperty("cacBanhXe"); pWheels.arraySize = wheels.Length;
        for (int i = 0; i < wheels.Length; i++) pWheels.GetArrayElementAtIndex(i).objectReferenceValue = wheels[i];
        so.ApplyModifiedProperties();
    }
    static ParticleSystem TaoParticle(Transform p, string ten, Vector3 pos, Color col)
    {
        GameObject go = new GameObject(ten); go.transform.SetParent(p); go.transform.localPosition = pos;
        ParticleSystem ps = go.AddComponent<ParticleSystem>(); var main = ps.main; main.startColor = col; main.startSize = 0.65f; main.startLifetime = 0.45f; main.playOnAwake = false; return ps;
    }
    static GameObject TaoXeJeepChiTiet(Material matThan, Material matLop, Material matMam, Material matKinh, Material matDen, Material matKhung)
    {
        GameObject root = new GameObject("Xe_Jeep");
        TaoBoPhan(root.transform, "KhungGam", PrimitiveType.Cube, new Vector3(0, 0.55f, 0), Vector3.zero, new Vector3(1.65f, 0.25f, 3.7f), matKhung);
        TaoBoPhan(root.transform, "ThanXeDuoi", PrimitiveType.Cube, new Vector3(0, 0.85f, -0.15f), Vector3.zero, new Vector3(1.8f, 0.48f, 3.6f), matThan);
        TaoBoPhan(root.transform, "NapCapo", PrimitiveType.Cube, new Vector3(0, 1.05f, 1.05f), Vector3.zero, new Vector3(1.55f, 0.35f, 1.3f), matThan);
        TaoBoPhan(root.transform, "LuoiTanNhiet", PrimitiveType.Cube, new Vector3(0, 0.98f, 1.72f), Vector3.zero, new Vector3(1.35f, 0.45f, 0.1f), matKhung);
        TaoBoPhan(root.transform, "DenPhaTrai", PrimitiveType.Cylinder, new Vector3(-0.52f, 1.02f, 1.76f), new Vector3(90, 0, 0), new Vector3(0.24f, 0.05f, 0.24f), matDen);
        TaoBoPhan(root.transform, "DenPhaPhai", PrimitiveType.Cylinder, new Vector3(0.52f, 1.02f, 1.76f), new Vector3(90, 0, 0), new Vector3(0.24f, 0.05f, 0.24f), matDen);
        TaoBoPhan(root.transform, "CanTruoc", PrimitiveType.Cube, new Vector3(0, 0.62f, 1.88f), Vector3.zero, new Vector3(1.85f, 0.18f, 0.22f), matKhung);
        TaoBoPhan(root.transform, "KinhChanGio", PrimitiveType.Cube, new Vector3(0, 1.48f, 0.42f), new Vector3(-20f, 0, 0), new Vector3(1.55f, 0.55f, 0.08f), matKinh);
        TaoBoPhan(root.transform, "KhungChongLat", PrimitiveType.Cube, new Vector3(0, 1.52f, -1.35f), Vector3.zero, new Vector3(1.5f, 0.75f, 0.12f), matKhung);
        TaoBoPhan(root.transform, "ThanhDocTrai", PrimitiveType.Cube, new Vector3(-0.7f, 1.82f, -0.45f), Vector3.zero, new Vector3(0.08f, 0.08f, 1.85f), matKhung);
        TaoBoPhan(root.transform, "ThanhDocPhai", PrimitiveType.Cube, new Vector3(0.7f, 1.82f, -0.45f), Vector3.zero, new Vector3(0.08f, 0.08f, 1.85f), matKhung);
        TaoBanhXeChiTiet(root.transform, "LopDuPhong", new Vector3(0, 1.15f, -1.96f), 0.95f, 0.16f, matLop, matMam);
        Transform[] wheels = new Transform[] {
            TaoBanhXeChiTiet(root.transform, "B1", new Vector3(-0.98f, 0.5f, 1.25f), 1.0f, 0.22f, matLop, matMam),
            TaoBanhXeChiTiet(root.transform, "B2", new Vector3(0.98f, 0.5f, 1.25f), 1.0f, 0.22f, matLop, matMam),
            TaoBanhXeChiTiet(root.transform, "B3", new Vector3(-0.98f, 0.5f, -1.25f), 1.0f, 0.22f, matLop, matMam),
            TaoBanhXeChiTiet(root.transform, "B4", new Vector3(0.98f, 0.5f, -1.25f), 1.0f, 0.22f, matLop, matMam)
        };
        GanHeThongVatLyXe(root, new Vector3(1.9f, 1.35f, 3.8f), new Vector3(0, 0.85f, 0), wheels);
        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(root, "Assets/08_Prefab/Xe_Jeep.prefab"); Object.DestroyImmediate(root); return prefab;
    }
    static GameObject TaoXeBuggyChiTiet(Material matThan, Material matLop, Material matMam, Material matDen, Material matKhung)
    {
        GameObject root = new GameObject("Xe_Buggy");
        TaoBoPhan(root.transform, "SanXe", PrimitiveType.Cube, new Vector3(0, 0.5f, 0), Vector3.zero, new Vector3(1.35f, 0.22f, 3.5f), matKhung);
        TaoBoPhan(root.transform, "ThanGiua", PrimitiveType.Cube, new Vector3(0, 0.72f, -0.1f), Vector3.zero, new Vector3(1.45f, 0.35f, 2.2f), matThan);
        TaoBoPhan(root.transform, "MuiXeTruoc", PrimitiveType.Cube, new Vector3(0, 0.65f, 1.25f), new Vector3(14f, 0, 0), new Vector3(1.1f, 0.28f, 1.2f), matThan);
        TaoBoPhan(root.transform, "NocKhung", PrimitiveType.Cube, new Vector3(0, 1.38f, -0.15f), Vector3.zero, new Vector3(1.05f, 0.08f, 1.1f), matKhung);
        TaoBoPhan(root.transform, "KhoiDongCoSau", PrimitiveType.Cube, new Vector3(0, 0.88f, -1.35f), Vector3.zero, new Vector3(0.85f, 0.45f, 0.8f), matKhung);
        TaoBoPhan(root.transform, "CanhLuotGio", PrimitiveType.Cube, new Vector3(0, 1.38f, -1.65f), new Vector3(-12f, 0, 0), new Vector3(1.55f, 0.08f, 0.42f), matThan);
        TaoBoPhan(root.transform, "ThanhDenNoc", PrimitiveType.Cube, new Vector3(0, 1.44f, 0.35f), Vector3.zero, new Vector3(0.95f, 0.1f, 0.12f), matDen);
        Transform[] wheels = new Transform[] {
            TaoBanhXeChiTiet(root.transform, "B1", new Vector3(-1.05f, 0.46f, 1.25f), 0.92f, 0.2f, matLop, matMam),
            TaoBanhXeChiTiet(root.transform, "B2", new Vector3(1.05f, 0.46f, 1.25f), 0.92f, 0.2f, matLop, matMam),
            TaoBanhXeChiTiet(root.transform, "B3", new Vector3(-1.12f, 0.52f, -1.25f), 1.04f, 0.26f, matLop, matMam),
            TaoBanhXeChiTiet(root.transform, "B4", new Vector3(1.12f, 0.52f, -1.25f), 1.04f, 0.26f, matLop, matMam)
        };
        GanHeThongVatLyXe(root, new Vector3(2.0f, 1.2f, 3.6f), new Vector3(0, 0.75f, 0), wheels);
        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(root, "Assets/08_Prefab/Xe_Buggy.prefab"); Object.DestroyImmediate(root); return prefab;
    }
    static GameObject TaoXeMonsterChiTiet(Material matThan, Material matLop, Material matMam, Material matKinh, Material matDen, Material matKhung)
    {
        GameObject root = new GameObject("Xe_MonsterTruck");
        TaoBoPhan(root.transform, "KhungGamCao", PrimitiveType.Cube, new Vector3(0, 0.95f, 0), Vector3.zero, new Vector3(1.5f, 0.35f, 4.1f), matKhung);
        TaoBoPhan(root.transform, "ThanBanTai", PrimitiveType.Cube, new Vector3(0, 1.38f, 0), Vector3.zero, new Vector3(2.15f, 0.65f, 4.3f), matThan);
        TaoBoPhan(root.transform, "CabinBanTai", PrimitiveType.Cube, new Vector3(0, 1.98f, 0.15f), Vector3.zero, new Vector3(1.95f, 0.62f, 1.85f), matThan);
        TaoBoPhan(root.transform, "KinhTruocMonster", PrimitiveType.Cube, new Vector3(0, 1.98f, 1.09f), new Vector3(-15f, 0, 0), new Vector3(1.75f, 0.48f, 0.08f), matKinh);
        TaoBoPhan(root.transform, "DenPhaTrai", PrimitiveType.Cube, new Vector3(-0.78f, 1.45f, 2.18f), Vector3.zero, new Vector3(0.32f, 0.22f, 0.08f), matDen);
        TaoBoPhan(root.transform, "DenPhaPhai", PrimitiveType.Cube, new Vector3(0.78f, 1.45f, 2.18f), Vector3.zero, new Vector3(0.32f, 0.22f, 0.08f), matDen);
        TaoBoPhan(root.transform, "OngXaTrai", PrimitiveType.Cylinder, new Vector3(-0.85f, 2.05f, -0.95f), Vector3.zero, new Vector3(0.14f, 0.65f, 0.14f), matMam);
        TaoBoPhan(root.transform, "OngXaPhai", PrimitiveType.Cylinder, new Vector3(0.85f, 2.05f, -0.95f), Vector3.zero, new Vector3(0.14f, 0.65f, 0.14f), matMam);
        Transform[] wheels = new Transform[] {
            TaoBanhXeChiTiet(root.transform, "B1", new Vector3(-1.32f, 0.72f, 1.4f), 1.44f, 0.34f, matLop, matMam),
            TaoBanhXeChiTiet(root.transform, "B2", new Vector3(1.32f, 0.72f, 1.4f), 1.44f, 0.34f, matLop, matMam),
            TaoBanhXeChiTiet(root.transform, "B3", new Vector3(-1.32f, 0.72f, -1.4f), 1.44f, 0.34f, matLop, matMam),
            TaoBanhXeChiTiet(root.transform, "B4", new Vector3(1.32f, 0.72f, -1.4f), 1.44f, 0.34f, matLop, matMam)
        };
        GanHeThongVatLyXe(root, new Vector3(2.4f, 1.9f, 4.4f), new Vector3(0, 1.25f, 0), wheels);
        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(root, "Assets/08_Prefab/Xe_MonsterTruck.prefab"); Object.DestroyImmediate(root); return prefab;
    }
    static GameObject TaoXeAI_ChiTiet(string ten, Material matThan, Material matLop, Material matMam, Material matKinh, Material matDen, Material matKhung)
    {
        GameObject root = new GameObject(ten);
        TaoBoPhan(root.transform, "ThanXe", PrimitiveType.Cube, new Vector3(0, 0.75f, 0), Vector3.zero, new Vector3(1.85f, 0.55f, 3.9f), matThan);
        TaoBoPhan(root.transform, "Cabin", PrimitiveType.Cube, new Vector3(0, 1.25f, -0.2f), Vector3.zero, new Vector3(1.55f, 0.48f, 1.9f), matThan);
        TaoBoPhan(root.transform, "KinhTruoc", PrimitiveType.Cube, new Vector3(0, 1.25f, 0.76f), new Vector3(-22f, 0, 0), new Vector3(1.42f, 0.42f, 0.08f), matKinh);
        TaoBoPhan(root.transform, "CanhSau", PrimitiveType.Cube, new Vector3(0, 1.18f, -1.85f), Vector3.zero, new Vector3(1.75f, 0.08f, 0.35f), matKhung);
        TaoBanhXeChiTiet(root.transform, "B1", new Vector3(-0.98f, 0.48f, 1.25f), 0.96f, 0.22f, matLop, matMam);
        TaoBanhXeChiTiet(root.transform, "B2", new Vector3(0.98f, 0.48f, 1.25f), 0.96f, 0.22f, matLop, matMam);
        TaoBanhXeChiTiet(root.transform, "B3", new Vector3(-0.98f, 0.48f, -1.25f), 0.96f, 0.22f, matLop, matMam);
        TaoBanhXeChiTiet(root.transform, "B4", new Vector3(0.98f, 0.48f, -1.25f), 0.96f, 0.22f, matLop, matMam);
        BoxCollider col = root.AddComponent<BoxCollider>(); col.size = new Vector3(1.9f, 1.2f, 4.0f); col.center = new Vector3(0, 0.8f, 0);
        Rigidbody rb = root.AddComponent<Rigidbody>(); rb.mass = 1200f;
        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(root, $"Assets/08_Prefab/{ten}.prefab"); Object.DestroyImmediate(root); return prefab;
    }

    static float LayTamDuongX(float z) { return Mathf.Sin(z * 0.016f) * 26f; }
    static float LayDoCaoMatDuong(float z)
    {
        float h = 0.5f;
        if (z > 60f && z < 130f) h += Mathf.Sin((z - 60f) / 70f * Mathf.PI) * 2.2f;
        if (z > 200f && z < 280f) h += Mathf.Sin((z - 200f) / 80f * Mathf.PI) * 2.8f;
        if (z > 340f && z < 410f) h += Mathf.Sin((z - 340f) / 70f * Mathf.PI) * 2.0f;
        return h;
    }

    static void TaoSceneDuaXeThucTe(GameObject[] prefabs, GameObject[] prefabAIs)
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
        RenderSettings.fog = true; RenderSettings.fogMode = FogMode.ExponentialSquared; RenderSettings.fogDensity = 0.0022f;
        GameObject qlObj = new GameObject("QuanLyCuocDua"); Component ql = qlObj.AddComponent(LayKieu("QuanLyCuocDua"));
        Component bgm = qlObj.AddComponent(LayKieu("TaoAmThanhTuDong")); SerializedObject soBgm = new SerializedObject(bgm); soBgm.FindProperty("laNhacNen").boolValue = true; soBgm.ApplyModifiedProperties();

        Material matMatDuong = TaoVatLieu("Assets/07_VatLieu/Mat_MatDuong.mat", new Color(0.18f, 0.19f, 0.21f), 0f, 0.35f);
        Material matVachTrang = TaoVatLieu("Assets/07_VatLieu/Mat_VachTrang.mat", new Color(0.96f, 0.96f, 0.96f), 0f, 0.5f);
        Material matLeDo = TaoVatLieu("Assets/07_VatLieu/Mat_LeDo.mat", new Color(0.85f, 0.15f, 0.15f), 0f, 0.4f);

        GameObject trackParent = new GameObject("DuongDuaThucTe"); float chieuRongDuong = 22f, buocZ = 5f;
        for (int i = 0; i < 96; i++)
        {
            float z1 = -10f + i * buocZ, z2 = z1 + buocZ, zMid = (z1 + z2) * 0.5f;
            float x1 = LayTamDuongX(z1), x2 = LayTamDuongX(z2), xMid = (x1 + x2) * 0.5f;
            float y1 = LayDoCaoMatDuong(z1), y2 = LayDoCaoMatDuong(z2), yMid = (y1 + y2) * 0.5f;
            Vector3 dir = new Vector3(x2, y2, z2) - new Vector3(x1, y1, z1);
            GameObject roadSeg = GameObject.CreatePrimitive(PrimitiveType.Cube); roadSeg.name = "MatDuong_" + i; roadSeg.transform.SetParent(trackParent.transform);
            roadSeg.transform.position = new Vector3(xMid, yMid - 0.25f, zMid); roadSeg.transform.rotation = Quaternion.LookRotation(dir);
            roadSeg.transform.localScale = new Vector3(chieuRongDuong, 0.5f, dir.magnitude + 0.35f); roadSeg.GetComponent<Renderer>().sharedMaterial = matMatDuong;
            for (int side = -1; side <= 1; side += 2)
            {
                TaoBoPhan(roadSeg.transform, "VachBien", PrimitiveType.Cube, new Vector3(side * 0.46f, 0.52f, 0), Vector3.zero, new Vector3(0.025f, 0.05f, 1f), matVachTrang);
                TaoBoPhan(roadSeg.transform, "LeDuong", PrimitiveType.Cube, new Vector3(side * 0.52f, 0.55f, 0), Vector3.zero, new Vector3(0.06f, 0.14f, 1f), (i % 2 == 0) ? matLeDo : matVachTrang);
            }
            if (i % 2 == 0)
            {
                TaoBoPhan(roadSeg.transform, "VachChiaLan1", PrimitiveType.Cube, new Vector3(-0.16f, 0.52f, 0), Vector3.zero, new Vector3(0.02f, 0.05f, 0.65f), matVachTrang);
                TaoBoPhan(roadSeg.transform, "VachChiaLan2", PrimitiveType.Cube, new Vector3(0.16f, 0.52f, 0), Vector3.zero, new Vector3(0.02f, 0.05f, 0.65f), matVachTrang);
            }
        }

        float zFinish = 440f, xFinish = LayTamDuongX(zFinish), yFinish = LayDoCaoMatDuong(zFinish);
        GameObject finishGate = new GameObject("FinishLine"); finishGate.transform.position = new Vector3(xFinish, yFinish, zFinish);
        BoxCollider trig = finishGate.AddComponent<BoxCollider>(); trig.isTrigger = true; trig.size = new Vector3(chieuRongDuong + 4f, 8f, 3f); trig.center = new Vector3(0, 3.5f, 0);
        TaoBoPhan(finishGate.transform, "TruTrai", PrimitiveType.Cylinder, new Vector3(-chieuRongDuong * 0.55f, 4f, 0), Vector3.zero, new Vector3(0.8f, 4f, 0.8f), matLeDo);
        TaoBoPhan(finishGate.transform, "TruPhai", PrimitiveType.Cylinder, new Vector3(chieuRongDuong * 0.55f, 4f, 0), Vector3.zero, new Vector3(0.8f, 4f, 0.8f), matLeDo);
        TaoBoPhan(finishGate.transform, "BangDich", PrimitiveType.Cube, new Vector3(0, 7.5f, 0), Vector3.zero, new Vector3(chieuRongDuong * 1.2f, 1.4f, 0.6f), matLeDo);
        Component vd = finishGate.AddComponent(LayKieu("VachDich"));

        string[] tenDH = { "DiaHinh_SaMac", "DiaHinh_Rung", "DiaHinh_DoiNui", "DiaHinh_DuongBun" };
        Color[] mauDat = { new Color(0.88f, 0.72f, 0.38f), new Color(0.20f, 0.48f, 0.18f), new Color(0.38f, 0.40f, 0.36f), new Color(0.32f, 0.20f, 0.11f) };
        GameObject[] dhObjs = new GameObject[4];
        Material matLaCay = TaoVatLieu("Assets/07_VatLieu/Mat_LaCay.mat", new Color(0.12f, 0.42f, 0.14f), 0f, 0.2f);
        Material matThanCay = TaoVatLieu("Assets/07_VatLieu/Mat_ThanCay.mat", new Color(0.35f, 0.22f, 0.12f), 0f, 0.1f);
        Material matXuongRong = TaoVatLieu("Assets/07_VatLieu/Mat_XuongRong.mat", new Color(0.18f, 0.55f, 0.22f), 0f, 0.3f);
        Material matDaNui = TaoVatLieu("Assets/07_VatLieu/Mat_DaNui.mat", new Color(0.42f, 0.43f, 0.45f), 0.1f, 0.2f);

        for (int idx = 0; idx < 4; idx++)
        {
            TerrainData td = new TerrainData(); td.heightmapResolution = 129; td.size = new Vector3(400, 45, 520);
            float[,] heights = new float[129, 129];
            for (int z = 0; z < 129; z++)
            {
                float worldZ = -30f + (z / 128f) * 520f, tamX = LayTamDuongX(worldZ), doCaoDuong = LayDoCaoMatDuong(worldZ);
                for (int x = 0; x < 129; x++)
                {
                    float worldX = -200f + (x / 128f) * 400f, dist = Mathf.Abs(worldX - tamX), baseH = (doCaoDuong + 5.8f) / 45f;
                    if (dist > 14f) baseH += Mathf.Clamp01((dist - 14f) / 65f) * (((idx == 2) ? 0.55f : 0.32f) + Mathf.PerlinNoise(x * 0.08f + idx * 10f, z * 0.08f) * 0.45f);
                    heights[z, x] = Mathf.Clamp01(baseH);
                }
            }
            td.SetHeights(0, 0, heights); AssetDatabase.CreateAsset(td, $"Assets/05_DiaHinh/{tenDH[idx]}_Data.asset");
            GameObject tObj = Terrain.CreateTerrainGameObject(td); tObj.name = tenDH[idx]; tObj.transform.position = new Vector3(-200f, -6f, -30f);
            tObj.GetComponent<Terrain>().materialTemplate = TaoVatLieu($"Assets/07_VatLieu/Mat_{tenDH[idx]}.mat", mauDat[idx], 0f, 0.15f);
            Random.InitState(100 + idx);
            for (int k = 0; k < 100; k++)
            {
                float zProp = Random.Range(0f, 460f), xProp = LayTamDuongX(zProp) + ((k % 2 == 0) ? 1f : -1f) * Random.Range(15.5f, 55f);
                float yProp = tObj.GetComponent<Terrain>().SampleHeight(new Vector3(xProp, 0, zProp)) - 6f;
                Vector3 pos = new Vector3(xProp, yProp, zProp);
                if (idx == 0) { if (k % 3 != 0) TaoCayXuongRong(tObj.transform, pos, matXuongRong); else TaoTangDa(tObj.transform, pos, matDaNui); }
                else if (idx == 1) TaoCayThong(tObj.transform, pos, matThanCay, matLaCay, Random.Range(1.1f, 1.8f));
                else if (idx == 2) { if (k % 2 == 0) TaoTangDa(tObj.transform, pos, matDaNui); else TaoCayThong(tObj.transform, pos, matThanCay, matLaCay, 1.2f); }
                else { if (k % 2 == 0) TaoCayKho(tObj.transform, pos, matThanCay); else TaoTangDa(tObj.transform, pos, matDaNui); }
            }
            tObj.SetActive(idx == 0); dhObjs[idx] = tObj;
        }

        GameObject startPlayer = new GameObject("DiemXuatPhat_Player");
        startPlayer.transform.position = new Vector3(LayTamDuongX(8f) - 5.5f, LayDoCaoMatDuong(8f) + 0.1f, 8f);
        List<Transform> wps = new List<Transform>();
        for (int z = 30; z <= 460; z += 25) { GameObject wp = new GameObject("Waypoint_" + z); wp.transform.position = new Vector3(LayTamDuongX(z), LayDoCaoMatDuong(z) + 0.5f, z); wps.Add(wp.transform); }
        List<Transform> dsXe = new List<Transform>();
        float[] lanAI = { 0f, 5.5f };
        for (int i = 0; i < 2; i++)
        {
            GameObject ai = (GameObject)PrefabUtility.InstantiatePrefab(prefabAIs[i]);
            ai.transform.position = new Vector3(LayTamDuongX(8f) + lanAI[i], LayDoCaoMatDuong(8f) + 0.1f, 8f);
            Component cAI = ai.AddComponent(LayKieu("XeAI"));
            SerializedObject soAI = new SerializedObject(cAI);
            SerializedProperty pWps = soAI.FindProperty("danhSachDiemMoc"); pWps.arraySize = wps.Count;
            for (int k = 0; k < wps.Count; k++) pWps.GetArrayElementAtIndex(k).objectReferenceValue = wps[k];
            soAI.FindProperty("tocDoAI").floatValue = 19.5f + i * 2.5f; soAI.ApplyModifiedProperties();
            dsXe.Add(ai.transform);
        }

        SerializedObject soQl = new SerializedObject(ql);
        SerializedProperty pPrefabs = soQl.FindProperty("prefabCacLoaiXe"); pPrefabs.arraySize = 3;
        for (int i = 0; i < 3; i++) pPrefabs.GetArrayElementAtIndex(i).objectReferenceValue = prefabs[i];
        SerializedProperty pDH = soQl.FindProperty("cacDiaHinh"); pDH.arraySize = 4;
        for (int i = 0; i < 4; i++) pDH.GetArrayElementAtIndex(i).objectReferenceValue = dhObjs[i];
        SerializedProperty pXe = soQl.FindProperty("danhSachXeDua"); pXe.arraySize = dsXe.Count;
        for (int i = 0; i < dsXe.Count; i++) pXe.GetArrayElementAtIndex(i).objectReferenceValue = dsXe[i];
        soQl.FindProperty("diemXuatPhatPlayer").objectReferenceValue = startPlayer.transform;
        soQl.FindProperty("vachDich").objectReferenceValue = finishGate.transform;
        soQl.ApplyModifiedProperties();

        Component camScript = Camera.main.gameObject.AddComponent(LayKieu("CameraTheoXe"));
        SerializedObject soCam = new SerializedObject(camScript); soCam.FindProperty("quanLyCuocDua").objectReferenceValue = ql; soCam.ApplyModifiedProperties();

        Canvas cv = TaoCanvasFullHD(out _, true);
        Sprite spHudBox = TaoSpriteBoGoc(360, 120, 16, new Color(0.10f, 0.13f, 0.19f, 0.85f), new Color(0.06f, 0.08f, 0.12f, 0.90f), new Color(0.95f, 0.72f, 0.15f, 0.7f), 2);
        Image hudLeft = TaoKhungUI(cv.transform, "HudLeft", new Vector2(-730, 440), new Vector2(360, 120), spHudBox, Color.white);
        Image hudRight = TaoKhungUI(cv.transform, "HudRight", new Vector2(730, 440), new Vector2(360, 120), spHudBox, Color.white);

        Component hud = cv.gameObject.AddComponent(LayKieu("HienThiHUD"));
        SerializedObject soHud = new SerializedObject(hud);
        soHud.FindProperty("quanLyCuocDua").objectReferenceValue = ql;
        soHud.FindProperty("txtTenXe").objectReferenceValue = TaoTextPro(hudLeft.transform, "Xe: Jeep", new Vector2(0, 26), new Vector2(320, 40), 26, Color.white, TextAnchor.MiddleLeft);
        soHud.FindProperty("txtTocDo").objectReferenceValue = TaoTextPro(hudLeft.transform, "Tốc độ: 0 km/h", new Vector2(0, -24), new Vector2(320, 45), 30, new Color(1f, 0.84f, 0.2f), TextAnchor.MiddleLeft);
        soHud.FindProperty("txtThoiGian").objectReferenceValue = TaoTextPro(hudRight.transform, "Thời gian: 00:00", new Vector2(0, 26), new Vector2(320, 40), 26, Color.white, TextAnchor.MiddleRight);
        soHud.FindProperty("txtXepHang").objectReferenceValue = TaoTextPro(hudRight.transform, "Hạng: Top 1", new Vector2(0, -24), new Vector2(320, 45), 32, new Color(0.35f, 0.95f, 0.65f), TextAnchor.MiddleRight);
        soHud.ApplyModifiedProperties();

        TaoTextPro(cv.transform, "W/S/A/D: Lái xe  •  Left Shift: Nitro tăng tốc", new Vector2(0, -490), new Vector2(680, 45), 22, Color.white);
        Text txtWin = TaoTextPro(cv.transform, "BẠN ĐÃ VỀ ĐÍCH!", new Vector2(0, 60), new Vector2(1100, 140), 68, new Color(1f, 0.84f, 0.15f));
        txtWin.gameObject.SetActive(false);

        SerializedObject soVd = new SerializedObject(vd);
        soVd.FindProperty("quanLyCuocDua").objectReferenceValue = ql;
        soVd.FindProperty("bangThongBaoChienThang").objectReferenceValue = txtWin.gameObject;
        soVd.ApplyModifiedProperties();

        EditorSceneManager.SaveScene(scene, "Assets/01_Canh/05_DuaXe.unity");
    }

    static void TaoCayThong(Transform p, Vector3 pos, Material mThan, Material mLa, float sc)
    {
        GameObject t = new GameObject("CayThong"); t.transform.SetParent(p); t.transform.position = pos; t.transform.localScale = Vector3.one * sc;
        TaoBoPhan(t.transform, "Than", PrimitiveType.Cylinder, new Vector3(0, 1.5f, 0), Vector3.zero, new Vector3(0.45f, 1.5f, 0.45f), mThan);
        TaoBoPhan(t.transform, "Tan1", PrimitiveType.Sphere, new Vector3(0, 3.3f, 0), Vector3.zero, new Vector3(2.8f, 2.2f, 2.8f), mLa);
        TaoBoPhan(t.transform, "Tan2", PrimitiveType.Sphere, new Vector3(0, 4.7f, 0), Vector3.zero, new Vector3(2.0f, 1.8f, 2.0f), mLa);
    }
    static void TaoCayXuongRong(Transform p, Vector3 pos, Material m)
    {
        GameObject c = new GameObject("XuongRong"); c.transform.SetParent(p); c.transform.position = pos;
        TaoBoPhan(c.transform, "Than", PrimitiveType.Capsule, new Vector3(0, 1.8f, 0), Vector3.zero, new Vector3(0.55f, 1.8f, 0.55f), m);
        TaoBoPhan(c.transform, "Nhanh1", PrimitiveType.Capsule, new Vector3(-0.55f, 2.1f, 0), new Vector3(0, 0, 25f), new Vector3(0.35f, 0.9f, 0.35f), m);
        TaoBoPhan(c.transform, "Nhanh2", PrimitiveType.Capsule, new Vector3(0.55f, 1.7f, 0), new Vector3(0, 0, -25f), new Vector3(0.35f, 0.8f, 0.35f), m);
    }
    static void TaoCayKho(Transform p, Vector3 pos, Material m)
    {
        GameObject t = new GameObject("CayKho"); t.transform.SetParent(p); t.transform.position = pos;
        TaoBoPhan(t.transform, "Than", PrimitiveType.Cylinder, new Vector3(0, 1.8f, 0), new Vector3(5f, 0, 4f), new Vector3(0.4f, 1.8f, 0.4f), m);
        TaoBoPhan(t.transform, "Canh", PrimitiveType.Cylinder, new Vector3(-0.6f, 3.1f, 0), new Vector3(0, 0, 35f), new Vector3(0.18f, 1.0f, 0.18f), m);
    }
    static void TaoTangDa(Transform p, Vector3 pos, Material m)
    {
        GameObject r = GameObject.CreatePrimitive(PrimitiveType.Sphere); r.name = "TangDa"; Object.DestroyImmediate(r.GetComponent<Collider>());
        r.transform.SetParent(p); r.transform.position = pos + Vector3.up * 0.6f; r.transform.localScale = new Vector3(3.2f, 2.0f, 3.2f); r.GetComponent<Renderer>().sharedMaterial = m;
    }
}