using risersoft.shared.bot;
using risersoft.shared.dotnetfx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CognitiveServiceRsMx.Speech.Quiz
{
    public class ItemUS:QuizItem

    {
        const string ABBREVIATION = "Abbreviation";
        const string CAPITAL = "Capital";
        const string STATEHOODYEAR = "StatehoodYear";
        const string STATEHOODORDER = "StatehoodOrder";
        const string STATE_HOOD_YEAR = "Statehood Year";
        const string STATE_HOOD_ORDER = "Statehood Order";
        const string STATENAME = "StateName";
        const string STATE_NAME = "State Name";

        protected override string REPROMPT_SPEECH { get; set; } = "Which other state or capital would you like to know about?";

        public string StateName { get; set; }
        public string Abbreviation { get; set; }
        public string Capital { get; set; }
        public int StatehoodYear { get; set; }
        public int StatehoodOrder { get; set; }

        public ItemUS(string _stateName, string _abbreviation, string _capital, int _statehoodYear, int _statehoodOrder)
        {
            StateName = _stateName;
            Abbreviation = _abbreviation;
            Capital = _capital;
            StatehoodYear = _statehoodYear;
            StatehoodOrder = _statehoodOrder;
        }

        /// <summary>
        /// return the string value of the specific property
        /// </summary>
        /// <param name="property"></param>
        /// <returns>string</returns>
        public override string PropertyValue(string property)
        {
            string ret = "";
            switch (property)
            {
                case ABBREVIATION: ret = this.Abbreviation; break;
                case CAPITAL: ret = this.Capital; break;
                case STATE_HOOD_YEAR:
                case STATEHOODYEAR: ret = this.StatehoodYear.ToString(); break;
                case STATE_HOOD_ORDER:
                case STATEHOODORDER: ret = this.StatehoodOrder.ToString(); break;
                case STATE_NAME:
                case STATENAME: ret = this.StateName; break;
            }
            return ret;
        }

        public static string[] PropertyNames = new string[5] { ABBREVIATION, CAPITAL, STATE_HOOD_YEAR, STATE_HOOD_ORDER, STATE_NAME };

        /// <summary>
        /// return the names and values of the properties as newline separated string
        /// </summary>
        /// <param name="property"></param>
        /// <returns>string</returns>
        public override string GetFormatedText()
        {
            string text = STATE_NAME + ": " + this.StateName + "\n";
            foreach (string name in ItemUS.PropertyNames)
            {
                text += name + ": " + this.PropertyValue(name) + "\n";
            }
            return text;
        }
        /// <summary>
        /// return the list of states
        /// </summary>
        /// <param name="property"></param>
        /// <returns>List<Item> </returns>
        public static List<ItemUS> ItemsArray()
        {
            List<ItemUS> theItems = new List<ItemUS>();
            theItems = new List<ItemUS>();
            theItems.Add(new ItemUS("Alabama", "AL", "Montgomery", 1819, 22));
            theItems.Add(new ItemUS("Alaska", "AK", "Juneau", 1959, 49));
            theItems.Add(new ItemUS("Arizona", "AZ", "Phoenix", 1912, 48));
            theItems.Add(new ItemUS("Arkansas", "AR", "Little Rock", 1836, 25));
            theItems.Add(new ItemUS("California", "CA", "Sacramento", 1850, 31));
            theItems.Add(new ItemUS("Colorado", "CO", "Denver", 1876, 38));
            theItems.Add(new ItemUS("Connecticut", "CT", "Hartford", 1788, 5));
            theItems.Add(new ItemUS("Delaware", "DE", "Dover", 1787, 1));
            theItems.Add(new ItemUS("Florida", "FL", "Tallahassee", 1845, 27));
            theItems.Add(new ItemUS("Georgia", "GA", "Atlanta", 1788, 4));
            theItems.Add(new ItemUS("Hawaii", "HI", "Honolulu", 1959, 50));
            theItems.Add(new ItemUS("Idaho", "ID", "Boise", 1890, 43));
            theItems.Add(new ItemUS("Illinois", "IL", "Springfield", 1818, 21));
            theItems.Add(new ItemUS("Indiana", "IN", "Indianapolis", 1816, 19));
            theItems.Add(new ItemUS("Iowa", "IA", "Des Moines", 1846, 29));
            theItems.Add(new ItemUS("Kansas", "KS", "Topeka", 1861, 34));
            theItems.Add(new ItemUS("Kentucky", "KY", "Frankfort", 1792, 15));
            theItems.Add(new ItemUS("Louisiana", "LA", "Baton Rouge", 1812, 18));
            theItems.Add(new ItemUS("Maine", "ME", "Augusta", 1820, 23));
            theItems.Add(new ItemUS("Maryland", "MD", "Annapolis", 1788, 7));
            theItems.Add(new ItemUS("Massachusetts", "MA", "Boston", 1788, 6));
            theItems.Add(new ItemUS("Michigan", "MI", "Lansing", 1837, 26));
            theItems.Add(new ItemUS("Minnesota", "MN", "St. Paul", 1858, 32));
            theItems.Add(new ItemUS("Mississippi", "MS", "Jackson", 1817, 20));
            theItems.Add(new ItemUS("Missouri", "MO", "Jefferson City", 1821, 24));
            theItems.Add(new ItemUS("Montana", "MT", "Helena", 1889, 41));
            theItems.Add(new ItemUS("Nebraska", "NE", "Lincoln", 1867, 37));
            theItems.Add(new ItemUS("Nevada", "NV", "Carson City", 1864, 36));
            theItems.Add(new ItemUS("New Hampshire", "NH", "Concord", 1788, 9));
            theItems.Add(new ItemUS("New Jersey", "NJ", "Trenton", 1787, 3));
            theItems.Add(new ItemUS("New Mexico", "NM", "Santa Fe", 1912, 47));
            theItems.Add(new ItemUS("New York", "NY", "Albany", 1788, 11));
            theItems.Add(new ItemUS("North Carolina", "NC", "Raleigh", 1789, 12));
            theItems.Add(new ItemUS("North Dakota", "ND", "Bismarck", 1889, 39));
            theItems.Add(new ItemUS("Ohio", "OH", "Columbus", 1803, 17));
            theItems.Add(new ItemUS("Oklahoma", "OK", "Oklahoma City", 1907, 46));
            theItems.Add(new ItemUS("Oregon", "OR", "Salem", 1859, 33));
            theItems.Add(new ItemUS("Pennsylvania", "PA", "Harrisburg", 1787, 2));
            theItems.Add(new ItemUS("Rhode Island", "RI", "Providence", 1790, 13));
            theItems.Add(new ItemUS("South Carolina", "SC", "Columbia", 1788, 8));
            theItems.Add(new ItemUS("South Dakota", "SD", "Pierre", 1889, 40));
            theItems.Add(new ItemUS("Tennessee", "TN", "Nashville", 1796, 16));
            theItems.Add(new ItemUS("Texas", "TX", "Austin", 1845, 28));
            theItems.Add(new ItemUS("Utah", "UT", "Salt Lake City", 1896, 45));
            theItems.Add(new ItemUS("Vermont", "VT", "Montpelier", 1791, 14));
            theItems.Add(new ItemUS("Virginia", "VA", "Richmond", 1788, 10));
            theItems.Add(new ItemUS("Washington", "WA", "Olympia", 1889, 42));
            theItems.Add(new ItemUS("West Virginia", "WV", "Charleston", 1863, 35));
            theItems.Add(new ItemUS("Wisconsin", "WI", "Madison", 1848, 30));
            theItems.Add(new ItemUS("Wyoming", "WY", "Cheyenne", 1890, 44));
            return theItems;
        }

        protected  override string GetSpeechDescriptionWithCard()
        {
            return this.StateName + " is the " + this.StatehoodOrder + "th state, admitted to the Union in " + this.StatehoodYear +
     ". The capital of " + this.StateName + " is " + this.Capital + ", and the abbreviation for " + this.StateName +
     " is " + breakstrong + sayas_spellout + this.Abbreviation + sayas + ".  I've added " + this.StateName
     + " to your Alexa app.  " + REPROMPT_SPEECH;

        }

        protected override string GetSpeechDescription()
        {
            //   return this.StateName + " is the " + this.StatehoodOrder + "th state, admitted to the Union in " + this.StatehoodYear +
            //     ". The capital of " + this.StateName + " is " + this.Capital + ", and the abbreviation for " + this.StateName +
            //   " is " + breakstrong + sayas_spellout + this.Abbreviation + sayas + ". Which other state or capital would you like to know about?";

            return this.StateName + " is the " + this.StatehoodOrder + "th state, admitted to the Union in " + this.StatehoodYear +
           ". The capital of " + this.StateName + " is " + this.Capital + ", and the abbreviation for " + this.StateName +
           " is " + this.Abbreviation + ". " + REPROMPT_SPEECH;
        }

        protected override string GetQuestion(int counter, string property)
        {
            return "Here is your " + counter.ToString() + "th question.  What is the " + property + " of " + this.StateName + "?";
            /*
            switch(property)
            {
                case "City":
                    return "Here is your " + counter + "th question.  In what city do the " + this.League + "'s "  + this.Mascot + " play?";
                break;
                case "Sport":
                    return "Here is your " + counter + "th question.  What sport do the " + this.City + " " + this.Mascot + " play?";
                break;
                case "HeadCoach":
                    return "Here is your " + counter + "th question.  Who is the head coach of the " + this.City + " " + this.Mascot + "?";
                break;
                default:
                    return "Here is your " + counter + "th question.  What is the " + property + " of the "  + this.Mascot + "?";
                break;
            }
            */
        }

        protected  override string GetAnswer(string property)
        {
            string ret = string.Empty;
            switch (property)
            {
                case "Abbreviation":
                    ret = "The " + property + " of " + this.StateName + " is " + sayas_spellout + this.PropertyValue(property) + sayas + ". ";
                    break;
                default:
                    ret = "The " + property + " of " + this.StateName + " is " + this.PropertyValue(property) + ". ";
                    break;
            }
            return ret;
        }

        protected  override string GetCardTitle()
        {
            return this.StateName;
        }

        protected override string GetSmallImage()
        {
            return "https://m.media-amazon.com/images/G/01/mobile-apps/dex/alexa/alexa-skills-kit/tutorials/quiz-game/state_flag/720x400/"
           + this.Abbreviation + "._TTH_.png";
        }

        protected override string GetLargeImage()
        {
            return "https://m.media-amazon.com/images/G/01/mobile-apps/dex/alexa/alexa-skills-kit/tutorials/quiz-game/state_flag/1200x800/"
                  + this.Abbreviation + "._TTH_.png";
        }

        protected  override bool Hasdata()
        {
            return (this.Capital !=null);
        }
    }
}