namespace Domain
{
    using System.Threading.Tasks;
    using Olive;

    partial class LogonFailure
    {
        const int ATTEMPTS_BEFORE_CAPTCHA = 3;

        public static async Task<bool> NextAttemptNeedsCaptcha(string email, string ip)
        {
            var record = await Database.FirstOrDefault<LogonFailure>(l => l.IP == ip && l.Email == email);
            var attempts = 1 + record?.Attempts ?? 0;

            return attempts >= ATTEMPTS_BEFORE_CAPTCHA;
        }

        public static async Task<bool> MustShowCaptcha(string email, string ip)
        {
            var record = await Database.FirstOrDefault<LogonFailure>(l => l.IP == ip && l.Email == email);
            var attempts = 1 + record?.Attempts ?? 0;

            await RecordAttempt(email, ip, attempts);

            return attempts >= ATTEMPTS_BEFORE_CAPTCHA;
        }

        public static Task Remove(string email, string ip)
        {
            return Database.DeleteAll<LogonFailure>(x => x.Email == email || x.IP == ip);
        }

        static async Task RecordAttempt(string email, string ip, int attempts)
        {
            var record = await Database.FirstOrDefault<LogonFailure>(l => l.IP == ip && l.Email == email);
            var attempt = record?.Clone() ?? new LogonFailure();

            attempt.Email = email;
            attempt.IP = ip;
            attempt.Attempts = attempts;
            attempt.Date = LocalTime.Now;

            await Database.Save(attempt);
        }
    }
}