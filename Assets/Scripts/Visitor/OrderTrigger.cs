using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderTrigger : MonoBehaviour
{
    [SerializeField]
    private List<Baking.Type> _allBakingTypes = new();
    [SerializeField]
    private List<DialogueTrigger> _allDialogueTriggers = new();

    [SerializeField]
    private int _choosenBaking;

    public int ChoosenBaking { get => _choosenBaking; }

    public void OrderBaking(VisitorAI visitor)
    {
        _choosenBaking = GetRandomBread();

        _allDialogueTriggers[_choosenBaking].OnEndDialogue += visitor.OrderedBaking;
        _allDialogueTriggers[_choosenBaking].TriggerRandomDialogue();
    }

    private int GetRandomBread()
    {
        int id = Random.Range(0, _allBakingTypes.Count);

        return id;
    }
}
