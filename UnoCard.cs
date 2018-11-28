using System;
using System.Linq;

namespace Uno {
    public class UnoCard {
        public string color;
        public string type;
        private int val;

        // Constructors
        public UnoCard(string type, string color) {
            if(!data.numCards.Contains(type)) {
                this.val = Array.IndexOf(data.numCards, type);
            }
            else {
                this.val = 0;
            }
            this.type = type;
            if(type == "Wild" || type == "Draw 4 Wild") {
                this.color = " ";
            }
            else {
                this.color = color;
            }
        }

        // get methods
        public int getVal() {
            return val;
        }
        public string getColor() {
            return color;
        }
        public string getType() {
            return type;
        }
        public string toString() {
            return this.color + " " + this.type;
        }
    }
}