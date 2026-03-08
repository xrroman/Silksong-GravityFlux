using UnityEngine;

namespace Silksong;

public class GravityMenu : MonoBehaviour
{
    public bool IsVisible { get; private set; } = false;

    private Rect _windowRect = new Rect(100, 100, 280, 420);

    private bool _randomModeActive = false;
    private float _timer = 0f;
    private float _randomInterval = 10f;
    private string _intervalInput = "10";

    private GUIStyle _highlightStyle;

    public void Toggle()
    {
        IsVisible = !IsVisible;
        Cursor.visible = IsVisible;
        Cursor.lockState = IsVisible ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!_randomModeActive) return;

        _timer += Time.deltaTime;

        if (_timer < _randomInterval) return;

        GravityLogic.SetRandomGravity();
        _timer = 0f;
    }

    private void OnGUI()
    {
        if (!IsVisible) return;

        if (_highlightStyle == null)
        {
            _highlightStyle = new GUIStyle(GUI.skin.box)
            {
                fontSize = 18,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            };

            _highlightStyle.normal.textColor = Color.white;
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GUI.depth = 0;

        _windowRect = GUI.Window(0, _windowRect, DrawWindow, "GRAVITY CONTROL PANEL");
    }

    private void DrawWindow(int windowID)
    {
        GUI.backgroundColor = Color.black;

        GUILayout.Space(10);

        GUILayout.Box($"SCALE: {GravityLogic.GetGravityScale:F2}x", _highlightStyle, GUILayout.Height(50));

        if (_randomModeActive)
        {
            GUI.color = Color.yellow;
            GUILayout.Label($"Next change in: {(_randomInterval - _timer):F1}s", GUILayout.Height(20));
            GUI.color = Color.white;
        }

        GUILayout.Space(15);

        if (GUILayout.Button("Increase Gravity", GUILayout.Height(30)))
            GravityLogic.IncrementGravity();

        if (GUILayout.Button("Decrease Gravity", GUILayout.Height(30)))
            GravityLogic.DecrementGravity();

        if (GUILayout.Button("Reset to Default", GUILayout.Height(30)))
            GravityLogic.ResetGravity();

        GUILayout.Space(15);

        GUILayout.BeginHorizontal(GUI.skin.box);
        GUILayout.Label("Interval (s):", GUILayout.Width(80));
        _intervalInput = GUILayout.TextField(_intervalInput, 5, GUILayout.Width(60));

        if (float.TryParse(_intervalInput, out float parsedValue))
            _randomInterval = Mathf.Max(0.5f, parsedValue);

        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        GUI.backgroundColor = _randomModeActive ? Color.green : Color.grey;
        string btnText = _randomModeActive ? "STOP RANDOM MODE" : "START RANDOM MODE";

        if (GUILayout.Button(btnText, GUILayout.Height(45)))
        {
            _randomModeActive = !_randomModeActive;

            if (_randomModeActive)
            {
                _timer = 0f;
                GravityLogic.SetRandomGravity();
            }
        }

        GUILayout.FlexibleSpace();

        GUI.backgroundColor = Color.red;

        if (GUILayout.Button("CLOSE", GUILayout.Height(30)))
            Toggle();

        GUI.DragWindow();
    }
}
