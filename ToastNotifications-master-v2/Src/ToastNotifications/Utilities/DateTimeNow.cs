using System;

namespace ToastNotifications.Utilities
{
    public static class DateTimeNow
    {
        private static IDateTimeProvider _provider;

        static DateTimeNow()
        {
            _provider = new DateTimeProvider();
        }

        public static void SetDateTimeProvider(IDateTimeProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public static DateTime Local => _provider.GetLocalDateTime();

        public static DateTime Utc => _provider.GetUtcDateTime();

        public static DateTime Today => _provider.GetLocalDateTime().Date;
    }
}
