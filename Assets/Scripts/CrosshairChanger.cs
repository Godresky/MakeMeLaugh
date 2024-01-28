using UnityEngine;
public class CrosshairChanger : MonoBehaviour
{
    [SerializeField] private GameObject _defaultCrosshair;
    [SerializeField] private GameObject _grabHandCrosshair;
    [SerializeField] private GameObject _interactHandCrosshair;
    [SerializeField] private GameObject _doubleHandCrosshair;

    private void Start()
    {
        SetDefault();
    }

    public void SetDefault()
    {
        _defaultCrosshair.SetActive(true);
        _grabHandCrosshair.SetActive(false);
        _interactHandCrosshair.SetActive(false);
        _doubleHandCrosshair.SetActive(false);
    }

    public void SetGrabHand()
    {
        _defaultCrosshair.SetActive(false);
        _grabHandCrosshair.SetActive(true);
        _interactHandCrosshair.SetActive(false);
        _doubleHandCrosshair.SetActive(false);
    }

    public void SetInteractHand()
    {
        _defaultCrosshair.SetActive(false);
        _grabHandCrosshair.SetActive(false);
        _interactHandCrosshair.SetActive(true);
        _doubleHandCrosshair.SetActive(false);
    }

    public void SetDoubleHand()
    {
        _defaultCrosshair.SetActive(false);
        _grabHandCrosshair.SetActive(false);
        _interactHandCrosshair.SetActive(false);
        _doubleHandCrosshair.SetActive(true);
    }
}
