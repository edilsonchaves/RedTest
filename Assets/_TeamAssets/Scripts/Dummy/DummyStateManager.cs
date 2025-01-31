using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyStateManager : MonoBehaviour
{

    private DummyBaseState _currentState;

    private DummyIdleState _idleState = new DummyIdleState();
    public DummyIdleState IdleState => _idleState;
    private DummyHurtState _hurtState = new DummyHurtState();
    public DummyHurtState HurtState => _hurtState;
    private float durationHurtAnimationTime = 1;
    private float elapsedHurtAnimationTime;
    private List<Material> _dummyMaterial = new List<Material>();
    [SerializeField] private AnimationCurve _dummyDamageCurve;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _audioClips;

    void Start()
    {
        _currentState = _idleState;
        _currentState.EnterState(this);
        var renderer = GetComponentsInChildren<MeshRenderer>();
        foreach (var render in renderer)
        {
            _dummyMaterial.Add(render.material);
        }
    }

    private void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(DummyBaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }

    public void ReceiveDamage()
    {
        SwitchState(_hurtState);
    }

    private void SetDummyHurtMaterialColor(float newValue)
    {
        foreach (var material in _dummyMaterial)
        {
            material.SetFloat("_FlashAmount", _dummyDamageCurve.Evaluate(newValue));
        }
    }

    public void StartAnimation(string coroutineMethodName)
    {
        StartCoroutine(coroutineMethodName);
    }

    private IEnumerator HurtCoroutine()
    {
        elapsedHurtAnimationTime = 0;
        while (elapsedHurtAnimationTime < durationHurtAnimationTime)
        {
            elapsedHurtAnimationTime += Time.deltaTime;

            var currentFlashAmount = Mathf.Lerp(0, 1, elapsedHurtAnimationTime / durationHurtAnimationTime);
            SetDummyHurtMaterialColor(currentFlashAmount);
            yield return null;

        }
    }
    public void ExecuteMusic(int musicIndex)
    {
        if (musicIndex >= _audioClips.Count)
            return;
        _audioSource.clip = _audioClips[musicIndex];
        _audioSource.Play();
    }
}
