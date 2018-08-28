using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MyMoney.Core;
using MyMoney.Core.Model;
using MyMoney.Core.Table;

namespace MyMoney.Windows.Components
{
    public class GraphHandler
    {

        private readonly Chart _chart;

        private readonly IDataController _controller;

        public GraphHandler(Chart chart, IDataController controller)
        {
            this._chart = chart;
            this._controller = controller;
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
            String monthString = month.ToString("MMMM yyyy");


            double monthlyAllowance = _controller.GetMonthlyAllowance(month);

            double currentTotal = GetValues(monthlyAllowance.Equals(Double.NaN) ? 200 : monthlyAllowance, month, out int[] xValues, out double[] yValues);

            // A series to be added to the graph
            Series series = new Series(monthString);
            series.ChartType = SeriesChartType.Line;
            series.Legend = "";

            // Set x axis details
            Axis xAxis = new Axis();
            xAxis.Title = "Transaction Number";
            xAxis.MajorGrid.Enabled = false;
            xAxis.Minimum = 0;

            // Set y axis details
            Axis yAxis = new Axis();
            yAxis.Title = "Available Funds £";
            yAxis.MajorGrid.Enabled = false;

            // Set Title details
            Title title = new Title(monthString + " - Total: " + currentTotal);
            title.Alignment = ContentAlignment.TopCenter;

            // A chart area that shows the axis labels.
            ChartArea chartArea = new ChartArea(monthString);

            // Set chart area axis
            chartArea.AxisY = yAxis;
            chartArea.AxisX = xAxis;

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

        private double GetValues(double monthlyAllowance, DateTime now, out int[] xValues, out double[] yValues)
        {
            // Store the values in lists as the length on a list is flexible.
            List<int> xValuesList = new List<int>();
            List<double> yValuesList = new List<double>();

            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var endOfMonth = new DateTime(now.Year, now.Month, 1).AddMonths(1).AddDays(-1);

            // Get all the transactions of the current month
            Row[] rows = _controller.GetRows(row =>
            {

                DateTime rowDateTime = DateTime.Parse(row.GetValue(CashFlowModel.DATE_COLOUMN));

                return DateTime.Compare(rowDateTime, startOfMonth) >= 0 && DateTime.Compare(rowDateTime, endOfMonth) <= 0;

            }, CashFlowModel.TABLE_NAME);


            // Holds the transaction number that will be displayed on screen.
            int index = 0;

            // Add the allowance to the list of y values and the index as the 
            // transaction number in the x values.
            yValuesList.Add(monthlyAllowance);
            xValuesList.Add(index);

            // Iterate through all the transactions in the past month starting at the oldest.
            for (index = 1; index < rows.Length + 1; index++)
            {
                Row row = rows[rows.Length - index];


                // Add the current transaction value to the monthly allowance.
                monthlyAllowance += double.Parse(row.GetValue(CashFlowModel.AMOUNT_COLOUMN));

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
