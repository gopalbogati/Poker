

/* Coding By Gopal Bogati*/

/* Your task is to create a Visual Studio 2019 C# console application poker game. The
requirements are as follows.
1. Generate a deck of cards (52 unique cards) with each card having a suite and a
value i.e. Ace of Spades.
2. Randomly deal 4 hands of 5 unique cards for each hand.
3. Identify the type of hand. For example, Royal Flush, Four-of-a-kind... so on.
4. Compare each hand and identify which hand has the highest ranking.
5. This must be written in C# and compiled in Visual Studio 2019 in Windows.
*/


using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace PokerConsoleGame
{
	public class PokerHand
	{
		public static void Main()
		{
			List<PlayerWithRank> playerScores = new List<PlayerWithRank>();
			PackOfCardFactory pack;
			pack = new PackOfCardFactory();
			pack.shuffle(100);

			//Randomly deal 4 hands of 5 unique cards for each hand

			var playerNames = new String[] { "Royal flush", "Four-of-a-kind", "King Plus", "Great Gambler" };

			List<string>[] player = new List<string>[4];

			for (int h = 0; h < 4; h++)
				player[h] = new List<string>();
			//Five hands
			for (int i = 0; i < 5; i++)
				for (int j = 0; j < 4; j++)
					player[j].Add(pack.deal().toString());
			//Identify the type of hand.For example, Royal Flush, Four - of - a - kind... so on.
			for (int k = 0; k < 4; k++)
			{
				var playerName = playerNames[k];
				var totalMark = getRankingOfThePlayer(player[k]);
				List<string> sortedList = player[k].OrderBy(x => ApplyPaddingLeft(x)).OrderBy(g => CheckForCossespondingStringValue(g)).ToList();
				Console.WriteLine($"Player {playerNames[k]} {(k + 1)}: {string.Join("-", sortedList)} . Total Rank : {totalMark}");
				playerScores.Add(new PlayerWithRank { PlayerName = playerName, RankInNumber = totalMark });

			}


			var thePlayerWithHighestMark = playerScores.OrderByDescending(c => c.RankInNumber).ToList()[0];
			Console.WriteLine($"The player name with the highest ranking : {thePlayerWithHighestMark.PlayerName}. The Score is : {thePlayerWithHighestMark.RankInNumber}");
		}

		public class PlayerWithRank
		{
			public string PlayerName { get; set; }
			public int RankInNumber { get; set; }
		}

		// Compare each hand and identify which hand has the highest ranking.

		public static int getRankingOfThePlayer(List<string> Player)
		{
			int totalRankNumber = 0;
			foreach (var item in Player)
			{
				string nameOfCard = item.Substring(item.Length - 1);
				string number = item.Substring(0, item.Length - 1);
				if (!int.TryParse(number, out int parsedNumber))
				{
					switch (number)
					{
						case "A":
							parsedNumber = 14;
							break;
						case "K":
							parsedNumber = 13;
							break;
						case "Q":
							parsedNumber = 12;
							break;
						case "J":
							parsedNumber = 11;
							break;

					}
				}
				totalRankNumber += parsedNumber;
			}

			return totalRankNumber;
		}

		public static string ApplyPaddingLeft(string input)
		{
			return Regex.Replace(input, "[0-9]+", match => match.Value.PadLeft(2, '0'));
		}

		public static int CheckForCossespondingStringValue(string input)
		{
			return input.Substring(0, 1) == "A" ? 14 : input.Substring(0, 1) == "K" ? 13 : input.Substring(0, 1) == "Q" ? 12 : input.Substring(0, 1) == "J" ? 11 : 0;
		}
	}

	public class CardTemplate
	{
		public static readonly String[] Rank = { "*", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
		private static readonly String[] Suit = { "*", "D", "C", "H", "S" };
		private byte cardSuit;
		private byte cardRank;
		public CardTemplate(int suit, int rank)
		{
			if (rank == 1)
				cardRank = 14;
			else
				cardRank = (byte)rank;
			cardSuit = (byte)suit;
		}

		public int suit()
		{
			return (cardSuit);
		}

		public int rank()
		{
			return (cardRank);
		}

		public String toString()
		{
			return (Rank[cardRank] + Suit[cardSuit]);
		}
	}

	/*	Generate a deck of cards(52 unique cards) with each card having a suite and a
	 value i.e.Ace of Spades. */

	public class PackOfCardFactory
	{
		private const int totalNumberOfCards = 52;
		private CardTemplate[] deckOfCards;
		private int currentCard;
		private Random randNum;
		public PackOfCardFactory()
		{
			deckOfCards = new CardTemplate[totalNumberOfCards];
			int i = 0;
			for (int suit = 1; suit <= 4; suit++)
				for (int rank = 1; rank <= 13; rank++)
					deckOfCards[i++] = new CardTemplate(suit, rank);
			currentCard = 0;
		}

		public void shuffle(int n)
		{
			int i, j;
			randNum = new Random();
			for (int k = 0; k < n; k++)
			{
				i = (int)(randNum.Next(totalNumberOfCards));
				j = (int)(randNum.Next(totalNumberOfCards));
				CardTemplate tmp = deckOfCards[i];
				deckOfCards[i] = deckOfCards[j];
				deckOfCards[j] = tmp;
			}

			currentCard = 0;
		}

		public CardTemplate deal()
		{
			if (currentCard < totalNumberOfCards)
				return (deckOfCards[currentCard++]);
			else
				return (null);
		}
	}
}