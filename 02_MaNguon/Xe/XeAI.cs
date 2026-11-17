using UnityEngine;
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
}