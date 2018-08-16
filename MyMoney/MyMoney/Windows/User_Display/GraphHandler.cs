using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyMoney.Database;
using MyMoney.Data_Storage;

namespace MyMoney.Windows.User_Display
{
    /// <summary>
    /// Encapsulates the behaviour of a line chart on a form. The chart on which this handler 
    /// operates is specified in the constructor. This handler is multi-thread safe.
    /// </summary>
    class GraphHandler
    {
        /// <summary>
        /// The chart that this handler will operate on.
        /// </summary>
        private Chart chart;

        /// <summary>
        /// Constructs a new <see cref="GraphHandler"/>.
        /// </summary>
        /// <param name="chart">The chart that this handler should operate on.</param>
        public GraphHandler(Chart chart)
        {
            this.chart = chart;
        }

        /// <summary>
        /// Draws the graph using the month specified.
        /// </summary>
        /// <param name="month">
        /// The month of transactions to be displayed on the graph.
        /// </param>
        public void draw(DateTime month)
        {
            checkInvoke(month);
        }

        /// <summary>
        /// Draws the graph using the current month.
        /// </summary>
        public void draw()
        {

            // Get the current date time.
            DateTime now = System.DateTime.Now;

            checkInvoke(now);
        }

        /// <summary>
        /// Checks if the chart reuires invoking before ploting the graph.
        /// </summary>
        /// <param name="month">
        /// The month of transactions to be displayed on the graph.
        /// </param>
        private void checkInvoke(DateTime month)
        {

            // Check if the chart requires invoking. This causes the action to be thread safe.
            if (chart.InvokeRequired)
            {
                chart.BeginInvoke((MethodInvoker)delegate()
                {
                    plot(month);
                });
            }
            else
            {
                plot(month);
            }


        }

        /// <summary>
        /// calculates the points on the graph and adds them to the chart series.
        /// </summary>
        private void plot(DateTime month)
        {

            // Get the long string format of the specified month.
            String monthString = month.ToString("MMMM yyyy");

            int[] xValues;
            double[] yValues;

            double monthlyAllowance = Budget.getInstance().getBudget((month.Month < 10 ? "0" + month.Month : "" + month.Month) + "" + month.Year);

            double currentTotal = getValues(monthlyAllowance.Equals(Double.NaN) ? 200 : monthlyAllowance, month, out xValues, out yValues);

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
            title.Alignment = System.Drawing.ContentAlignment.TopCenter;

            // A chart area that shows the axis labels.
            ChartArea chartArea = new ChartArea(monthString);

            // Set chart area axis
            chartArea.AxisY = yAxis;
            chartArea.AxisX = xAxis;

            // Add the points to the series
            series.Points.DataBindXY(xValues, yValues);

            // Add the chart area for axis labals
            chart.ChartAreas.Clear();
            chart.ChartAreas.Add(chartArea);

            // Add the series.
            chart.Series.Clear();
            chart.Series.Add(series);
            chart.Series[0].IsVisibleInLegend = false;
            chart.Titles.Clear();
            chart.Titles.Add(title);



        }

        /// <summary>
        /// Retrieves the x and y values to be displayed on the graph based on the users monthly 
        /// allowance and the current date time..
        /// </summary>
        /// <param name="monthlyAllowance">The amount of money that the users starts the month with.</param>
        /// <param name="now">The current date time.</param>
        /// <param name="xValues">This return variable contains all x values to be displayed on the graph.</param>
        /// <param name="yValues">This return variable contains all y values to be displayed on the graph.</param>
        private double getValues(double monthlyAllowance, DateTime now, out int[] xValues, out double[] yValues)
        {
            // Store the values in lists as the length on a list is flexible.
            List<int> xValuesList = new List<int>();
            List<double> yValuesList = new List<double>();

            // Get all the transactions of the current month
            Row[] rows = CashFlow.getInstance().getRows(now);

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
                monthlyAllowance += Double.Parse(row.getValue(CashFlow.AMOUNT_COLOUMN));

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
