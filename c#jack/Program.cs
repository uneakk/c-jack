using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to Blackjack!");

        while (true)
        {
            PlayGame();
            Console.Write("Do you want to play again? (y/n): ");
            if (Console.ReadLine().ToLower() != "y")
            {
                Console.Clear();
                break;
            }
        }

        Console.WriteLine("Thanks for playing!");
    }

    static void PlayGame()
    {
        // Initialize and shuffle the deck
        List<Card> deck = InitializeDeck();
        ShuffleDeck(deck);

        // Initialize players
        List<Card> playerHand = new List<Card>();
        List<Card> dealerHand = new List<Card>();

        // Deal initial cards
        DealCard(playerHand, deck);
        DealCard(dealerHand, deck);
        DealCard(playerHand, deck);
        DealCard(dealerHand, deck);

        // Display initial hands
        Console.WriteLine($"\n  Your hand: \n{DisplayHand(playerHand)}\n");
        Console.WriteLine($"Dealer's hand: \n{DisplayPartialHand(dealerHand)}\n");

        // Player's turn
        while (CalculateHandValue(playerHand) < 21)
        {
            Console.Write("Do you want to hit or stand? (h/s): ");
            string choice = Console.ReadLine().ToLower();
            Console.Clear();



            if (choice == "h")
            {
                DealCard(playerHand, deck);
                Console.WriteLine($"\nYour hand: \n{DisplayHand(playerHand)}\n");
            }
            else if (choice == "s")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter 'h' or 's'.");
            }
        }

        // Dealer's turn
        while (CalculateHandValue(dealerHand) < 17)
        {
            DealCard(dealerHand, deck);
        }
        Console.Clear();

        // Display final hands
        Console.WriteLine($"\nYour hand: \n{DisplayHand(playerHand)}\n");
        Console.WriteLine($"Dealer's hand: \n{DisplayHand(dealerHand)}\n");

        // Determine the winner
        int playerScore = CalculateHandValue(playerHand);
        int dealerScore = CalculateHandValue(dealerHand);

        if (playerScore > 21 || (dealerScore <= 21 && dealerScore > playerScore))
        {
            Console.WriteLine("Dealer wins!");
        }
        else if (dealerScore > 21 || playerScore > dealerScore)
        {
            Console.WriteLine("You win!");
        }
        else
        {
            Console.WriteLine("It's a tie!");
        }
    }

    static List<Card> InitializeDeck()
    {
        List<Card> deck = new List<Card>();

        string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
        string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

        foreach (var suit in suits)
        {
            foreach (var rank in ranks)
            {
                deck.Add(new Card(rank, suit));
            }
        }

        return deck;
    }

    static void ShuffleDeck(List<Card> deck)
    {
        Random rng = new Random();
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card value = deck[k];
            deck[k] = deck[n];
            deck[n] = value;
        }
    }

    static void DealCard(List<Card> hand, List<Card> deck)
    {
        if (deck.Count > 0)
        {
            Card card = deck[0];
            hand.Add(card);
            deck.RemoveAt(0);
        }
        else
        {
            Console.WriteLine("No more cards in the deck!");
        }
    }

    static int CalculateHandValue(List<Card> hand)
    {
        int value = 0;
        int numAces = 0;

        foreach (var card in hand)
        {
            if (card.Rank == "Ace")
            {
                value += 11;
                numAces++;
            }
            else if (card.Rank == "Jack" || card.Rank == "Queen" || card.Rank == "King")
            {
                value += 10;
            }
            else
            {
                value += int.Parse(card.Rank);
            }
        }

        // Adjust for Aces
        while (value > 21 && numAces > 0)
        {
            value -= 10;
            numAces--;
        }

        return value;
    }

    static string DisplayHand(List<Card> hand)
    {
        string handString = "";
        foreach (var card in hand)
        {
            handString += $"{card.Rank} of {card.Suit}, ";
        }
        return handString.TrimEnd(',', ' ');
    }

    static string DisplayPartialHand(List<Card> hand)
    {
        string handString = $"{hand[0].Rank} of {hand[0].Suit}, [Hidden]";
        return handString;
    }
}

enum Suit
{
 Diamonds,
 Spades,
 Clubs, 
 Hearts
   
}

class Card
{
   private Suit suit;
   private int value;

   public Card(Suit suit, int value)
   {
    this.suit = suit;
    this.value = value;
   } 
    
public string printcard()
{
    return $"{value} of {suit}";
}

}
