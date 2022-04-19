using CsvHelper;
using System.Globalization;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
using CsvHelper.Configuration;

public class CardManager : MonoBehaviour
{
    private List<Card> allCards;
    private List<List<int>> roundIndexToCardList;

    public TextAsset AddressCardsCSV;
    public TextAsset StrategyCardsCSV;
    public TextAsset RivalCardsCSV;

    public Transform LeftCardArea;
    public Transform RightCardArea;

    public GameObject CardContainerPrefab;

    public static CardManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        roundIndexToCardList = new List<List<int>>();
        for (int i = 0; i < GameControl.WinningRounds + 1; i++)
        {
            roundIndexToCardList.Add(new List<int>());
        }

        allCards = new List<Card>();

        ReadAddressCard();
        ReadStrategyCard();
        ReadRivalCard();

        DebugCard();
    }

    private void ReadAddressCard()
    {
        using (var reader = new StreamReader(new MemoryStream((AddressCardsCSV.bytes))))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                var addressCard = csv.GetRecord<AddressCard>();
                allCards.Add(addressCard);
                // all address cards will appear in first round
                roundIndexToCardList[0].Add(allCards.Count - 1
);
            }
        }
    }

    private void ReadStrategyCard()
    {
        using (var reader = new StreamReader(new MemoryStream((StrategyCardsCSV.bytes))))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                var strategyCard = csv.GetRecord<StrategyCard>();
                allCards.Add(strategyCard);
                ReadRoundsFromCSV(csv.GetField("AppearenceRound"), allCards.Count - 1);
            }
        }
    }

    private void ReadRivalCard()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            MissingFieldFound = null
        };

        using (var reader = new StreamReader(new MemoryStream((RivalCardsCSV.bytes))))
        using (var csv = new CsvReader(reader, config))
        {
            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                var strategyCard = csv.GetRecord<RivalCard>();
                allCards.Add(strategyCard);
                ReadRoundsFromCSV(csv.GetField("AppearenceRound"), allCards.Count - 1);
            }
        }
    }

    public void DrawCards(int roundsNum)
    {
        if (LeftCardArea.childCount != 0 || RightCardArea.childCount != 0)
        {
            return;
        }

        List<Card> leftCards = GetRandomCards(roundsNum, 3);
        List<Card> rightCards = GetRandomCards(roundsNum, 3);

        for (int i = 0; i < 3; i++)
        {
            var leftCardGameObject = Instantiate(CardContainerPrefab, LeftCardArea);
            var rightCardGameObject = Instantiate(CardContainerPrefab, RightCardArea);

            leftCardGameObject.GetComponent<CardContainer>().SetCard(leftCards[i]);
            leftCardGameObject.GetComponent<CardContainer>().PlayerIndex = PlayerIndex.PlayerOne;

            rightCardGameObject.GetComponent<CardContainer>().SetCard(rightCards[i]);
            rightCardGameObject.GetComponent<CardContainer>().PlayerIndex = PlayerIndex.PlayerTwo;

            Debug.Log($"Player One Draw {i} card: {leftCards[i].Name}");
            Debug.Log($"Player Two Draw {i} card: {rightCards[i].Name}");
        }
    }

    private List<Card> GetRandomCards(int round, int cardNum)
    {
        var cardList = roundIndexToCardList[round];
        List<Card> DrawnCards = new List<Card>();
        for (int i = 0; i < cardNum; i++)
        {
            int randomIndex = Random.Range(i, cardList.Count - 1);
            DrawnCards.Add(allCards[cardList[randomIndex]]);

            int temp = cardList[randomIndex];
            cardList[randomIndex] = cardList[i];
            cardList[i] = temp;
        }
        return DrawnCards;
    }

    private void ReadRoundsFromCSV(string roundNumStr, int cardIndex)
    {
        string[] roundNums = roundNumStr.Split('-');
        int startRound = Int32.Parse(roundNums[0]) - 1;
        int endRound = Int32.Parse(roundNums[roundNums.Length - 1]) - 1;
        for (int i = startRound; i <= endRound; ++i)
        {
            roundIndexToCardList[i].Add(cardIndex);
        }
    }

    public void DestroyAllCards(PlayerIndex playerIndex)
    {
        var area = playerIndex == PlayerIndex.PlayerOne ? LeftCardArea : RightCardArea;
        for (int i = 0; i < area.transform.childCount; i++)
        {
            Destroy(area.transform.GetChild(i).gameObject);
        }
    }

    public void DestroyAllOtherCards(int cardIndex, PlayerIndex playerIndex)
    {
        var area = playerIndex == PlayerIndex.PlayerOne ? LeftCardArea : RightCardArea;
        for (int i = 0; i < area.transform.childCount; i++)
        {
            if (i != cardIndex)
            {
                Destroy(area.GetChild(i).gameObject);
            }
        }
    }

    public void ExecuteCard(int cardIndex, PlayerIndex playerIndex)
    {
        Debug.Log($"{playerIndex} has executed card {cardIndex}");
        var cardArea = playerIndex == PlayerIndex.PlayerOne ? LeftCardArea : RightCardArea;
        cardArea.GetChild(cardIndex).GetComponent<CardContainer>().ExecuteCard();
        cardArea.GetChild(0).GetComponent<Animator>().SetTrigger("issel");
        DestroyAllOtherCards(cardIndex, playerIndex);
    }

    private void DebugCard()
    {
        foreach (var card in allCards)
        {
            Debug.Log(card.ToString());
        }

        for (int i = 0; i < roundIndexToCardList.Count; ++i)
        {
            var list = roundIndexToCardList[i];
            string cardIndicesThisRound = "";
            foreach (var index in list)
            {
                cardIndicesThisRound += $"{index},";
            }
            Debug.Log($"Round{i}: {cardIndicesThisRound}\n");
        }
    }
}

