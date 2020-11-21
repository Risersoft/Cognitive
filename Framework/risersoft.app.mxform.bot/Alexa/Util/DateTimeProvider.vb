Namespace Providers

	Public Class DateTimeProvider
		Implements IDateTimeProvider
		Public Function Now() As DateTime Implements IDateTimeProvider.Now
			Return DateTime.Now
		End Function
	End Class
End Namespace
