using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class Bowl : MonoBehaviour
{
    [Header("Dough")]
    [SerializeField] private Button _makeDoughButton;
    [SerializeField] private Animation _makingAnimation;
    [SerializeField] private Dough _dough;
    [SerializeField] protected Vector3 _doughPosition;
    [Space(3)]
    [SerializeField] private Fridge _fridge;

    private int _ingridients = 0 ;
    private void OnTriggerEnter(Collider other){
        if(other.TryGetComponent(out DoughIngridient ingridient)){
            _ingridients++;

            if(other.TryGetComponent(out Egg egg) && egg.IsGood == false)
                //error;

            if(_ingridients == 4){
                _makeDoughButton.onClick.AddListener(OnMakeDoughButtonClick);
                _ingridients = 0;
            }
        }
    }

    private void OnMakeDoughButtonClick(){
        _makingAnimation.Play();
        _fridge.UpdateFridge();

        Instantiate(_dough.gameObject, _doughPosition, Quaternion.identity);
        _makeDoughButton.onClick.RemoveListener(OnMakeDoughButtonClick);
    }
}
