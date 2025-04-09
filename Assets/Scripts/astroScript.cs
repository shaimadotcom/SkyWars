using UnityEngine;

public class astroScript : MonoBehaviour
{
    public float moveSpeed = 4f; // سرعة الحركة
  // القيمة التي يجب أن يختفي عندها الكويكب

    void Update()
    {
        // تحريك الاسترويد لليسار
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

     
    }
}
