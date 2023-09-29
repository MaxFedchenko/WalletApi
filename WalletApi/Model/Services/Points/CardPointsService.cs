namespace WalletApi.Model.Services
{
    public class CardPointsService : ICardPointsService
    {
        public int GetCurrentPoints()
        {
            var now = DateTime.Now;
            var m = now.Month % 12 / 3 * 3;
            m = m != 0 ? m : 12;
            var first_day_date = new DateTime(now.Year, m, 1);

            var season_day = now.DayOfYear - first_day_date.DayOfYear + 1;

            return GetPointsByDay(season_day);
        }

        private int GetPointsByDay(int day) => day switch
        {
            1 => 2,
            2 => 3,
            _ => GetPointsByDay(day - 2) + (int)Math.Round(GetPointsByDay(day - 1) * 0.6)
        };
    }
}
