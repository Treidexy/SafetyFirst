using UnityEngine;

using NaughtyAttributes;

public class HpBar: MonoBehaviour {
    [Required]
    public Transform view;

    [Required]
    public Transform bar;

    void FixedUpdate() {
        transform.LookAt(view);
        transform.Rotate(0, 90, 90);
    }
}
