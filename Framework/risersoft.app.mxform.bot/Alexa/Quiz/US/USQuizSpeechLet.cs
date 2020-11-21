using risersoft.shared.bot;
using System.Collections.Generic;
namespace CognitiveServiceRsMx.Speech.Quiz
{

    public class USQuizSpeechlet : RSQuizSpeechletBase<ItemUS>

    { 


        //This is the welcome message for when a user starts the skill without a specific intent.
        protected override string WELCOME_MESSAGE { get; set; } = "Welcome to the {0}!  You can ask me about any of the fifty states and their capitals, or you can ask me to start a quiz.  What would you like to do?";

        //This is the message a user will hear when they try to cancel or stop the skill, or when they finish a quiz.
        protected override string EXIT_SKILL_MESSAGE { get; set; } = "Thank you for playing the {0}!  Let's play again soon!";

        //This is the message a user will hear when they ask Alexa for help in your skill.
        protected override string HELP_MESSAGE { get; set; } = "I know lots of things about the United States.  You can ask me about a state or a capital, and I'll tell you what I know.  You can also test your knowledge by asking me to start a quiz.  What would you like to do?";


        //This is the message a user will hear when they start a quiz.
        protected override string START_QUIZ_MESSAGE { get; set; } = "OK.  I will ask you " + MAX_QUESTION.ToString() + " questions about the United States.";
        protected  override string SkillName { get; set; } = "United States Quiz Game";

        public override string[] PropertyNames()
        {
            return ItemUS.PropertyNames;
        }

        

        public override List<ItemUS> ItemsArray()
        {
            return ItemUS.ItemsArray();
        }
    }
}
