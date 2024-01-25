using UnityEngine;
public class CrosshairChanger : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _crossair;
    [SerializeField] private GameObject _hand;
    public void Change(Color newColor){
        _crossair.gameObject.SetActive(false);
        _hand.SetActive(true);
    }

    public void SetDefault() {
        _crossair.gameObject.SetActive(true);
        _hand.SetActive(false);
    }
    
}
