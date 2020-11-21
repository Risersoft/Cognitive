using risersoft.shared.bot;
using risersoft.shared.dotnetfx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CognitiveServiceRsMx.Speech.Quiz
{
    public class ItemIN : QuizItem

    {
        const string ABBREVIATION = "Abbreviation";
        const string CAPITAL = "Capital";
        const string STATENAME = "StateName";
        const string STATE_NAME = "State Name";
        const string CHIEF = "Chief";
        const string FOOD = "Food";
        const string DANCE = "Dance";

        protected override string REPROMPT_SPEECH { get; set; } = "Which other state or capital would you like to know about?";


        public string StateName { get; set; }
        public string Abbreviation { get; set; }
        public string Capital { get; set; }
        public string Chief { get; set; }
        public string Food { get; set; }
        public string Dance { get; set; }

        public ItemIN(string _stateName, string _abbreviation, string _capital, string _chief, string _food, string _dance)
        {
            StateName = _stateName;
            Abbreviation = _abbreviation;
            Capital = _capital;
            Chief = _chief;
            Food = _food;
            Dance = _dance;
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
                case CHIEF: ret = this.Chief.ToString(); break;
                case FOOD: ret = this.Food.ToString(); break;
                case DANCE: ret = this.Dance.ToString();break;
                case STATE_NAME:
                case STATENAME: ret = this.StateName; break;
            }
            return ret;
        }

        public static string[] PropertyNames = new string[6] { ABBREVIATION, CAPITAL,CHIEF, STATE_NAME, DANCE, FOOD };

        /// <summary>
        /// return the names and values of the properties as newline separated string
        /// </summary>
        /// <param name="property"></param>
        /// <returns>string</returns>
        public override string GetFormatedText()
        {
            string text = STATE_NAME + ": " + this.StateName + "\n";
            foreach (string name in ItemIN.PropertyNames)
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
        public static List<ItemIN> ItemsArray()
        {
            List<ItemIN> theItems = new List<ItemIN>();
            theItems = new List<ItemIN>
            {
                new ItemIN("Andhra Pradesh","AP","Amravathi","Chandrababu Naidu","gongura pachadi"," kuchipudi"),
                new ItemIN("Arunachal Pradesh","AR","Itanagar","Pema Khandu","SMOKED PORK","bardo chham"),
                new ItemIN("Assam","AS","Dispur","Sarbananda Sonowal","masor tenga","bihu"),
                new ItemIN("Bihar","BR","Patna","Nitish Kumar","litti chokha","Kathaputli"),
                new ItemIN("Chhattisgarh","CG","Raipur","Raman Singh","dehrori","raut nacha"),
                new ItemIN("Goa","GA","Panaji","Manohar Parrikar","goan fish curry","fugdi"),
                new ItemIN("Gujarat","GJ","Gandhinagar","Vijay Rupani","dhokhla","garba"),
                new ItemIN("Haryana","HR","Chandigarh","Manohar Lal Khatter","bajra khichdi","ghumar"),
                new ItemIN("Himachal Pradesh","HP","Simla","Jai Ram Thakur","madra","nati"),
                new ItemIN("Jammu and Kashmir","JK","Srinagar","Mehbooba Mohammed Sayeed","mutton biryani","rouff"),
                new ItemIN("Jharkhand","JH","Ranchi","Raghubar Das","sattu ka parantha",""),
                new ItemIN("Karnataka","KA","Bengaluru","Siddaramaiah","pandi curry","bharatnatyam"),
                new ItemIN("Kerala","KL","Thiruvananthapuram","Pinarayi Vijayan","Irachi ishtu","kathakali"),
                new ItemIN("Madhya Pradesh","MP","Bhopal","Shivraj Singh Chauhan","Bhutte ki kees","matki dance"),
                new ItemIN("Maharashtra","MH","Mumbai","Devendra Fadnavis","puran poli","lavani"),
                new ItemIN("Manipur","MN","Imphal","Biren Singh","thambou singjou","manipuri"),
                new ItemIN("Meghalaya","ML","Shillong","Mukul Sangma","jadoh","laho"),
                new ItemIN("Mizoram","MZ","Aizawl","Pu Lalthanhawla","momos","cheraw dance"),
                new ItemIN("Nagaland","NL","Kohima","T.R. Zeliang"," bamboo shoot fry","chang lo"),
                new ItemIN("Odisha","OR","Bhubaneshwar","Naveen Patnaik","rasgulla","ghumuru"),
                new ItemIN("Punjab","PB","Chandigarh","Captain Amrinder Singh","makke ki roti and sarson ka saag","bhangra"),
                new ItemIN("Rajasthan","RJ","Jaipur","Vasundhara Raje","dal batti churma","kalbelia"),
                new ItemIN("Sikkim","SK","Gangtok","Pawan Chamling","momos","singhi chham"),
                new ItemIN("Tamil Nadu","TN","Chennai","Edapadi Palanisamy","sambhar idli","bharatnatyam"),
                new ItemIN("Telangana","TS","Hyderabad","K Chandrashekhar Rao","natu kodi pulusu","Perini Shivatandavam"),
                new ItemIN("Tripura","TR","Agartala","Manik Sarkar","Awan Bangwi","Hojagiri"),
                new ItemIN("Uttar Pradesh","UP","Lucknow","Yogi Adityanath","puri sabzi","kathak"),
                new ItemIN("Uttarakhand","UK","Dehradun","Trivendra Singh Rawat"," chainsoo","Chholiya"),
                new ItemIN("West Bengal","WB","Kolkata","Mamata Banerjee","fish curry","chhau")
            };
            return theItems;
        }

        protected  override string GetSpeechDescriptionWithCard()
        {
            return $"{this.StateName} has its chief minister as {this.Chief}. " +
                $"The capital of {this.StateName} is {this.Capital}, dance is {this.Dance}, food is {this.Food} "+
                $"and the abbreviation for {this.StateName} is " + breakstrong + sayas_spellout + this.Abbreviation + sayas + ". " +
                $"I've added {this.StateName} to your Alexa app.  " + REPROMPT_SPEECH;

        }

        protected  override string GetSpeechDescription()
        {
            //   return this.StateName + " is the " + this.StatehoodOrder + "th state, admitted to the Union in " + this.StatehoodYear +
            //     ". The capital of " + this.StateName + " is " + this.Capital + ", and the abbreviation for " + this.StateName +
            //   " is " + breakstrong + sayas_spellout + this.Abbreviation + sayas + ". Which other state or capital would you like to know about?";

            return $"{this.StateName} has its chief minister as {this.Chief}. " +
                $"The capital of {this.StateName} is {this.Capital}, dance is {this.Dance}, food is {this.Food} " +
                $"and the abbreviation for {this.StateName} is {this.Abbreviation}.  " + REPROMPT_SPEECH;

        }

        protected  override string GetQuestion(int counter, string property)
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

        protected override string GetCardTitle()
        {
            return this.StateName;
        }

        protected override string GetSmallImage()
        {
            return "https://m.media-amazon.com/images/G/01/mobile-apps/dex/alexa/alexa-skills-kit/tutorials/quiz-game/state_flag/720x400/"
           + this.Abbreviation + "._TTH_.png";
        }

        protected  override string GetLargeImage()
        {
            return "https://m.media-amazon.com/images/G/01/mobile-apps/dex/alexa/alexa-skills-kit/tutorials/quiz-game/state_flag/1200x800/"
                  + this.Abbreviation + "._TTH_.png";
        }

        protected override bool Hasdata()
        {
            return (this.Capital != null);
        }
    }
}