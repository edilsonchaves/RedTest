using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpecialPointBar : MonoBehaviour
{
    [SerializeField] private Image _currentSpecialPointBar;
    [SerializeField] private TextMeshProUGUI _specialPointText;
    [SerializeField] private AudioSource _audioSource;

    public void SetSpecialPointBar(float currentAvatarSpecialPoint, float maxAvatarSpecialPoint, bool canUseSpecialAttack)
    {
        var relativeSpecialPoint = currentAvatarSpecialPoint / maxAvatarSpecialPoint;
        _currentSpecialPointBar.transform.localScale = new Vector3(relativeSpecialPoint, _currentSpecialPointBar.transform.localScale.y, _currentSpecialPointBar.transform.localScale.z);
        if(currentAvatarSpecialPoint < maxAvatarSpecialPoint)
        {
            _specialPointText.text = $"{currentAvatarSpecialPoint} / {maxAvatarSpecialPoint}";

        }
        else
        {
            _specialPointText.text = $"Special Attack";
            if(!canUseSpecialAttack)
                _audioSource.Play();
        }
    }
}
