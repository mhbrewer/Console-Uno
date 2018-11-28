using System;
using System.Collections.Generic;
using System.Linq;

namespace Uno {
    public class Game {
        public Deck deck;
        public List<Player> players;
        public Queue<Player> turnQueue;
        public UnoCard lastCard;
        public string lastColor; 
        public int lastNum;

        // Constructor
        public Game() {
            this.players = new List<Player>();
            this.turnQueue = new Queue<Player>();
            // Start the Game
            Console.WriteLine("How Many Players are Playing?");
            int numPlayers = Convert.ToInt32(Console.ReadLine());
            for(int ii = 0; ii < numPlayers; ii++) {
                Console.WriteLine("Enter the name of the next player:");
                string name = Console.ReadLine();
                Player nextP = new Player(name);
                players.Add(nextP);
                Console.WriteLine(name + " has entered the game!");
            }
            this.deck = new Deck();
            deck.shuffle();
            // Deal 7 cards
            for(int ii = 0; ii < 7; ii++) {
                for(int jj = 0; jj < players.Count; jj++) {
                    players[jj].draw(deck);
                }
            }
            // Creating Turn Order
            for(int ii = 0; ii < players.Count; ii++) {
                turnQueue.Enqueue(players[ii]);
            }
            lastCard = null;
            lastColor = null;
        }

        // Bool is whether or not game is still going on.
        public bool play() {
            Player currentP = turnQueue.Dequeue();
            turnQueue.Enqueue(currentP);
            printGame(currentP);
            Console.WriteLine("Pick the card you wish to play:");
            Console.WriteLine();
            Console.WriteLine("0) Don't Play");
            for(int b = 0; b<currentP.hand.Count; b++ ) {
                Console.WriteLine($"{b+1}) {currentP.hand[b].toString()}");
            } 
            if(lastCard != null) {
                Console.WriteLine("******************************");
                assignColor();
                Console.WriteLine("Last card played is "+ lastCard.toString());
                if(lastCard.type.Equals("Wild") || lastCard.type.Equals("Draw 4 Wild")) {
                    Console.WriteLine("Color Chosen for Wild was " + lastColor);
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("******************************");
            }
            int input = 0;
            bool CanPlay = false;
            while(!CanPlay) {
                input = Convert.ToInt32(Console.ReadLine());
                if(input<=0  || input > currentP.hand.Count){
                    CanPlay = true;
                } 
                else if(lastCard == null) {
                    CanPlay = true;
                }
                else{
                    CanPlay = ValidPlay(currentP.hand[input - 1]);
                }
                if(!CanPlay) {
                    Console.WriteLine("Invalid play, please choose another card.");
                }
            }
            if(input<=0  ||input > currentP.hand.Count) {
                currentP.draw(deck);
            } else{
                UnoCard card = currentP.play(input-1);
                processCard(card);
            }
            if(currentP.hand.Count == 0) {
                Console.WriteLine($"{currentP.name} wins!!!!!!!!!!");
                return false;
            }
            if(deck.cards.Count == 0) {
                Console.WriteLine("You all suck, game was a draw.");
                return false;
            }
            return true;
        }

        // Bool is whether or not you can make this play with the given card.
        public bool ValidPlay(UnoCard thisCard){
            if(thisCard.type.Equals("Wild") || thisCard.type.Equals("Draw 4 Wild")) {
                return true;
            }
            if(thisCard.color.Equals(lastColor)){
                if(contains(data.otherCards, thisCard.type) || contains(data.otherCards, lastCard.type)) {
                    return true;
                } else if(Array.IndexOf(data.numCards, thisCard.type) >= lastNum) {
                    return true;
                }
            }
            if(thisCard.type.Equals(lastCard.type)) {
                return true;
            }
            return false;
        }

        // Procssing the card played.
        public void processCard(UnoCard thisCard) {
            lastCard = thisCard;
            lastColor = thisCard.color;
            if(thisCard.type.Equals("Wild")) {
                chooseColor();
                lastNum = 0;
            }
            if(thisCard.type.Equals("Draw 4 Wild")) {
                chooseColor();
                lastNum = 0;
                for(int ii = 0; ii < 4; ii++) {
                    turnQueue.Peek().draw(deck);
                }
            }
            if(thisCard.type.Equals("Skip")) {
                Player temp = turnQueue.Dequeue();
                turnQueue.Enqueue(temp);
                lastNum = 0;
            }
            if(thisCard.type.Equals("Reverse")) {
                reverse();
                lastNum = 0;
            }
            if(thisCard.type.Equals("Draw 2")) {
                for(int ii = 0; ii < 4; ii++) {
                    turnQueue.Peek().draw(deck);
                }
                lastNum = 0;
            }
            if(contains(data.numCards, thisCard.type)) {
                lastNum = Convert.ToInt32(thisCard.type);
            }
        }

        // Reversing the order of play.
        public void reverse(){
            int len = turnQueue.Count;
            Stack<Player> stack = new Stack<Player>();
            for(int ii = 0; ii < len - 1; ii++) {
                stack.Push(turnQueue.Dequeue());
            }
            Player temp = turnQueue.Dequeue();
            len = stack.Count;
            for(int ii = 0; ii < len; ii++) {
                turnQueue.Enqueue(stack.Pop());
            }
            turnQueue.Enqueue(temp);
        }

        // Choosign the color.
        public void chooseColor() {
            Console.WriteLine("What color would you like to play on?");
            for(int ii = 0; ii < data.colors.Length; ii++) {
                Console.WriteLine($"{ii}) {data.colors[ii]}");
            }
            int input = Convert.ToInt32(Console.ReadLine());
            lastColor = data.colors[input];
        }

        // Prints out the game.
        public void printGame(Player currentP) {
            Player temp;
            for(int k = 0; k < turnQueue.Count - 1; k++)
            {
                temp=turnQueue.Dequeue();
                Console.WriteLine("******************************");
                Console.WriteLine(temp.name);
                Console.WriteLine("******************************");
                turnQueue.Enqueue(temp);
            }
            temp=turnQueue.Dequeue();
            turnQueue.Enqueue(temp);
            Console.WriteLine("******************************");
            Console.WriteLine($"It is {currentP.name}'s turn.");


        }

        // Writing our own contains method.
        public bool contains(String[] arr, string str) {
            for(int ii = 0; ii < arr.Length; ii++) {
                if(arr[ii].Equals(str)) {
                    return true;
                }
            }
            return false;
        }
        // Troubleshooting
        public void printQueue() {
            Player[] arr = turnQueue.ToArray();
            for(int ii = 0; ii < turnQueue.Count; ii++) {
                Console.WriteLine(arr[ii].name);
            }
        }
        // Determining which color to make our text.
        public void assignColor() {
            if(lastColor.Equals("Yellow")) {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            if(lastColor.Equals("Blue")) {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            if(lastColor.Equals("Red")) {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            if(lastColor.Equals("Green")) {
                Console.ForegroundColor = ConsoleColor.Green;
            }
        }
    }
}