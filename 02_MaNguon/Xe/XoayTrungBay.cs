using UnityEngine;

public class XoayTrungBay : MonoBehaviour
{
    public float tocDoXoay = 28f;
    private Vector3 viTriCoDinh;

    void Start()
    {
        // Vô hiệu hóa vật lý va chạm để xe không bị bục Cylinder đẩy bay lên
        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
        if (TryGetComponent<BoxCollider>(out BoxCollider col))
        {
            col.enabled = false;
        }

        // Khóa chặt độ cao xe nằm ngay trên mặt bục trưng bày
        viTriCoDinh = new Vector3(transform.position.x, 0.05f, transform.position.z);
        transform.position = viTriCoDinh;
    }

    void Update()
    {
        transform.position = viTriCoDinh;
        transform.Rotate(Vector3.up, tocDoXoay * Time.deltaTime, Space.World);
    }
}