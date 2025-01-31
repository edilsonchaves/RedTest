using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpecialPointBar : MonoBehaviour
{
    [SerializeField] private Image _currentSpecialPointBar;
    [SerializeField] private TextMeshProUGUI _specialPointText;
    [SerializeField] private AudioSource _audioSource;
    private const int FULL_SPECIAL_BAR = 1;
    public void SetSpecialPointBar(float currentAvatarSpecialPoint, float maxAvatarSpecialPoint, bool canUseSpecialAttack)
    {
        if(currentAvatarSpecialPoint < maxAvatarSpecialPoint)
        {
            var relativeSpecialPoint = currentAvatarSpecialPoint / maxAvatarSpecialPoint;
            _currentSpecialPointBar.transform.localScale = new Vector3(relativeSpecialPoint, _currentSpecialPointBar.transform.localScale.y, _currentSpecialPointBar.transform.localScale.z);
            _specialPointText.text = $"{currentAvatarSpecialPoint} / {maxAvatarSpecialPoint}";

        }
        else
        {
            _currentSpecialPointBar.transform.localScale = new Vector3(FULL_SPECIAL_BAR, _currentSpecialPointBar.transform.localScale.y, _currentSpecialPointBar.transform.localScale.z);
            _specialPointText.text = $"Special Attack";
            if(!canUseSpecialAttack)
                _audioSource.Play();
        }
    }
}
