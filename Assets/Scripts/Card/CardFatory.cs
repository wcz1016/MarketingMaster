using CsvHelper;
using System.Globalization;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardFatory : MonoBehaviour
{
    private List<Card> allCards;
    private List<List<int>> roundIndexToCardList;

    public TextAsset AddressCardsCSV;
    public TextAsset StrategyCardsCSV;
    public TextAsset RivalCardsCSV;

    private void Awake()
    {
        roundIndexToCardList = new List<List<int>>(GameControl.WinningRounds);
        for (int i = 0; i < GameControl.WinningRounds; i++)
        {
            roundIndexToCardList.Add(new List<int>());
        }
        allCards = new List<Card>();
        int cardIndex = 0;
        // read Address Cards
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
                roundIndexToCardList[0].Add(cardIndex);
                ++cardIndex;
            }
        }
        // read Strategy Cards
        using (var reader = new StreamReader(new MemoryStream((StrategyCardsCSV.bytes))))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                var strategyCard = csv.GetRecord<StrategyCard>();
                allCards.Add(strategyCard);
                ReadRounds(csv.GetField("AppearenceRound"), cardIndex);
                ++cardIndex;
            }
        }

        DebugCard();
    }

    public List<Card> DrawCards(int round, int cardNum)
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

    private void ReadRounds(string roundNumStr, int cardIndex)
    {
        string[] roundNums = roundNumStr.Split('-');
        int startRound = Int32.Parse(roundNums[0]) - 1;
        int endRound = Int32.Parse(roundNums[roundNums.Length - 1]) - 1;
        for (int i = startRound; i <= endRound; ++i)
        {
            roundIndexToCardList[i].Add(cardIndex);
        }
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

