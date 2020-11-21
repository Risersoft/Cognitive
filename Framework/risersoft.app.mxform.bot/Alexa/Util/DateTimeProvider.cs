namespace CognitiveServiceRsMx.Providers
{
    using System;

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}