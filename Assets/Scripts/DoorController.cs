using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform door;        // Cánh cửa để xoay hoặc trượt
    public float openAngle = 90f; // Góc mở cửa mặc định 90 độ
    public float openSpeed = 2f;
    public bool isLeftDoor = true;  // true: cửa trái, false: cửa phải

    private Quaternion closedRot;
    private Quaternion openRot;
    private bool isPlayerNear = false;
    private bool isOpen = false;

    void Start()
    {
        closedRot = door.rotation;
        float angle = isLeftDoor ? openAngle : -openAngle;
        openRot = Quaternion.Euler(0, angle, 0) * closedRot;
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
        }

        door.rotation = Quaternion.Lerp(door.rotation,
            isOpen ? openRot : closedRot,
            Time.deltaTime * openSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
