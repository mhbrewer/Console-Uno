using System;
using System.Collections.Generic;

namespace Uno {
    public class Deck {
        public List<UnoCard> cards;

        // Constructors
        public Deck() {
            build();
        }

        public void shuffle() {
            UnoCard temp;
            Random rand = new Random();
            int rNum;
            for(int ii = 0; ii < cards.Count; ii++) {
                rNum = rand.Next(0, cards.Count);
                temp = cards[ii];
                cards[ii] = cards[rNum];
                cards[rNum] = temp;
            }
        }
        public UnoCard deal() {
            if(cards.Count == 0) {
                return null;
            }
            UnoCard topCard = cards[0];
            cards.RemoveAt(0);
            return topCard;
        }
        public void build() {
            cards = new List<UnoCard>();
            foreach(var color in data.colors) {
                for(int ii = 0; ii <= 9; ii++) {
                    cards.Add(new UnoCard(data.numCards[ii], color));
                    cards.Add(new UnoCard(data.numCards[ii], color));
                }
                for(int ii = 0; ii <= 2; ii++) {
                    cards.Add(new UnoCard(data.otherCards[ii], color));
                    cards.Add(new UnoCard(data.otherCards[ii], color));
                }
            }
            for(int ii = 0; ii < 4; ii++) {
                cards.Add(new UnoCard(data.otherCards[3], "All"));
                cards.Add(new UnoCard(data.otherCards[4], "All"));
            }
        }

    }
}