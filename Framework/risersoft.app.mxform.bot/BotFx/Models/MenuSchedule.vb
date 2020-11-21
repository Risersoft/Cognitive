Imports System.Collections.Generic
Imports Newtonsoft.Json

Namespace Models

	Public Class MenuSchedule
		<JsonProperty("initialDate")> _
		Public Property InitialDate() As DateTime
			Get
				Return m_InitialDate
			End Get
			Set
				m_InitialDate = Value
			End Set
		End Property
		Private m_InitialDate As DateTime

		<JsonProperty("menu")> _
		Public Property Menu() As List(Of Menu)
			Get
				Return m_Menu
			End Get
			Set
				m_Menu = Value
			End Set
		End Property
		Private m_Menu As List(Of Menu)

		Public Function GetMenuForDate([date] As DateTime) As Menu
			Dim daysBetween = ([date] - InitialDate).TotalDays
			If daysBetween < 0 Then
				Return Nothing
			End If

			' Count forward to find index of menu for the requested date
			Dim dayOfWeek__1 = DayOfWeek.Monday
			Dim menuIndex = 0
            For i = 0 To daysBetween - 1
                dayOfWeek__1 = IncrementDayOfWeek(dayOfWeek__1)

                ' If a week-day, advance to next menu item
                If IsWeekDay(dayOfWeek__1) Then
                    menuIndex += 1

                    ' Reset to first item in menu once we've got to the end
                    If menuIndex = Menu.Count Then
                        menuIndex = 0
                    End If
                End If
            Next

            Return Menu(menuIndex)
		End Function

		Private Shared Function IncrementDayOfWeek(dayOfWeek__1 As DayOfWeek) As DayOfWeek
			If dayOfWeek__1 = DayOfWeek.Saturday Then
				Return DayOfWeek.Sunday
			End If

			Return CType(CInt(dayOfWeek__1) + 1, DayOfWeek)
		End Function

		Private Shared Function IsWeekDay(dayOfWeek__1 As DayOfWeek) As Boolean
			Return Not (dayOfWeek__1 = DayOfWeek.Saturday OrElse dayOfWeek__1 = DayOfWeek.Sunday)
		End Function
	End Class
End Namespace
