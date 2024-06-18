using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomCursor : MonoBehaviour
{
    [Header("Cursor Textures")]
    [Tooltip("Cursor texture for when the cursor is in its default state.")]
    public Texture2D DefaultCursor;
    [Tooltip("Cursor texture for when the left mouse button is held.")]
    public Texture2D LeftClickCursor;
    [Tooltip("Cursor texture for when the right mouse button is held.")]
    public Texture2D RightClickCursor;
    [Tooltip("Cursor texture for when the scroll button is used for scrolling.")]
    public Texture2D ScrollingCursor;
    [Tooltip("Cursor texture for when the scroll button is held.")]
    public Texture2D ScrollButtonHeldCursor;
    [Tooltip("Cursor texture for when the cursor is hovering over a clickable object.")]
    public Texture2D HoverCursor;

    private CustomCursorInputs _controls;
    private bool isHovering;

    private void Awake()
    {
        _controls = new CustomCursorInputs();

        // Left-click
        _controls.CustomCursor.LeftClicking.performed += _ => OnLeftClick();
        _controls.CustomCursor.LeftClicking.canceled += _ => Invoke("SetDefaultCursor", 0.15f);

        // Right-click
        _controls.CustomCursor.RightClicking.performed += _ => OnRightClick();
        _controls.CustomCursor.RightClicking.canceled += _ => Invoke("SetDefaultCursor", 0.15f);

        // Scroll button clicked
        _controls.CustomCursor.ScrollClicking.performed += _ => OnScrollClick();
        _controls.CustomCursor.ScrollClicking.canceled += _ => Invoke("SetDefaultCursor", 0.15f);

        // Scrolling
        _controls.CustomCursor.Scrolling.performed += _ => OnScrolling();
        _controls.CustomCursor.Scrolling.canceled += _ => Invoke("SetDefaultCursor", 0.5f);

        if (DefaultCursor == null)
        {
            throw new Exception("You need to set the default cursor texture!");
        }

        // Set the cursor to the default state
        SetCursorTexture(DefaultCursor);
    }

    private void SetCursorTexture(Texture2D cursorTexture)
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    private void OnLeftClick()
    {
        SetCursorTexture(LeftClickCursor);
    }

    private void OnRightClick()
    {
        SetCursorTexture(RightClickCursor);
    }

    private void OnScrollClick()
    {
        SetCursorTexture(ScrollButtonHeldCursor);
    }

    private void OnScrolling()
    {
        SetCursorTexture(ScrollingCursor);
    }

    private void SetDefaultCursor()
    {
        if (isHovering)
        {
            SetCursorTexture(HoverCursor);
        }
        else
        {
            SetCursorTexture(DefaultCursor);
        }
    }

    public void SetHoverState(bool isHovering)
    {
        this.isHovering = isHovering;
        SetCursorTexture(isHovering ? HoverCursor : DefaultCursor);
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }
}
