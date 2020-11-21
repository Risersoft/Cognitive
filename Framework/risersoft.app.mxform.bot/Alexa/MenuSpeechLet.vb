Imports System
Imports System.IO
Imports System.Threading.Tasks
Imports System.Web
Imports AlexaSkillsKit.Slu
Imports AlexaSkillsKit.Speechlet
Imports AlexaSkillsKit.UI
Imports CognitiveServiceRsMx.Helpers
Imports CognitiveServiceRsMx.Models
Imports Newtonsoft.Json
Imports risersoft.app.mxform.bot.Helpers
Imports risersoft.app.mxform.bot.Models

Namespace SchoolMenuSkill.Speechlet

    Public Class MenuSpeechlet
        Inherits SpeechletAsync
        Private Shared _menuSchedule As MenuSchedule

        Private Const DateKey As String = "Date"
        Private Const MenuForDateIntent As String = "MenuForDateIntent"

        Public Overrides Function OnLaunchAsync(launchRequest As LaunchRequest, session As Session) As Task(Of SpeechletResponse)
            Return Task.FromResult(GetWelcomeResponse())
        End Function

        Private Function GetWelcomeResponse() As SpeechletResponse
            Dim output = "Welcome to the English International School menu app. Please request the menu for a given date."
            Return BuildSpeechletResponse("Welcome", output, False)
        End Function

        Public Overrides Async Function OnIntentAsync(intentRequest As IntentRequest, session As Session) As Task(Of SpeechletResponse)
            ' Get intent from the request object.
            Dim intent = intentRequest.Intent
            Dim intentName = intent?.Name

            ' Note: If the session is started with an intent, no welcome message will be rendered;
            ' rather, the intent specific response will be returned.
            If MenuForDateIntent.Equals(intentName) Then
                Return Await GetMenuResponse(intent, session)
            End If

            Throw New SpeechletException("Invalid Intent")
        End Function

        Public Overrides Function OnSessionStartedAsync(sessionStartedRequest As SessionStartedRequest, session As Session) As Task
            Return Task.FromResult(0)
        End Function

        Public Overrides Function OnSessionEndedAsync(sessionEndedRequest As SessionEndedRequest, session As Session) As Task
            Return Task.FromResult(0)
        End Function

        Private Async Function GetMenuResponse(intent As Intent, session As Session) As Task(Of SpeechletResponse)
            ' Retrieve date from the intent slot
            Dim dateSlot = intent.Slots(DateKey)

            ' Create response
            Dim output As String
            Dim dated As DateTime
            Dim endSession = False
            If dateSlot IsNot Nothing AndAlso DateTime.TryParse(dateSlot.Value, dated) Then
                ' Retrieve and return the menu response
                If _menuSchedule Is Nothing Then
                    _menuSchedule = Await LoadMenuSchedule()
                End If

                Dim menu = _menuSchedule.GetMenuForDate(dated)
                If menu IsNot Nothing Then
                    output = _menuSchedule.GetMenuForDate(dated).ToString(dated)
                    endSession = True
                Else
                    output = String.Format("Sorry, no menu is available for {0}.  Please try another date or say quit to exit", dated.ToStringWithSuffix("d MMMM"))
                End If
            Else
                ' Render an error since we don't know what the date requested is
                output = "I'm not sure which date you require, please try again or say quit to exit."
            End If

            ' Return response, passing flag for whether to end the conversation
            Return BuildSpeechletResponse(intent.Name, output, endSession)
        End Function

        Private Async Function LoadMenuSchedule() As Task(Of MenuSchedule)
            Dim filePath = HttpContext.Current.Server.MapPath("/App_Data/menu.json")
            Using reader = New StreamReader(filePath)
                Dim json = Await reader.ReadToEndAsync()
                Return JsonConvert.DeserializeObject(Of MenuSchedule)(json)
            End Using
        End Function

        ''' <summary>
        ''' Creates and returns the visual and spoken response with shouldEndSession flag
        ''' </summary>
        ''' <param name="title">Title for the companion application home card</param>
        ''' <param name="output">Output content for speech and companion application home card</param>
        ''' <param name="shouldEndSession">Should the session be closed</param>
        ''' <returns>SpeechletResponse spoken and visual response for the given input</returns>
        Private Function BuildSpeechletResponse(title As String, output As String, shouldEndSession As Boolean) As SpeechletResponse
            ' Create the Simple card content
            Dim card = New SimpleCard() With {
            .Title = String.Format("SessionSpeechlet - {0}", title),
            .Content = String.Format("SessionSpeechlet - {0}", output)
            }

            ' Create the plain text output
            Dim speech = New PlainTextOutputSpeech() With {
             .Text = output
            }

            ' Create the speechlet response
            Dim response = New SpeechletResponse() With {
             .ShouldEndSession = shouldEndSession,
             .OutputSpeech = speech,
             .Card = card
            }
            Return response
        End Function
    End Class
End Namespace
