using UnityEngine;

public class VillagerAnimator : MonoBehaviour {

    private const string SPEED = "Speed";
    private Animator _anim;
    private Villager _villager;
    

    private void Awake() {
        _anim = GetComponent<Animator>();
        _villager = GetComponentInParent<Villager>();
    }
    private void Start() {
        _villager.OnSpeedChanged += Villager_OnSpeedChanged;
    }

    private void Villager_OnSpeedChanged(float speed) {
        _anim.SetFloat(SPEED, speed, 0.1f, Time.deltaTime);
        //_anim.SetFloat(SPEED, speed);
    }
} 