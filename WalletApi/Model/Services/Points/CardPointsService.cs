namespace WalletApi.Model.Services
{
    public class CardPointsService : ICardPointsService
    {
        public long GetPoints(DateTime date)
        {
            date = date.Date;

            var first_day_date = GetSeasonFirstDayDate(date);

            var season_day = (int)(date - first_day_date).TotalDays + 1;

            return GetPointsByDay(season_day);
        }

        private DateTime GetSeasonFirstDayDate(DateTime date) 
        {
            int month = date.Month switch
            {
                12 or 1 or 2 => 12,
                3 or 4 or 5 => 3,
                6 or 7 or 8 => 6,
                9 or 10 or 11 => 9,
                _ => throw new NotSupportedException() // Unreachable code
            };

            int year = date.Month == 1 || date.Month == 2 ? date.Year - 1 : date.Year;

            return new DateTime(year, month, 1);
        }

        private long GetPointsByDay(int day)
        {
            if (day == 1) return 1;
            if (day == 2) return 2;

            long second_prev_points = 1;
            long prev_points = 2;
            long points = 0;
            for (int i = 3; i <= day; i++)
            {
                points = (long)Math.Round(prev_points * 0.6) + second_prev_points;
                second_prev_points = prev_points;
                prev_points = points;
            }

            return points;
        }
    }
}
