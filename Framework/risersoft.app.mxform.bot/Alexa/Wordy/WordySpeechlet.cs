using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Newtonsoft.Json;
using risersoft.shared;
using risersoft.shared.bot;
using risersoft.shared.web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CognitiveServiceRsMx.Speech.Wordy
{
    public class WordySpeechlet : RSSpeechletBase

    {

        protected enum AppState
        {
            Start,
            Game
        }

        protected override  string SkillName { get; set; } = "Voodoo World of Words";

        //This is the welcome message for when a user starts the skill without a specific intent.
        protected override string WELCOME_MESSAGE { get; set; } = "Welcome to the {0}!  World of words is an interactive way to learn and challenge yourself with words. Please choose from level 1-5";

        //This is the message a user will hear when they try to cancel or stop the skill, or when they finish a quiz.
        protected override string EXIT_SKILL_MESSAGE { get; set; } = "Thank you for playing the {0}!  Let's play again soon!";

        //This is the message a user will hear after they ask (and hear) about a specific data element.

        //This is the message a user will hear when they ask Alexa for help in your skill.
        protected override string HELP_MESSAGE { get; set; } = "I start by saying a word. You then have to respond back with a word that begins with the letter in which my word ended. I do the same in response to your word. As you say words, you earn scores with each word.  Compete with me to make a high score. Playing with words was never so much fun! " +
            "The game continues until someone fails to respond to the next word or you say Exit.";



        //This is the message a user will hear when they start a quiz.
        protected override string START_QUIZ_MESSAGE { get; set; } = "Let's start play.";



        protected static int MAX_CHAIN = 100;
        protected AppState appstate = AppState.Start;
        protected const string GAMESCORE = "gamescore";
        protected const string LEVEL = "level";

        /// <summary>
        /// Get current state of the app whether its a quiz or fact based
        /// </summary>
        /// <param name="input"></param>
        /// <returns>appstate</returns>
        protected AppState GetAppState(Session session)
        {
            AppState ret = AppState.Start;
            string property = GetStringAttributeProperty(session.Attributes, STATE);
            if (!string.IsNullOrEmpty(property) && property.Equals(AppState.Game.ToString()))
            {
                ret = AppState.Game;
            }
            return ret;
        }

        //If you don't want to use cards in your skill, set the USE_CARDS_FLAG to false. 
        //If you set it to true, you will need an image for each item in your data.
        private static List<WordListEntry> theItems = null;
        private int counter = 0;
        private int level = 1;
        private int gamescore = 0;
        protected List<WordPlayEntry> playlist;
        protected List<string> outputspeech2 = new List<string>();
        protected List<string> outputtext2 = new List<string>();

        protected void AddOutput(string speech, string text)
        {
            if (!string.IsNullOrEmpty(speech)) outputspeech2.Add(speech);
            if (!string.IsNullOrEmpty(text)) outputtext2.Add(text + Environment.NewLine);
        }

        protected void AddOutput(string speechtext)
        {
            this.AddOutput(speechtext, speechtext);
        }

        protected int HIGHEST_LEVEL = 5;

        public override Task<SkillResponse> OnLaunchAsync(LaunchRequest launchRequest, Session session, Context context)
        {
            return Task.FromResult(GetWelcomeResponse());
        }

        private SkillResponse GetWelcomeResponse()
        {
            var output = string.Format(WELCOME_MESSAGE, this.SkillName);
            return BuildSimpleResponse("Welcome", output, false);
        }

        protected async Task<SkillResponse> ContinuePlay(IntentRequest intentRequest, Session session, Context context, bool RepeatWord)
        {
            SkillResponse response = null;
            if (playlist.Count > 0)
            {
                var lastword = playlist[playlist.Count - 1].spokenword;
                if (RepeatWord)
                    this.AddOutput(this.GetChosenWordSpeech(lastword, true),lastword);
                else
                {
                    var str1 = $"Your turn with {breakstrong} {lastword.Last()}. ";
                    this.AddOutput(str1, lastword);
                }
                response = BuildResponse("Play", false);
            }
            else
                response = await GenerateNextWord(intentRequest, session, context, false);

            return response;
        }
        public override async Task<SkillResponse> OnIntentAsync(IntentRequest intentRequest, Session session, Context context)
        {
            // Get intent from the request object.
            var intent = intentRequest.Intent;
            var intentName = intent?.Name;
            SkillResponse response = null;
            // Note: If the session is started with an intent, no welcome message will be rendered;
            // rather, the intent specific response will be returned.
            MakeItemList();
            appstate = GetAppState(session);
            counter = GetIntAttributeProperty(session.Attributes, COUNTER);
            level = Math.Max(1, GetIntAttributeProperty(session.Attributes, LEVEL));
            gamescore = Math.Max(0, GetIntAttributeProperty(session.Attributes, GAMESCORE));
            playlist = GetTypedAttributeProperty<List<WordPlayEntry>>(session.Attributes, PLAYLIST);
            if (playlist == null) playlist = new List<WordPlayEntry>();

            switch (intentName)
            {
                case "LevelIntent":
                    var gotolevel = this.GetIntSlotValue(intent.Slots, "gotolevel");
                    if (gotolevel > HIGHEST_LEVEL) gotolevel = HIGHEST_LEVEL;
                    if (appstate == AppState.Start)
                    {
                        if (gotolevel < 1) gotolevel = level;   //maintain the level if not specified or misunderstood by Alexa
                        level = gotolevel;
                        this.AddOutput($"Playing level {level}. ");
                        appstate = AppState.Game;
                        response = await this.ContinuePlay(intentRequest, session, context, true);
                    }
                    else
                    {
                        if (gotolevel > 0)
                        {
                            if (gotolevel == level) this.AddOutput($"Already playing at level {level}.");
                            else
                            {
                                level = gotolevel;
                                this.AddOutput($"Switching to level {level}.");
                            }
                        }
                        response = await this.ContinuePlay(intentRequest, session, context, false);
                    }
                    break;
                case "PlayIntent":
                    response = await EvaluatePlayWord(intentRequest, session, context);
                    break;
                case "ScoreIntent":
                    this.AddOutput(GetCurrentScore(gamescore, counter), gamescore.ToString());
                    response = await this.ContinuePlay(intentRequest, session, context, false);
                    break;
                case "MeaningIntent":
                    if (playlist.Count > 0)
                    {
                        var lastword = playlist[playlist.Count - 1];
                        var client = new OxfordApiClient();
                        var result = client.GetDictionaryEntry(lastword.spokenword);
                        var def = "";
                        if ((result != null) && (result.Results != null) && (result.Results.Count() > 0))
                        {
                            def = result.Results[0].LexicalEntries[0].Entries[0].Senses[0].Definitions[0];
                        } else
                        {
                            def = "Sorry - Could not find the meaning of this word. Lets continue play.";
                        }
                        this.AddOutput(def + "<break time='1s'/>", def);

                    }

                    response = await this.ContinuePlay(intentRequest, session, context, false);
                    break;
                case "SkipIntent":

                    var SkipSlot = this.GetStringSlotValue(intent.Slots, "SkipSlot");
                    if (myUtils.IsInList(SkipSlot, "word", "letter", "alphabet"))
                    {
                        var exclaim = sayas_interject + "Ooh la la !" + sayas + breakstrong;
                        this.AddOutput($"{exclaim} I am so cool. Ain't I?", "");
                        response = await GenerateNextWord(intentRequest, session, context, true);
                    } else {
                        response = await this.ContinuePlay(intentRequest, session, context, false);
                    }
                    break;
                case AlexaConstants.RepeatIntent:
                    response = await this.ContinuePlay(intentRequest, session, context, true);
                    break;
                case AlexaConstants.CancelIntent:
                    this.AddOutput(string.Format(EXIT_SKILL_MESSAGE, this.SkillName),"Exiting");
                    if (counter>0) this.AddOutput(GetFinalScore(gamescore, counter), gamescore.ToString());
                    response = BuildResponse("Cancel", true);
                    break;

                case AlexaConstants.StartOverIntent:
                    this.InitCounters();
                    response = await GenerateNextWord(intentRequest, session, context, false);
                    break;

                case AlexaConstants.StopIntent:

                    this.AddOutput(string.Format(EXIT_SKILL_MESSAGE, this.SkillName),"Exiting");
                    if (counter>0) this.AddOutput(GetFinalScore(gamescore, counter),gamescore.ToString());
                    response = BuildResponse("Stop", true);
                    break;

                case AlexaConstants.HelpIntent:
                    response = BuildSimpleResponse("Help", HELP_MESSAGE, false);
                    break;
                default:
                    if (appstate == AppState.Game)
                    {
                        response = await GenerateNextWord(intentRequest, session, context, false);
                    }
                    else
                    {
                        response = GetWelcomeResponse();
                    }
                    break;
            }

            foreach (string str1 in new string[] { STATE, LEVEL })
            {
                if (session.Attributes.ContainsKey(str1)) session.Attributes.Remove(str1);
            }

            session.Attributes.Add(STATE, appstate);
            session.Attributes.Add(LEVEL, level);
            return response;
        }

     
        public override Task<SkillResponse> OnSessionEndedAsync(SessionEndedRequest sessionEndedRequest, Session session, Context context)
        {
            return Task.FromResult<SkillResponse>(null);
        }

        protected string GetChosenWordSpeech(string word, bool repeated)
        {
            var startpart = repeated ? "Last word was " : "I say";
            //{breakstrong} {word.First()} for 
            var str1 = $"{startpart} {emphasis_strong}{word}{emphasis}. {sayas_spellout}{word}{sayas}. Your turn with {breakstrong} {word.Last()}. ";
            return str1;
        }

        private void InitCounters()
        {
            counter = 0;
            gamescore = 0;
            playlist.Clear();
        }


        /// <summary>
        /// Check the answer returned and determine the correct response
        /// then configure the next question if there is to be one storing the data
        /// inthe sessionAttributes array
        /// </summary>
        /// <param name="input"></param>
        /// <param name="innerReponse"></param>
        /// <returns>void</returns>
        private async Task<SkillResponse> EvaluatePlayWord(IntentRequest intentRequest, Session session, Context context)
        {

            var phrase = this.GetStringSlotValue(intentRequest.Intent.Slots, "Phrase");

            if (string.IsNullOrWhiteSpace(phrase) || (phrase.Trim().Length < 3))
            {
                if (phrase.Trim().Length < 3)
                    this.AddOutput("Too short!");
                else
                    this.AddOutput("Cannot find answer.");
                if (playlist.Count > 0)
                {
                    var lastword = playlist[playlist.Count - 1].spokenword;
                    this.AddOutput(this.GetChosenWordSpeech(lastword, true),lastword);
                    return BuildResponse("Play", false);
                }
                else
                    return await GenerateNextWord(intentRequest, session, context, false);


            }
            else
            {
                var lastword = playlist[playlist.Count - 1].spokenword;
                if (phrase.First().ToString().ToLower() == lastword.Last().ToString().ToLower())
                {
                    var client = new OxfordApiClient();
                    var result = client.GetLemma(phrase);
                    if ((result == null) || (result.Results == null) || (result.Results.Count() == 0))
                    {
                        this.AddOutput(GetSpeechCon(false),"");
                        this.AddOutput("Invalid word.");
                        this.AddOutput(this.GetChosenWordSpeech(lastword, true),lastword);
                        return BuildResponse("Play", false);

                    }
                    else
                    {
                        var rootword = result.Results[0].LexicalEntries[0].InflectionOf[0].Id;
                        var foundlist = playlist.Where(wordPlayEntry => myUtils.IsInList(rootword, wordPlayEntry.rootword)).ToList();
                        var cnt = foundlist.Count();
                        if (foundlist.Count == 0)
                        {
                            gamescore++;
                            this.AddOutput(GetSpeechCon(true),"");
                            playlist.Add(new WordPlayEntry() { rootword = rootword, spokenword = phrase });
                            if (counter < MAX_CHAIN)
                            {
                                //outputspeech.Add( GetCurrentScore(gamescore, counter));
                                return await GenerateNextWord(intentRequest, session, context, false);
                            }
                            else
                            {
                                this.AddOutput(GetFinalScore(gamescore, counter),gamescore.ToString());
                                this.AddOutput(" " + string.Format(EXIT_SKILL_MESSAGE, this.SkillName),"Exiting");
                                appstate = AppState.Start;
                                InitCounters();
                                return BuildResponse("Exit", true);
                            }

                        }
                        else
                        {
                            this.AddOutput(GetSpeechCon(false),"");
                            this.AddOutput($"This word was already spoken as {breakstrong} {foundlist[0].spokenword}.",
                                $"This word was already spoken as {foundlist[0].spokenword}");
                            this.AddOutput(this.GetChosenWordSpeech(lastword, true), lastword);
                            return BuildResponse("Play", false);

                        }

                    }

                }
                else
                {
                    this.AddOutput(GetSpeechCon(false),"");
                    this.AddOutput($"This word {phrase} does not start with correct letter.");
                    this.AddOutput(this.GetChosenWordSpeech(lastword, true),lastword);
                    return BuildResponse("Play", false);

                }
            }

        }

        /// <summary>
        /// Builds up the details required to ask a question and stores them in the
        /// sessionAtrtibutes array
        /// </summary>
        /// <param name="input"></param>
        /// <param name="innerReponse"></param>
        /// <returns>void</returns>
        private Task<SkillResponse> GenerateNextWord(IntentRequest intentRequest, Session session, Context context, bool userSkipped)
        {

            if (counter <= 0)
            {
                this.AddOutput(START_QUIZ_MESSAGE + " ","Starting");
                counter = 0;
            }

            foreach (string str1 in new string[] { COUNTER, GAMESCORE, PLAYLIST })
            {
                if (session.Attributes.ContainsKey(str1)) session.Attributes.Remove(str1);
            }

            counter++;
            session.Attributes.Add(COUNTER, counter);

            char lastletter;
            if (playlist.Count > 0)
            {
                var lastentry = playlist[playlist.Count - 1];
                if (userSkipped) lastletter = lastentry.spokenword.ToLower().First();
                else lastletter = lastentry.spokenword.ToLower().Last();
            }
            else
            {
                lastletter = (char)('a' + GetRandomNumber(0, 25));
            }


            List<WordListEntry> lst = new List<WordListEntry>();
            var foundlevel = level;
            while (foundlevel <= HIGHEST_LEVEL)
            {
                lst = theItems.Where(entry =>
                   ((entry.Word.First() == lastletter) && (entry.Level == foundlevel ) &&
                       (playlist.Where(x => myUtils.IsInList(x.rootword, entry.Word)).ToList().Count == 0))

                ).ToList();
                if (lst.Count > 0) break;
                foundlevel++;
                if ((foundlevel > HIGHEST_LEVEL) && (playlist.Count == 0))
                {
                    //started with a random letter that did not exist in the word list
                    lastletter = (char)('a' + GetRandomNumber(0, 25));
                    foundlevel = level;
                }
            }



            if (lst.Count == 0)
            {
                //game is up
                this.AddOutput($"Bravo! I have exhausted my supply of words. You win !","You win!");
                this.AddOutput(GetFinalScore(gamescore, counter),gamescore.ToString());
                this.AddOutput(" " + string.Format(EXIT_SKILL_MESSAGE, this.SkillName), "Exiting");
                appstate = AppState.Start;
                InitCounters();
                return Task.FromResult(BuildResponse("Exit", true));

            }
            else
            {
                //if (level != foundlevel) this.AddOutput($"Bravo! You are playing so well that I am switching to level {level}. ","");
                var cnt = GetRandomNumber(0, lst.Count - 1);
                var nextitem = lst[cnt];
                playlist.Add(new WordPlayEntry() { rootword = nextitem.Word, spokenword = nextitem.Word });
                session.Attributes.Add(PLAYLIST, playlist);
                session.Attributes.Add(GAMESCORE, gamescore);

                this.AddOutput(this.GetChosenWordSpeech(nextitem.Word, false), nextitem.Word);

                return Task.FromResult(BuildResponse("Play", false));
            }
        }




        /// <summary>
        /// Get a named item from the slots 
        /// </summary>
        /// <param name="slots"></param>
        /// <param name="out text"></param>
        /// <returns>Item</returns>
        private WordListEntry GetWordEntry(IDictionary<string, Slot> slots, out string valueName)
        {
            valueName = string.Empty;
            try
            {

                foreach (KeyValuePair<string, Slot> kvp in slots)
                {
                    if (!string.IsNullOrEmpty(kvp.Value.Value))
                    {
                        string val = kvp.Value.Value.ToLower();
                        valueName = kvp.Value.Value;
                        var item = theItems.FindAll(x => x.Word.ToLower().Equals(val)).FirstOrDefault();
                        if (item != null)
                        {
                            return item;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log("GeWordEntry " + ex.Message);
            }
            return null;
        }




        protected SkillResponse BuildResponse(string title, bool shouldEndSession)
        {
            var speech = string.Join(" ", outputspeech2.ToArray());
            var text = string.Join(Environment.NewLine, outputtext2.ToArray());

            SsmlOutputSpeech output = new SsmlOutputSpeech();
            output.Ssml = "<speak>" + speech + "</speak>";
            var card = new SimpleCard
            {
                Title = $"{SkillName} - {title}",
                 Content= text
            };

            return this.BuildResponse(card, output, shouldEndSession);
        }


        /// <summary>
        /// Create the list of USA States with details  
        /// </summary>
        /// <returns></returns>
        private void MakeItemList()
        {
            if (theItems == null)
            {
                theItems = this.ItemsArray();
            }
        }



        /// <summary>
        /// return the list of states
        /// </summary>
        /// <param name="property"></param>
        /// <returns>List<Item> </returns>
        public List<WordListEntry> ItemsArray()
        {
            var path = ServiceHostProvider.FindFile("Alexa/Wordy/wordlist.json");
            var str1 = System.IO.File.ReadAllText(path);
            var lst = JsonConvert.DeserializeObject<List<WordListEntry>>(str1);
            return lst;
        }
    }
}