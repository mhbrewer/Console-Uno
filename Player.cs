using System.Collections.Generic;

namespace Uno {
    public class Player {
        public string name;
        public List<UnoCard> hand;

        // Constructor
        public Player(string name) {
            hand = new List<UnoCard>();
            this.name = name;
        }

        public UnoCard draw(Deck deck) {
            UnoCard next = deck.deal();
            hand.Add(next);
            return next;
        }
        public UnoCard play(int idx){
            if(idx < 0 || idx >= hand.Count) {
                return null;
            }
            UnoCard card = hand[idx];
            hand.RemoveAt(idx);
            return card;
        }
        // get Methods
        public string[] look() {
            string[] handString = new string[this.hand.Count];
            for(int ii = 0; ii < hand.Count; ii++) {
                handString[ii] = this.hand[ii].toString();
            }
            return handString;
        }
        public List<UnoCard> getHand() {
            return hand;
        }
        public string getName() {
            return this.name;
        }

    }
}