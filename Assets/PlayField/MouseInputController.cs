using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputController : MonoBehaviour
{
    public TargetingReticle TargetingReticleInstance;
    public Camera PointerCamera;

    public LayerMask ReactorLayerMask;

    Reactor HoveredReactor { get; set; }

    private void Update()
    {
        HandleTargetReticle();
    }

    void HandleTargetReticle()
    {
        if (GameplayController.GameIsStopped)
        {
            TargetingReticleInstance.Hide();
            return;
        }

        Ray pointerRay = PointerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pointerRay.origin, pointerRay.direction, float.MaxValue, ReactorLayerMask);

        if (hit.collider != null)
        {
            Reactor hitReactor = hit.collider.GetComponentInParent<Reactor>();
            TargetingReticleInstance.MoveToReactor(hitReactor);
            HoveredReactor = hitReactor;
            return;
        }

        HoveredReactor = null;
        TargetingReticleInstance.Hide();
    }

    public Reactor GetHoveredReactor()
    {
        return HoveredReactor;
    }
}
