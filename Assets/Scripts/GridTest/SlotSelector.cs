using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlotSelector : MonoBehaviour
{
    public GameObject SlotHighlight;

    public Material TrueColor;

    public Material FalseColor;

    public LayerMask layerMask;

    public Vector2Int mainSlot;

    public Vector2Int subSlot;

    public Vector3 mainOriginPoint;

    public Vector3 subOriginPoint;

    public bool ObstacleMode;

    GridControls gridControls;

    bool CanInteract;

    Ray ray;

    RaycastHit hit;

    Transform target => hit.collider.transform;

    private void Awake()
    {
        gridControls = new GridControls();

        gridControls.Grid.Place.performed += ctx => Interact(ctx);

        gridControls.Grid.Enable();

        ObstacleMode = false;

        CanInteract = false;
    }

    private void FixedUpdate()
    {
        MainCellSelector();
    }

    private void Interact(InputAction.CallbackContext ctx)
    {
        if (CanInteract)
        {
            switch (ObstacleMode)
            {
                case true:

                    GridManager.Instance.SubGridInteract(mainSlot, subSlot);

                    break;

                case false:

                    GridManager.Instance.MainGridInteract(mainSlot);

                    break;
            }
        }
    }

    private void MainCellSelector()
    {
        ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            CanInteract = true;

            SlotHighlight.SetActive(true);

            mainOriginPoint.x = target.position.x - target.localScale.x / 2;
            mainOriginPoint.y = target.localScale.y / 2;
            mainOriginPoint.z = target.position.z - target.localScale.z / 2;

            Vector3 treatedHit = MyMath.RotatePointAroundPivot(hit.point, target.position, Quaternion.Inverse(target.rotation));

            mainSlot = GetSlot(mainOriginPoint, target.eulerAngles, GridManager.Instance.MainGridSize, GridManager.Instance.MainSlotSize, treatedHit);

            Vector3 newSize = new Vector3();
            Vector3 newPos = new Vector3();
            Quaternion newRot = target.rotation;

            subOriginPoint.x = mainSlot.x * GridManager.Instance.MainSlotSize.x + mainOriginPoint.x;
            subOriginPoint.y = target.localScale.y / 2;
            subOriginPoint.z = mainSlot.y * GridManager.Instance.MainSlotSize.y + mainOriginPoint.z;

            subSlot = GetSlot(subOriginPoint, target.eulerAngles, GridManager.Instance.SubGridSize, GridManager.Instance.SubSlotSize, treatedHit);

            var renderer = SlotHighlight.GetComponent<Renderer>();

            Vector2 SlotSize;

            Vector2Int TargetSlot;

            Vector3 OriginPoint;

            switch (ObstacleMode)
            {
                case true:

                    SlotSize = GridManager.Instance.SubSlotSize;
                    TargetSlot = subSlot;
                    OriginPoint = subOriginPoint;

                    if (GridManager.Instance.CheckSlot(mainSlot, subSlot))
                        renderer.material = TrueColor;
                    else
                        renderer.material = FalseColor;

                    break;

                case false:

                    SlotSize = GridManager.Instance.MainSlotSize;
                    TargetSlot = mainSlot;
                    OriginPoint = mainOriginPoint;

                    if (GridManager.Instance.CheckSlot(mainSlot))
                        renderer.material = TrueColor;
                    else
                        renderer.material = FalseColor;

                    break;
            }

            newSize.x = SlotSize.x;
            newSize.y = SlotHighlight.transform.localScale.y;
            newSize.z = SlotSize.y;

            newPos.x = TargetSlot.x * SlotSize.x + SlotSize.x / 2 + OriginPoint.x;
            newPos.y = OriginPoint.y;
            newPos.z = TargetSlot.y * SlotSize.y + SlotSize.y / 2 + OriginPoint.z;

            newPos = MyMath.RotatePointAroundPivot(newPos, target.position, target.rotation);

            SlotHighlight.transform.localScale = newSize;
            SlotHighlight.transform.position = newPos;
            SlotHighlight.transform.rotation = newRot;
        }
        else
        {
            CanInteract = false;

            SlotHighlight.SetActive(false);
        }
    }

    Vector2Int GetSlot(Vector3 OriginPoint, Vector3 OriginRotation, Vector2Int GridSize, Vector2 SlotSize, Vector3 HitPoint)
    {
        Vector2Int slot = new Vector2Int();

        slot.x = (int)(GridSize.x * (HitPoint.x - OriginPoint.x) / (GridSize.x * SlotSize.x));

        slot.y = (int)(GridSize.y * (HitPoint.z - OriginPoint.z) / (GridSize.y * SlotSize.y));

        return slot;
    }

    private void OnDrawGizmos()
    {
        if (ObstacleMode)
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawSphere(subOriginPoint, 1.25f);
        }
        else
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawSphere(mainOriginPoint, 2.5f);
        }

        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawSphere(hit.point, 2f);

        Vector3 treatedHit = MyMath.RotatePointAroundPivot(hit.point, target.position, Quaternion.Inverse(target.rotation));

        Gizmos.color = new Color(1, 0, 1, 0.5f);
        Gizmos.DrawSphere(treatedHit, 2f);
    }
}