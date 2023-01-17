using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfLimit : MonoBehaviour
{
    // Lưu ý nếu khi các vùng deathzone or limit ko đi theo cam mak bị tụt lại ở sau -> kéo các vùng ý vào trong maincamera
    // khi platform đi qua limit -> sẽ destroy, đi xuyên qua, trigger qua
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag(TagConst.PLATFORM))
        {
            Destroy(col.gameObject);
        }
    }
}
