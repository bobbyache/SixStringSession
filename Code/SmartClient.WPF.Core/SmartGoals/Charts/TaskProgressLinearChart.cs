using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using SmartClient.Domain;
using System;

namespace SmartGoals.Charts
{
    public class TaskProgressLinearChart
    {
        public SeriesCollection Series;
        public Func<double, string> Formatter;

        public TaskProgressLinearChart(IGoalTaskProgressSnapshot[] data)
        {
            //// Example From: https://lvcharts.net/App/examples/v1/wpf/Date%20Time
            var dayConfig = Mappers.Xy<IGoalTaskProgressSnapshot>()
              .X(dateModel => dateModel.Day.Ticks / TimeSpan.FromDays(1).Ticks)
              .Y(dateModel => dateModel.Value);

            //Notice you can also configure this type globally, so you don't need to configure every
            //SeriesCollection instance using the type.
            //more info at http://lvcharts.net/App/Index#/examples/v1/wpf/Types%20and%20Configuration
            Series = new SeriesCollection(dayConfig)
            {
                new LineSeries { Values = new ChartValues<IGoalTaskProgressSnapshot>(data) }
            };

            Formatter = value => new DateTime((long)(value * TimeSpan.FromDays(1).Ticks)).ToString("d");
        }
    }
}
