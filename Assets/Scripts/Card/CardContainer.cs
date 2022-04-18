using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardContainer : MonoBehaviour
{
    private Card _card;
    [SerializeField]
    private PlayerIndex _playerIndex;

    public PlayerIndex PlayerIndex { get => _playerIndex; set => _playerIndex = value; }

    public void ExecuteCard()
    {
        _card.Execute(_playerIndex);
    }

    public void SetCard(Card card)
    {
        _card = card;
        var cardSprite = Resources.Load<Sprite>($"Image/Cards/{card.Name}");
        gameObject.GetComponent<Image>().sprite = cardSprite;
    }
}
