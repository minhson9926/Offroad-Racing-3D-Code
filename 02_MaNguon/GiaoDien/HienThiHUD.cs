using UnityEngine;
using UnityEngine.UI;
public class HienThiHUD : MonoBehaviour {
    public QuanLyCuocDua quanLyCuocDua;
    public Text txtTenXe, txtTocDo, txtThoiGian, txtXepHang;
    void Start() { if (txtTenXe != null) txtTenXe.text = "Xe: " + PlayerPrefs.GetString("XeDaChon", "Xe Jeep"); }
    void Update() {
        if (quanLyCuocDua == null || quanLyCuocDua.xeNguoiChoi == null) return;
        if (txtTocDo != null) txtTocDo.text = string.Format("Tốc độ: {0:0} km/h", quanLyCuocDua.xeNguoiChoi.LayTocDoKmh());
        float t = quanLyCuocDua.thoiGianDua;
        if (txtThoiGian != null) txtThoiGian.text = string.Format("Thời gian: {0:00}:{1:00}", Mathf.FloorToInt(t / 60f), Mathf.FloorToInt(t % 60f));
        if (txtXepHang != null) txtXepHang.text = "Hạng: Top " + quanLyCuocDua.hangHienTai;
    }
}