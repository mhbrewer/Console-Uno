using System;

namespace Uno{
    class Program
    {
        static void Main(string[] args)
        {
            bool gameOn = true;
            Game game = new Game();
            while(gameOn) {
                gameOn = game.play();
            }
        }
        // static void Main(string[] args) {
        //     Deck deck = new Deck();
        //     deck.shuffle();
        //     Player[] players = new player
        //     for(int ii = 0; ii < 7; ii++) {
        //         for(int jj = 0; jj < players.Length; jj++) {
        //             players[jj].draw(deck);
        //         }
        //     }

        // }
    }
}
