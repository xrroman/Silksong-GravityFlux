using UnityEngine;

namespace Silksong;

public static class GravityLogic
{
    public static bool _initialized = false;

    private static float _modifiedGravityScale = 1f;
    private static bool _isGravityInverted = false;

    private static float _baseGravityScale;
    private static float _baseMaxFallVelocity;
    private static float _baseDefaultGravity;
    private static float _baseAirHangGravity;

    public static bool GetIsGravityInverted => _isGravityInverted;
    public static float GetGravityScale => _modifiedGravityScale;

    public static void Init()
    {
        HeroController hc = HeroController.instance;
        if (hc == null || _initialized) return;

        _baseGravityScale = hc.rb2d.gravityScale;
        _baseMaxFallVelocity = hc.MAX_FALL_VELOCITY;
        _baseDefaultGravity = hc.DEFAULT_GRAVITY;
        _baseAirHangGravity = hc.AIR_HANG_GRAVITY;

        _initialized = true;
    }

    private static void ApplyGravity()
    {
        if (!_initialized) Init();

        HeroController hc = HeroController.instance;
        if (hc == null) return;

        hc.rb2d.gravityScale = _baseGravityScale * _modifiedGravityScale;
        hc.MAX_FALL_VELOCITY = _baseMaxFallVelocity * _modifiedGravityScale;
        hc.DEFAULT_GRAVITY = _baseDefaultGravity * _modifiedGravityScale;
        hc.AIR_HANG_GRAVITY = _baseAirHangGravity * _modifiedGravityScale;
    }

    public static void ResetGravity()
    {
        _modifiedGravityScale = 1f;
        ApplyGravity();
    }

    public static void IncrementGravity()
    {
        _modifiedGravityScale += 0.5f;
        ApplyGravity();
    }

    public static void DecrementGravity()
    {
        if (_modifiedGravityScale <= 0.5f)
        {
            float tmp = _modifiedGravityScale / 2f;
            _modifiedGravityScale -= tmp;
        }
        else
        {
            _modifiedGravityScale -= 0.5f;
        }
        ApplyGravity();
    }

    public static void SetRandomGravity()
    {
        ResetGravity();
        _modifiedGravityScale = UnityEngine.Random.Range(0.2f, 3.0f);
        ApplyGravity();
    }
}