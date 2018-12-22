using MyMoney.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace MyMoney.Windows.Components
{
    public class GraphHandler
    {

        private readonly Chart _chart;
        private readonly IController controller;

        public GraphHandler(Chart chart, IController controller)
        {
            this._chart = chart;
            this.controller = controller;
        }

        public void Draw(DateTime month)
        {
            CheckInvoke(month);
        }

        public void Draw()
        {

            // Get the current date time.
            DateTime now = DateTime.Now;

            CheckInvoke(now);
        }

        private void CheckInvoke(DateTime month)
        {

            // Check if the chart requires invoking. This causes the action to be thread safe.
            if (_chart.InvokeRequired)
            {
                _chart.BeginInvoke((MethodInvoker)delegate
                {
                    Plot(month);
                });
            }
            else
            {
                Plot(month);
            }


        }

        private void Plot(DateTime month)
        {

            // Get the long string format of the specified month.
            string monthString = month.ToString("MMMM yyyy");

            using (var model = controller.Database()) {

                var budget = model.Budgets.Find(month);

                double monthlyAllowance = budget?.Amount ?? 200;

                double currentTotal = GetValues(model, monthlyAllowance , month, out int[] xValues, out double[] yValues);

                // A series to be added to the graph
                Series series = new Series(monthString)
                {
                    ChartType = SeriesChartType.Line,
                    Legend = ""
                };

                // Set x axis details
                Axis xAxis = new Axis
                {
                    Title = "Transaction Number"
                };
                xAxis.MajorGrid.Enabled = false;
                xAxis.Minimum = 0;

                // Set y axis details
                Axis yAxis = new Axis
                {
                    Title = "Available Funds £"
                };
                yAxis.MajorGrid.Enabled = false;

                // Set Title details
                Title title = new Title(monthString + " - Total: " + currentTotal)
                {
                    Alignment = ContentAlignment.TopCenter
                };

                // A chart area that shows the axis labels.
                ChartArea chartArea = new ChartArea(monthString)
                {

                    // Set chart area axis
                    AxisY = yAxis,
                    AxisX = xAxis
                };

                // Add the points to the series
                series.Points.DataBindXY(xValues, yValues);

                // Add the chart area for axis labals
                _chart.ChartAreas.Clear();
                _chart.ChartAreas.Add(chartArea);

                // Add the series.
                _chart.Series.Clear();
                _chart.Series.Add(series);
                _chart.Series[0].IsVisibleInLegend = false;
                _chart.Titles.Clear();
                _chart.Titles.Add(title);
            }
        }

        private double GetValues(IDatabase model, double monthlyAllowance, DateTime now, out int[] xValues, out double[] yValues)
        {
            // Store the values in lists as the length on a list is flexible.
            var xValuesList = new List<int>();
            var yValuesList = new List<double>();

            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var endOfMonth = new DateTime(now.Year, now.Month, 1).AddMonths(1).AddDays(-1);

            var transactions = model.Transactions.Where(startOfMonth, endOfMonth).ToArray();

            // Holds the transaction number that will be displayed on screen.
            int index = 0;

            // Add the allowance to the list of y values and the index as the 
            // transaction number in the x values.
            yValuesList.Add(monthlyAllowance);
            xValuesList.Add(index);

            // Iterate through all the transactions in the past month starting at the oldest.
            for (index = 1; index <= transactions.Length; index++)
            {
                
                // Add the current transaction value to the monthly allowance.
                monthlyAllowance += transactions[index - 1].Amount;

                // Add the new allowance to the list of y values and the index as the 
                // transaction number in the x values.
                yValuesList.Add(monthlyAllowance);
                xValuesList.Add(index);
            }

            // Convert the list of x and y values into an array.
            xValues = xValuesList.ToArray<int>();
            yValues = yValuesList.ToArray<double>();

            return monthlyAllowance;
        }

    }
}
