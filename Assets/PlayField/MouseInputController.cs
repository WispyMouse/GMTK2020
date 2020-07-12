using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputController : MonoBehaviour
{
    public TargetingReticle TargetingReticleInstance;
    public Camera PointerCamera;

    public LayerMask ReactorLayerMask;

    Reactor HoveredReactor { get; set; }
    GameResource SelectedResource { get; set; }
    System.Action MouseClickedAction { get; set; }
    System.Action MouseRightClickedAction { get; set; }

    public void Initiate(System.Action mouseClickedAction, System.Action rightMouseClickedAction)
    {
        MouseClickedAction = mouseClickedAction;
        MouseRightClickedAction = rightMouseClickedAction;
    }

    private void Update()
    {
        HandleTargetReticle();

        if (!GameplayController.GameIsStopped && Input.GetMouseButtonDown(0))
        {
            MouseClickedAction();
        }

        if (!GameplayController.GameIsStopped && Input.GetMouseButtonDown(1))
        {
            MouseRightClickedAction();
        }
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
            hitReactor.BeingHovered(SelectedResource);
            return;
        }

        if (HoveredReactor != null)
        {
            HoveredReactor.EndHovered();
            TargetingReticleInstance.Hide();
            HoveredReactor = null;
        }
        else
        {
            TargetingReticleInstance.Hide();
        }
    }

    public Reactor GetHoveredReactor()
    {
        return HoveredReactor;
    }

    public void ResourceSelected(GameResource selected)
    {
        SelectedResource = selected;
    }
}
