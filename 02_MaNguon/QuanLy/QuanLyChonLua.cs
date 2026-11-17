using UnityEngine;
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

        string[] ten = { "JEEP WRANGLER OFFROAD", "SAND BUGGY TURBO V8", "MONSTER TRUCK TITAN 4X4" };
        string[] moTa = {
            "Khung gầm thép chịu lực, mui trần chống lật, cân bằng hoàn hảo trên mọi địa hình.",
            "Trọng lượng siêu nhẹ, động cơ tăng áp sau, bứt tốc cực nhanh trên đường trường.",
            "Hệ thống treo khổng lồ, 4 bánh cỡ đại vượt dốc và địa hình lầy lội dễ dàng."
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
        string[] tenLuu = { "Xe Jeep", "Xe Buggy", "Xe Monster Truck" };
        DuLieuGame.LuuXe(tenLuu[xeDangXem], xeDangXem);
        SceneManager.LoadScene("03_ChonNhanVat");
    }

    public void NutBatDau() { SceneManager.LoadScene("02_ChonXe"); }
    public void NutThoat() { Application.Quit(); Debug.Log("Da thoat game!"); }
    public void VeMenuChinh() { SceneManager.LoadScene("01_MenuChinh"); }
    public void ChonXeJeep() { DuLieuGame.LuuXe("Xe Jeep", 0); SceneManager.LoadScene("03_ChonNhanVat"); }
    public void ChonXeBuggy() { DuLieuGame.LuuXe("Xe Buggy", 1); SceneManager.LoadScene("03_ChonNhanVat"); }
    public void ChonXeMonsterTruck() { DuLieuGame.LuuXe("Xe Monster Truck", 2); SceneManager.LoadScene("03_ChonNhanVat"); }
    public void ChonTayDua(int index) { DuLieuGame.LuuNhanVat("Tay đua " + (index + 1), index); SceneManager.LoadScene("04_ChonDiaHinh"); }
    public void ChonDiaHinh(int index) {
        string[] ds = { "Sa mạc", "Rừng", "Đồi núi", "Đường bùn" };
        DuLieuGame.LuuDiaHinh(ds[index], index);
        SceneManager.LoadScene("05_DuaXe");
    }
}