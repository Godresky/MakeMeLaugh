using UnityEngine;
public class CrosshairColorChanger : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _sprite;
    [SerializeField] private Color _defaultColor;
    public void ChangeColor(Color newColor){
        _sprite.color = newColor;
    }

    public void SetDefaultColor() { 
        _sprite.color = _defaultColor;
    }
    
}
