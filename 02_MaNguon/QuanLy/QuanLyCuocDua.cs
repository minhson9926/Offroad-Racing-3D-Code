using UnityEngine;
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
        int indexDH = PlayerPrefs.GetInt("IndexDiaHinh", 0);
        for (int i = 0; i < cacDiaHinh.Length; i++)
            if (cacDiaHinh[i] != null) cacDiaHinh[i].SetActive(i == indexDH);

        int indexXe = PlayerPrefs.GetInt("IndexXe", 0);
        if (prefabCacLoaiXe != null && prefabCacLoaiXe.Length > indexXe && prefabCacLoaiXe[indexXe] != null) {
            GameObject xeObj = Instantiate(prefabCacLoaiXe[indexXe], diemXuatPhatPlayer.position, diemXuatPhatPlayer.rotation);
            xeObj.tag = "Player";
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
        string tenTayDua = laPlayer ? PlayerPrefs.GetString("NhanVatDaChon", "Tay đua 1") + " (BẠN)" : (xeTransform.name.Contains("1") ? "Alex Mercer (AI 1)" : "Marcus Vance (AI 2)");
        string tenXe = laPlayer ? PlayerPrefs.GetString("XeDaChon", "Xe Jeep") : (xeTransform.name.Contains("1") ? "Xe Thể Thao Xanh" : "Xe Thể Thao Vàng");

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
                string tenAI = xe.name.Contains("1") ? "Alex Mercer (AI 1)" : "Marcus Vance (AI 2)";
                string xeAI = xe.name.Contains("1") ? "Xe Thể Thao Xanh" : "Xe Thể Thao Vàng";
                danhSachVeDich.Add(new KetQuaTungXe { tenTayDua = tenAI, tenXe = xeAI, thoiGian = thoiGianUocTinh, laNguoiChoi = false });
            }
        }

        danhSachVeDich.Sort((a, b) => a.thoiGian.CompareTo(b.thoiGian));
        for (int i = 0; i < danhSachVeDich.Count; i++) {
            var kq = danhSachVeDich[i];
            PlayerPrefs.SetString($"Top{i + 1}_Ten", kq.tenTayDua);
            PlayerPrefs.SetString($"Top{i + 1}_Xe", kq.tenXe);
            PlayerPrefs.SetFloat($"Top{i + 1}_ThoiGian", kq.thoiGian);
            PlayerPrefs.SetInt($"Top{i + 1}_LaPlayer", kq.laNguoiChoi ? 1 : 0);
            if (kq.laNguoiChoi) DuLieuGame.LuuKetQua(i + 1, kq.thoiGian);
        }
        PlayerPrefs.Save();
        Invoke(nameof(ChuyenSangKetQua), 2.0f);
    }

    void ChuyenSangKetQua() { SceneManager.LoadScene("06_KetQua"); }
}